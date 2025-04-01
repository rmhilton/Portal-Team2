using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/*  Raymend started this class to set up the new Input system
 *  Joe added movement
 *  Raymend added jump
 */

public class PlayerManager : MonoBehaviour
{
    private Vector2 inputDir = Vector2.zero;

    //movement
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private Transform orientation;
    private Vector3 moveDir = Vector3.zero;
    private Rigidbody rb;

    [SerializeField] private float groundDrag = 5f;
    [SerializeField] private float airDrag = 3f;

    //air movement
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float jumpCooldown = .25f;
    [SerializeField] private float airMultiplier = .4f;
    private bool readyToJump = true;

    //ground check
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private LayerMask groundLayer;
    bool grounded = false;
    bool stopAirDrag = false;

    [SerializeField] private bool breakOnTeleport = false;

    //teleport lock
    public Portal teleported;

    public Vector3 vel;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + 0.2f, groundLayer);

        //resume air drag when grounded
        if(grounded && stopAirDrag) { stopAirDrag = false; }

        SpeedControl();

        //handle drag
        if(grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
        vel = rb.linearVelocity;
    }

    private void FixedUpdate()
    {
        MovePlayer();

        //custom gravity
        rb.AddForce(Vector3.down * Physics.gravity.magnitude * 2f, ForceMode.Force);
    }

    private void MovePlayer()
    {
        moveDir = orientation.forward * inputDir.y + orientation.right * inputDir.x;

        if(grounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        //limit velocity if needed
        if(grounded)
        {
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            }
        }
        else if(!stopAirDrag)
        {
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel * (1 - Time.deltaTime * airDrag);
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            }
        }
    }

    public Vector3 GetVelocity()
    {
        return rb.linearVelocity;
    }

    public void SetVelocity(Vector3 vel)
    {
        print(vel);
        rb.linearVelocity = Vector3.zero;
        if(breakOnTeleport)
        {
            Debug.Break();
        }
        rb.AddForce(vel, ForceMode.Impulse);

        //this is only called when teleporting, so temporarily disable air drag
        stopAirDrag = true;
    }

    public void GetInputDir(InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            //Debug.Log("Jump button pressed!");
            if(readyToJump && grounded)
            {
                readyToJump = false;
                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }
    }

    private void Jump()
    {
        //reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    public void InteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            //Debug.Log("Interact button pressed!");
            if (InteractController.instance)
            {
                InteractController.instance.AttemptInteract();
            }
            else
            {
                Debug.Log("No Interact Controller instance!");
            }
        }
    }

    public void AltInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            //Debug.Log("Interact button pressed!");
            if (InteractController.instance)
            {
                InteractController.instance.AttemptAltInteract();
            }
            else
            {
                Debug.Log("No Interact Controller instance!");
            }
        }
    }

    public void MenuInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if(PauseMenu.instance)
            {
                PauseMenu.instance.PauseButtonPressed();
            }
            else
            {
                Debug.Log("Error: No Pause menu detected!");
            }
            
        }
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.collider.isTrigger)
        {
            if(stopAirDrag)
            {
                stopAirDrag = false;
            }
        }
    }
}
