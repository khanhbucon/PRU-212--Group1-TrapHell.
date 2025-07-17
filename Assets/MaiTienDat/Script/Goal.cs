using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Goal : MonoBehaviour
{
    public GameObject winPanel;
    public TMP_Text finalScoreText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (winPanel != null)
                winPanel.SetActive(true);

            if (finalScoreText != null)
                finalScoreText.text = "Total Deaths: " + GameManager.Instance.deathCount;

            Time.timeScale = 0f;
        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
