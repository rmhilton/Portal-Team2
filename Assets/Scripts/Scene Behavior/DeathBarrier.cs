using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBarrier : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
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

