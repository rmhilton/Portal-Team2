using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    #region Firing Settings
    [SerializeField] float fireSpeed;
    [SerializeField] float damage;
    #endregion

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
    }
    private void Update()
    {
        if (isActive)
        {
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
            if(Physics.Raycast(gun.shape.position, player.transform.position, out hit))
            {
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
}
