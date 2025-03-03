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

    //air movement
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float jumpCooldown = .25f;
    [SerializeField] private float airMultiplier = .4f;
    private bool readyToJump = true;

    //ground check
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private LayerMask groundLayer;
    bool grounded = false;

    //interactions
    [SerializeField] private InteractController interactController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + 0.2f, groundLayer);

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
    }

    private void FixedUpdate()
    {
        MovePlayer();

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
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.y);
        }
    }

    public void GetInputDir(InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Jump button pressed!");
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
            Debug.Log("Interact button pressed!");
            if(interactController)
            {
                interactController.AttemptInteract();
            }
            else
            {
                Debug.Log("No Interact Controller set in Player script");
            }
        }
    }
}
