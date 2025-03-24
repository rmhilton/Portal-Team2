using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    private void OnParticleTrigger()
    {
        //RestartLevel(); //use this for instant death level reset
        Debug.Log("Player was shot");
    }
    void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.name);
    }
}
