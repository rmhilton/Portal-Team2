using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.Rendering;

public class Portal : MonoBehaviour
{
    [SerializeField] PortalData data;
    public Camera portalView;
    public PortalPair pair;
    public int pairIndex;
    public bool rendering;

    private void Start()
    {
        rendering = false;
        data = ScriptableObject.CreateInstance<PortalData>();
        data.init(this, pairIndex);
        if (pair.getPartner(this))
        {
            StartCoroutine(data.Activate(this));
        }
        else
        {
            data.Deactivate(this);
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
            Transform playerLook = other.transform.Find("Orientation");
            if (!other.GetComponent<PlayerManager>().teleported)
            {
                var relativeposition = transform.InverseTransformPoint(other.transform.position);
                relativeposition = Vector3.Scale(relativeposition, new Vector3(1, 1, 1));
                other.transform.position = pair.getPartner(this).transform.TransformPoint(relativeposition);
                
                var relativeRot = transform.InverseTransformDirection(playerLook.forward);
                relativeRot = Vector3.Scale(relativeRot, new Vector3(-1, 1, -1));
                playerLook.forward = pair.getPartner(this).transform.TransformDirection(relativeRot);

                other.GetComponent<PlayerManager>().teleported = this;

                //code by Raymend to maintain velocity
                Vector3 startVel = other.GetComponent<PlayerManager>().GetVelocity();
                Vector3 end_vel = pair.getPartner(this).transform.forward * startVel.magnitude;
                other.GetComponent<PlayerManager>().SetVelocity(end_vel);
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
