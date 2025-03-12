using UnityEngine;

/////////////////////////////////////////////
//Assignment/Lab/Project: Portal
//Name: Team 2
//Section: SGD285.4173
//Instructor: Ven Lewis
//Date: 3/11/25
/////////////////////////////////////////////
public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    public GameObject pauseMenu;
    private bool isPaused;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Pause Menu instance already set!");
            this.enabled = false;
            return;
        }
        
        pauseMenu.SetActive(false);
    }
    
    public void PauseButtonPressed()
    {
        if(isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        print("Game paused");
    }

    private void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        print("Resume Game");
    }
}
