using UnityEngine;
using UnityEngine.InputSystem;

public class PortalGun : MonoBehaviour
{
    PortalPair pair;
    public void GunPrimaryInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("PortalSurface"))
                {
                    TryPlacePortal(hit.transform.position, hit.transform, 1);
                }
            }
        }
    }
    /// <summary>
    /// Attempt to place a portal.
    /// </summary>
    /// <param name="position">The position at which a potal should be created.</param>
    /// <param name="which">Which slot the portal should be assigned to. 1 == Portal A, 2 == Portal B</param>
    public void TryPlacePortal(Vector3 position, Transform transform, int pairIndex)
    {
        //raycast up
        if (Physics.Raycast(position, transform.up, 2.5f))
        {
            return;
        }
        //raycast down
        if (Physics.Raycast(position, -transform.up, 2.5f))
        {
            return;
        }
        //raycast left
        if (Physics.Raycast(position, -transform.right, 1))
        {
            return;
        }
        //raycast right
        if (Physics.Raycast(position, transform.right, 1))
        {
            return;
        }


    }
}
