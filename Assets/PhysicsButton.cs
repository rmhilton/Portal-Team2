using UnityEngine;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float returnForce = 10f;
    [SerializeField] private Transform topLimit;
    [SerializeField] private Transform bottomLimit;
    [SerializeField] private float threshHold = 0.5f;
    private float upperLowerDiff;

    private bool isPressed = false;

    private Rigidbody rb;

    [SerializeField] private Button control;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        upperLowerDiff = topLimit.localPosition.y - bottomLimit.localPosition.y;
    }

    private void Update()
    {
        //lock button to highest limit
        if(transform.localPosition.y >= topLimit.localPosition.y)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, topLimit.localPosition.y, transform.localPosition.z);
        }
        //or return up
        else
        {
            rb.AddRelativeForce(Vector3.up * returnForce * Time.fixedDeltaTime, ForceMode.Force);
        }

        //lock button to lowest limit
        if (transform.localPosition.y <= bottomLimit.localPosition.y)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, bottomLimit.localPosition.y, transform.localPosition.z);
        }

        //check to see if button is pressed
        if(Vector3.Distance(transform.localPosition, bottomLimit.localPosition) < upperLowerDiff * threshHold)
        {
            if(!isPressed)
            {
                isPressed = true;
                control.buttonPressed?.Invoke(isPressed);
            }
        }
        else
        {
            if(isPressed)
            {
                isPressed = false;
                control.buttonPressed?.Invoke(isPressed);
            }
        }
    }
}
