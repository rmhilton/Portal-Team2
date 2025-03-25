using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    //different portal types have different behaviors
    enum PlatformType
    {
        DoTimes,
        OnWhile,
        OnStand
    }
    [SerializeField] PlatformType platformType;

    //how many times to move if in DoTimes mode
    [SerializeField] int moveTimes;
    int timesMoved = 0;

    //bool to store whether platform is on
    [SerializeField] bool shouldDo;

    //bool to store if platform has been stood on
    [SerializeField] bool stoodOn;

    //List to store positions the portal will go to, excluding starting position
    [SerializeField] List<Transform> moveTargets;
    List<Vector3> moveTargetsPosition;
    int nextTargetIndex = 1;
    Vector3 nextTargetPosition;

    //platform speed
    [SerializeField] float speed;

    private void Start()
    {
        moveTargetsPosition = new List<Vector3>();
        moveTargetsPosition.Insert(0, transform.position);
        foreach (Transform t in moveTargets)
        {
            moveTargetsPosition.Add(t.position);
        }
    }
    private void FixedUpdate()
    {
        switch (platformType)
        {
            case PlatformType.DoTimes:
                if (shouldDo)
                {
                    nextTargetPosition = moveTargetsPosition[nextTargetIndex];
                    MovePlatform();
                }
                break;
            case PlatformType.OnWhile:
                if (shouldDo)
                {
                    nextTargetPosition = moveTargetsPosition[1];
                    MovePlatform();
                }
                else
                {
                    nextTargetPosition = moveTargetsPosition[0];
                    MovePlatform();
                }
                break;
            case PlatformType.OnStand:
                if (stoodOn)
                {
                    nextTargetPosition = moveTargetsPosition[1];
                    MovePlatform();
                }
                else
                {
                    nextTargetPosition = moveTargetsPosition[0];
                    MovePlatform();
                }
                break;
        }
    }
    void MovePlatform()
    {
        if(Vector3.Distance(transform.position, nextTargetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextTargetPosition, speed*Time.fixedDeltaTime);
        }
        else if (platformType == PlatformType.DoTimes)
        {
            if (moveTimes > timesMoved)
            {
                timesMoved++;
                if (nextTargetIndex > moveTargets.Count - 1)
                {
                    nextTargetIndex = 0;
                }
                else
                {
                    nextTargetIndex++;
                }
            }
            else
            {
                shouldDo = false;
                timesMoved = 0;
            }
        }
    }
    public void ActivatePlatform()
    {
        shouldDo = true;
    }
    public void DeactivatePlatform()
    {
        shouldDo = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            other.transform.parent = transform;
            stoodOn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
            stoodOn = false;
        }
    }
}
