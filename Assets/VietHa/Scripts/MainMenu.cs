using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadSence()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 2 - Ha");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        // Trong Editor, dừng play mode
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Trong build game, quit thật
            Application.Quit();
#endif
    }

    public void LoadScence()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
