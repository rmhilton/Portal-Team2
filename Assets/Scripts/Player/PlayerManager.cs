using UnityEngine;
using UnityEngine.InputSystem;

/*  Raymend started this class to set up the new Input system
 * 
 * 
 */

public class PlayerManager : MonoBehaviour
{
    private Vector2 inputDir = Vector2.zero;    //use alongside camera orientation to move player


    //movement
//    [SerializeField] private float moveSpeed;
  //  [SerializeField] private Transform orientation;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Movement");
        jumpAction = playerInput.actions.FindAction("Jump");
    }

    private void FixedUpdate()
    {
        Move();
    }
    public void Move(/*InputAction.CallbackContext context*/)
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y) * Time.deltaTime;
        
        //inputDir = context.ReadValue<Vector2>();
       // Debug.Log(inputDir);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            Debug.Log("Jump button pressed!");
        }
        
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("Interact button pressed!");
        }
    }
}
