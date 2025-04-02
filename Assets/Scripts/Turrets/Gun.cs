using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }
    private void OnParticleTrigger()
    {
        //RestartLevel(); //use this for instant death level reset
        Debug.Log("Player was shot");
        playerManager.health--;
        if(playerManager.health <= 0)
        {
            RestartLevel();
        }
    }
    void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.name);
    }
}
