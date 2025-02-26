using UnityEngine;

public class Turret : MonoBehaviour
{
    //firing settings
    [SerializeField] float fireRate;
    [SerializeField] float damage;
    [SerializeField] bool seesPlayer;
    [SerializeField] bool hostile;

    ParticleSystem gun;

    void Update()
    {
        PlayerLook();
    }


    /// <summary>
    /// Holds the logic for detecting the player.
    /// </summary>
    void PlayerLook()
    {

    }
}
