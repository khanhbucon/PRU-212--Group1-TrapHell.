using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.teleClip, 3f);
        }
    }
    [SerializeField] Transform targetLocation;
    public Transform TargetLocation()
    {
        return targetLocation;
    }
}
