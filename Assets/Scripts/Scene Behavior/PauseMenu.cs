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
    public GameObject pauseMenu;
    private bool isPaused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
  
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
       
        print("Game paused");
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        print("Resume Game");
    }
}
