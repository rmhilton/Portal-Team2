using UnityEngine;

public class Cube : InteractableObject
{
    private bool grabbed = false;
    private Transform goalPos;
    private Rigidbody rb;

    [SerializeField] private float maxMoveSpeed = 5.0f;
    [SerializeField] private float maxCatchUpDist = 1.2f;

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
    public override void Interact()
    {
        if(!grabbed)
        {
            grabbed = true;
            rb.useGravity = false;
        }
        else
        {
            grabbed = false;
            rb.useGravity = true;
            goalPos = null;
        }
    }

    public void SetGoal(Transform goal)
    {
        goalPos = goal;
    }

    private void FixedUpdate()
    {
        if(grabbed && goalPos)
        {
            //attempt to reach goal position
        }
    }
}
