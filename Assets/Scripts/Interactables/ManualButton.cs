using UnityEngine;

public class ManualButton : MonoBehaviour
{
    private bool pressed = false;

    [SerializeField] private float pressedTimer = 0; // this will make the button stay pressed for x time if not 0

    [SerializeField] private Button control;

    [SerializeField] private Animator animator;

    public void PressButton()
    {
        Press();
        Invoke(nameof(Unpress), pressedTimer);
    }

    private void Press()
    {
        pressed = true;
        control.buttonPressed?.Invoke(pressed);
        animator.SetTrigger("Press");
    }

    private void Unpress()
    {
        pressed = false;
        control.buttonPressed?.Invoke(pressed);
        animator.SetTrigger("Unpress");
    }
}
