using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //set Player rigidbody to "continous", and "interpolate"
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    public Transform orientation;

    private float xRotate;
    private float yRotate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        yRotate = orientation.rotation.eulerAngles.y;
        
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        xRotate -= mouseY;
        yRotate += mouseX;

        xRotate = Mathf.Clamp(xRotate, -90f, 90f);

        //rotating orientation/cam

        transform.rotation = Quaternion.Euler(xRotate, yRotate, 0);
        orientation.rotation = Quaternion.Euler(0, yRotate, 0);
    }
}
