using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform cameraPos;


    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = cameraPos.position;
    }
}
