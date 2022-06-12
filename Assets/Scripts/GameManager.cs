using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool isPaused = false;
    public void Play()
    {
        SceneManager.LoadScene("Level01");
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        isPaused = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && IsNotInMainMenu())
        {
            if(isPaused)
            {
                Resume();
                return;
            }
            Pause();
        }
    }

    bool IsNotInMainMenu()
    {
        return SceneManager.GetActiveScene().name != "MainMenu";
    }
}
