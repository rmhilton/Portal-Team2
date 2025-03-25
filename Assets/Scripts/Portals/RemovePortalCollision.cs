using UnityEngine;

public class RemovePortalCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Collider>().excludeLayers += LayerMask.GetMask("PortalSurface");
        }
    }
}
