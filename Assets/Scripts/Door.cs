using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void OpenDoor(bool open)
    {
        if(open)
        {
            animator.SetTrigger("Open");
        }
        else
        {
            animator.SetTrigger("Close");
        }
    }
}
