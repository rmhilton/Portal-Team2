using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //fire settings are in particle system

    [SerializeField] ParticleSystem gun;

    GameObject player;

    #region Activation
    [SerializeField] bool isActive;
    [SerializeField] bool seesPlayer;
    Animator anim;
    #endregion

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        gun.trigger.AddCollider(player.transform);
    }
    private void Update()
    {
        if (isActive)
        {
            isActive = CheckFallOver();
            seesPlayer = LookForPlayer();
        }
    }
    /// <summary>
    /// Handles the Turret detecting the player
    /// </summary>
    bool LookForPlayer()
    {
        if(Vector3.Distance(player.transform.position, transform.position)<gun.shape.length && Vector3.Angle(player.transform.position-transform.position, transform.forward) <= gun.shape.angle)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, player.transform.position-transform.position, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.collider.gameObject);
                Debug.DrawLine(transform.position, hit.point,Color.yellow);
                if (hit.collider.CompareTag("Player"))
                {
                        anim.SetBool("Hostile", true);
                        return true;
                }
            }
        }
        anim.SetBool("Hostile", false);
        return false;
    }

    bool CheckFallOver()
    {
        if(Mathf.Abs(transform.localRotation.eulerAngles.x) > 20 || Mathf.Abs(transform.localRotation.eulerAngles.z) > 20)
        {
            anim.SetTrigger("fall");
            //play sad sound effect, maybe explosion particle
            return false;
        }
        return true;
    }
}
