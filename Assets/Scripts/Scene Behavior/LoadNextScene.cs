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
        SceneManager.LoadScene(nextSceneIndex);
    }
}
