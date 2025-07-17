using UnityEngine;
using TMPro; // 👉 Thêm thư viện TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int deathCount = 0;

    public TMP_Text scoreText;        // ✅ TMP_Text thay cho Text
    public GameObject winPanel;
    public TMP_Text finalScoreText;   // ✅ TMP_Text thay cho Text

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        PlayerControllerr.OnPlayerDeath += AddDeath;
    }

    private void OnDisable()
    {
        PlayerControllerr.OnPlayerDeath -= AddDeath;
    }

    public void AddDeath()
    {
        deathCount++;
        if (scoreText != null)
        {
            scoreText.text = "Deaths: " + deathCount;
        }
    }

    public void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = "Total Deaths: " + deathCount;
        }

        Time.timeScale = 0f;
    }
}
