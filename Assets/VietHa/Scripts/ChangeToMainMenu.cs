using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToMainMenu : MonoBehaviour
{
    public void LoadSence()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
