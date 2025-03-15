using UnityEngine;

/* Class Created By Raymend */

public class GrabbableObject : InteractableObject
{
    private bool grabbed = false;
    private Transform goalPos;
    private Rigidbody rb;

    //[SerializeField] private float maxMoveSpeed = 5.0f;
    //[SerializeField] private float maxCatchUpDist = 1.2f;
    [SerializeField] private float pickupForce = 150f;
    [SerializeField] private float releaseForce = 10f;
    private Vector3 prevPos;

    [SerializeField] private LayerMask playerMask;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(rb == null )
        {
            Debug.Log("There is no rigidbody attached to this cube!");
            this.enabled = false;
        }
    }

    // called when the player interacts with the cube
    public void Interact(bool isGrabbed = false, Transform holdGoal = null)
    {
        if(isGrabbed)
        {
            //pick up obj
            grabbed = true;
            rb.useGravity = false;
            rb.linearDamping = 10f;
            rb.freezeRotation = true;
            if(holdGoal)
            {
                goalPos = holdGoal;
                transform.parent = goalPos;
            }
            rb.excludeLayers += playerMask;
        }
        else
        {
            //drop object
            grabbed = false;
            rb.useGravity = true;
            rb.linearDamping = 0f;
            rb.freezeRotation = false;
            goalPos = null;
            transform.parent = null;
            //Vector3 dropDir = (transform.position - prevPos);
            //rb.AddForce(dropDir * releaseForce, ForceMode.Impulse);
            rb.excludeLayers -= playerMask;
        }
    }

    private void FixedUpdate()
    {
        if(grabbed && goalPos)
        {
            //move to goal pos
            if (Vector3.Distance(transform.position, goalPos.position) > 0.1f)
            {
                Vector3 moveDir = (goalPos.position - transform.position).normalized;
                rb.AddForce(moveDir * pickupForce);
                prevPos = transform.position;
            }
        }
    }
}
