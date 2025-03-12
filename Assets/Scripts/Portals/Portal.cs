using UnityEngine;
using UnityEngine.Rendering;

public class Portal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Transform playerView;
    public Camera portalView;
    [SerializeField] PortalPair pair;
    bool rendering;

    private void Start()
    {
        rendering = false;
        if (pair.getPartner(this) == null)
        {

        }
        else
        {
            playerView = GameObject.Find("CameraPosition").transform;
            GetComponent<MeshRenderer>().material.SetTexture("_PortalCam", pair.getPartner(this).portalView.targetTexture);
            rendering = true;
        }
        
    }
    private void Update()
    {
        if (rendering)
        {
            UpdateCamera();
        }
    }

    void UpdateCamera()
    {
        portalView.projectionMatrix = Camera.main.projectionMatrix;
        /*
        var offset = transform.InverseTransformPoint(Camera.main.transform.position);
        offset = Vector3.Scale(offset, new Vector3(1, 1, 1));
        pair.getPartner(this).portalView.transform.position = pair.getPartner(this).transform.TransformPoint(offset);
        */
        var offRotation = transform.InverseTransformDirection(Camera.main.transform.forward);
        offRotation = Vector3.Scale(offRotation, new Vector3(-1, 1, -1));
        pair.getPartner(this).portalView.transform.forward = pair.getPartner(this).transform.TransformDirection(offRotation); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<PlayerManager>().teleported)
            {
                var relativeposition = transform.InverseTransformPoint(other.transform.position);
                relativeposition = Vector3.Scale(relativeposition, new Vector3(1, 1, 1));
                other.transform.position = pair.getPartner(this).transform.TransformPoint(relativeposition);

                var relativeRot = transform.InverseTransformDirection(other.transform.forward);
                relativeRot = Vector3.Scale(relativeRot, new Vector3(-1, 1, -1));
                other.transform.forward = pair.getPartner(this).transform.TransformDirection(relativeRot);

                other.GetComponent<PlayerManager>().teleported = this;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerManager>().teleported != this)
            {
                other.GetComponent<PlayerManager>().teleported = null;
            }
        }
    }
}
