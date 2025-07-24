using UnityEngine;
using TMPro;

public class GameManagerKhanh : MonoBehaviour
{
    public static GameManagerKhanh Instance;

    public int deathCount = 0;

    public TMP_Text scoreText;
    public GameObject winPanel;
    public TMP_Text finalScoreText;

    [Header("Audio")]
    public AudioClip deathSound;          // 👈 Âm thanh mỗi khi chết
    private AudioSource audioSource;

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

        audioSource = GetComponent<AudioSource>(); // 👈 Lấy AudioSource
    }

    private void OnEnable()
    {
        PlayerControllerKhanh.OnPlayerDeath += AddDeath;
    }

    private void OnDisable()
    {
        PlayerControllerKhanh.OnPlayerDeath -= AddDeath;
    }

    public void AddDeath()
    {
        deathCount++;

        if (scoreText != null)
        {
            scoreText.text = "Deaths: " + deathCount;
        }

        // 👇 Phát âm thanh chết
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
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
