using UnityEngine;

/* Class Created By Raymend */

public class InteractController : MonoBehaviour
{
    public static InteractController instance;

    [SerializeField] private Transform holdArea;
    private GrabbableObject grabbedObj;

    [SerializeField] private float pickupRange = 5f;
    [SerializeField] private float maxDistToGoal = 6f;
    [SerializeField] private float throwForce = 10f;

    [SerializeField] private LayerMask interactLayer;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Error: Interact Controller already set!");
            this.enabled = false;
        }
    }
    private void FixedUpdate()
    {
        if(grabbedObj)
        {
            if (Vector3.Distance(grabbedObj.transform.position, holdArea.position) > maxDistToGoal)
            {
                DropObj();
            }
        }
    }

    public void AttemptInteract()
    {
        if (grabbedObj == null)
        {
            RaycastHit hit;
            //Debug.DrawLine(transform.position, transform.position + Camera.main.transform.forward * pickupRange, Color.magenta, 3f);
            if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, pickupRange, interactLayer))
            {
                Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.gameObject.CompareTag("Grabbable"))
                {
                    if (hit.transform.gameObject.GetComponent<GrabbableObject>())
                    {
                        PickUpObj(hit.transform.gameObject.GetComponent<GrabbableObject>());
                    }
                }
                else if (hit.transform.gameObject.CompareTag("Interactable"))
                {
                    if(hit.transform.gameObject.GetComponent<ManualButton>())
                    {
                        InteractWithObj(hit.transform.gameObject.GetComponent<ManualButton>());
                    }
                    
                }
            }
        }
        else
        {
            DropObj();
        }
    }

    public void AttemptAltInteract()
    {
        ThrowObj();
    }

    private void InteractWithObj(ManualButton btn)
    {
        btn.PressButton();
    }

    private void PickUpObj(GrabbableObject obj)
    {
        grabbedObj = obj;
        grabbedObj.Interact(true, holdArea);
    }

    public void DropObj()
    {
        if(grabbedObj)
        {
            grabbedObj.Interact(false);
            grabbedObj = null;
        }
    }

    private void ThrowObj()
    {
        if(grabbedObj)
        {
            grabbedObj.Interact(false);
            grabbedObj.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
            grabbedObj = null;
        }
    }
}
