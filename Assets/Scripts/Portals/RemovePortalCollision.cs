using UnityEngine;

public class RemovePortalCollision : MonoBehaviour
{
    [SerializeField] private Portal portal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Collider>().excludeLayers += LayerMask.GetMask("PortalSurface");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //return;
        //unused
        if (other.CompareTag("Player"))
        {
            print("Exited!");
            if (other.GetComponent<PlayerManager>().teleported != portal)
            {
                print("teleported = null!");
                other.GetComponent<PlayerManager>().teleported = null;
                other.GetComponent<Collider>().excludeLayers -= LayerMask.GetMask("PortalSurface");
            }
        }
    }
}
