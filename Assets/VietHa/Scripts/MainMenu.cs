using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void LoadSence()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelDevil_Khanh");
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void LoadScence()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
