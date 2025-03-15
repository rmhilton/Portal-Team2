using UnityEngine;

// Created By Raymend

public class HoldAreaManager : MonoBehaviour
{
    [SerializeField] private Transform holdAreaConst;
    private Portal hitPortal;

    private void PlayerTeleported()
    {
        this.gameObject.transform.position = holdAreaConst.transform.position;
        this.gameObject.transform.rotation = holdAreaConst.transform.rotation;
    }
}
