using UnityEngine;

//created by Raymend
public class CursorOnStart : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
