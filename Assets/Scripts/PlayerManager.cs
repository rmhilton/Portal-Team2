using UnityEngine;
using UnityEngine.InputSystem;

/*  Raymend started this class to set up the new Input system
 * 
 * 
 */

public class PlayerManager : MonoBehaviour
{
    private Vector2 inputDir = Vector2.zero;    //use alongside camera orientation to move player

    public void Move(InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();
        Debug.Log(inputDir);
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
