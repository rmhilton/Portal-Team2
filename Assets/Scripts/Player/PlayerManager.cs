using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/*  Raymend started this class to set up the new Input system
 *  Joe continued it to add movement
 *  Raymend optimized code
 */

public class PlayerManager : MonoBehaviour
{
    private Vector2 inputDir = Vector2.zero;    //use alongside camera orientation to move player

    //movement
    [SerializeField] private float moveSpeed = 6f;
    //[SerializeField] private Transform orientation;

    private void Update()
    {
        Move();
    }

    public void GetInputDir(InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector2 moveDir = inputDir; //multiply inputDir by camera rotation (flattened and normalized) to get movement direction

        transform.position += new Vector3(inputDir.x, 0, inputDir.y) * moveSpeed * Time.deltaTime;
        
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
