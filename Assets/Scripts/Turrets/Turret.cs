using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    #region Firing Settings
    [SerializeField] float fireSpeed;
    [SerializeField] float damage;
    #endregion

    ParticleSystem gun;

    GameObject player;

    #region Activation
    [SerializeField] bool isActive;
    [SerializeField] bool seesPlayer;
    Animator anim;
    #endregion

    private void Start()
    {
        
    }
    private void Update()
    {
        if (isActive)
        {
            LookForPlayer();
        }
    }
    /// <summary>
    /// Handles the Turret detecting the player
    /// </summary>
    void LookForPlayer()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,player.transform.position, out hit, 50f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (!seesPlayer)
                {
                    seesPlayer = true;
                    StartCoroutine(FireGun());
                }
                
            }
            else
            {
                seesPlayer = false;
            }
        }
    }
    /// <summary>
    /// Routine for turret fire.
    /// </summary>
    IEnumerator FireGun()
    {
        anim.SetTrigger("Hostile");
        yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
        while (seesPlayer)
        {
            yield return new WaitForSeconds(1 / fireSpeed);
            //subtract player health
            Debug.Log("hit!");
        }
    }
}
