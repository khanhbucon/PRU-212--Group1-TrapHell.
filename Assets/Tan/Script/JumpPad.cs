using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float bounceForce = 15f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.jumpClip, 0.7f);
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);
            }

            // Chạy animation khi chạm
            if (animator != null)
            {
                animator.SetTrigger("Activate");
            }
        }
    }
}
