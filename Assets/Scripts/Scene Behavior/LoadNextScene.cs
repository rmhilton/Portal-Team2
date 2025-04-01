using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
//making this script to have an on trigger enter that moves player to next scene in build.
public class LoadNextScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            NextSceneLoad();
        }
    }

   public  void NextSceneLoad()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
            ResumeGame();
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        
    }
    private void ResumeGame()
    {
        
        Time.timeScale = 1f;

       // Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
  
    }
}
