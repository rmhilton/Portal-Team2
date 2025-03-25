using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //Create a component where you want the movement to take place,
    //copy the position of it
    //paste it into these public point A corrdinates
    public Vector3 pointA;
    public Vector3 pointB;

    [SerializeField] private float speed = 1f;

    private Vector3 target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = pointB;
    }

    // Update is called once per frame
    void Update()
    {
        //move this object towards position
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        //check if its moved to target
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            target = target == pointA ? pointB : pointA;
        }
    }
}
