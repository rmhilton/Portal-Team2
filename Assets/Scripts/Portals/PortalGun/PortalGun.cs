using UnityEngine;
using UnityEngine.InputSystem;

public class PortalGun : MonoBehaviour
{
    [SerializeField] PortalPair pair;
    [SerializeField] GameObject portalPrefab;
    public void GunPrimaryInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, LayerMask.GetMask("Ground", "Default", "PortalSurface")))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("PortalSurface"))
                {
                    if(pair.a == null)
                    {
                        TryPlacePortal(hit.point, hit.transform, 1);
                    }
                    else
                    {
                        Destroy(pair.a.gameObject);
                        TryPlacePortal(hit.point, hit.transform, 1);
                    }
                }
            }
        }
    }
    public void GunSecondaryInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, LayerMask.GetMask("Ground","Default","PortalSurface")))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("PortalSurface"))
                {
                    if (pair.b == null)
                    {
                        TryPlacePortal(hit.point, hit.transform, 2);
                    }
                    else
                    {
                        Destroy(pair.b.gameObject);
                        TryPlacePortal(hit.point, hit.transform, 2);
                    }
                }
            }
        }
    }
    /// <summary>
    /// Attempt to place a portal.
    /// </summary>
    /// <param name="position">The position at which a potal should be created.</param>
    /// <param name="transform">The transform on which the portal is being created.</param>
    /// <param name="pairIndex">Which slot the portal should be assigned to. 1 == Portal A, 2 == Portal B</param>
    public void TryPlacePortal(Vector3 position, Transform transform, int pairIndex)
    {
        #region Perform Raycasts for location
        LayerMask obstacle = LayerMask.GetMask("Ground", "Default", "PortalSurface", "PortalBounds");
        RaycastHit hit;
        //raycast up
        if (Physics.Raycast(position, transform.up, out hit, 1.2f, obstacle))
        {
            Debug.Log("up " + hit.transform.name);
            return;
        }
        //raycast down
        else if (Physics.Raycast(position, -transform.up, out hit, 1.2f, obstacle))
        {
            Debug.Log("down " + hit.transform.name);
            return;
        }
        //raycast left
        else if (Physics.Raycast(position, -transform.right, out hit, 0.7f, obstacle))
        {
            Debug.Log("left " + hit.transform.name);
            return;
        }
        //raycast right
        else if (Physics.Raycast(position, transform.right, out hit, 0.7f, obstacle))
        {
            Debug.Log("right " + hit.transform.name);
            return;
        }
        #endregion
        else {
            Debug.Log("Raycast did not fail");
            portalPrefab.GetComponent<Portal>().pair = pair;
            portalPrefab.GetComponent<Portal>().pairIndex = pairIndex;
            Portal newPortal = Instantiate(portalPrefab, position, transform.rotation).GetComponent<Portal>();
            newPortal.transform.parent = transform;
            Portal partner = newPortal.pair.getPartner(newPortal);
            if(!partner.rendering)
            {
                partner.data.Activate(partner);
            }
        }
        
    }
    public void TryMovePortal(Vector3 position, Transform transform, int pairIndex)
    {
        #region Perform Raycasts for location
        LayerMask obstacle = LayerMask.GetMask("Ground", "Default", "PortalSurface", "PortalBounds");
        RaycastHit hit;
        //raycast up
        if (Physics.Raycast(position, transform.up, out hit, 1.2f, obstacle))
        {
            Debug.Log("up " + hit.transform.name);
            return;
        }
        //raycast down
        else if (Physics.Raycast(position, -transform.up, out hit, 1.2f, obstacle))
        {
            Debug.Log("down " + hit.transform.name);
            return;
        }
        //raycast left
        else if (Physics.Raycast(position, -transform.right, out hit, 0.7f, obstacle))
        {
            Debug.Log("left " + hit.transform.name);
            return;
        }
        //raycast right
        else if (Physics.Raycast(position, transform.right, out hit, 0.7f, obstacle))
        {
            Debug.Log("right " + hit.transform.name);
            return;
        }
        #endregion
        else
        {
            Debug.Log("Raycast did not fail");
            switch(pairIndex)
            {
                case 1:
                    pair.a.transform.rotation = transform.rotation;
                    pair.a.transform.position = position;
                    break;
                case 2:
                    pair.b.transform.rotation = transform.rotation;
                    pair.b.transform.position = position;
                    break;
                default:
                    Debug.Log("Tried to move nothing!");
                    break;
            }
            
        }

    }
}
