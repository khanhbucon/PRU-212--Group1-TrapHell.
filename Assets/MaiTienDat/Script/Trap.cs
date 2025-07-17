using UnityEngine;
using UnityEngine.Tilemaps;

public class Trap : MonoBehaviour
{
    private bool isActivated = false;
    private TilemapRenderer tileRenderer;
    private Rigidbody2D rb;
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;  // ← Lưu tại AWAKE
    }

    void Start()
    {
        tileRenderer = GetComponent<TilemapRenderer>();
        if (tileRenderer != null)
            tileRenderer.enabled = false;

        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;
        }

        PlayerControllerr.OnPlayerDeath += ResetTrap;
    }

    private void OnDestroy()
    {
        PlayerControllerr.OnPlayerDeath -= ResetTrap;
    }

    public void ActivateTrap()
    {
        if (isActivated) return;
        isActivated = true;

        if (tileRenderer != null)
            tileRenderer.enabled = true;

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActivated) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Bẫy trúng Player!");

            PlayerControllerr player = other.GetComponent<PlayerControllerr>();
            if (player != null)
            {
                player.Die();
            }
        }
    }

    private void ResetTrap()
    {
        Debug.Log("ResetTrap called!");
        Debug.Log("Trap reset to: " + startPosition);

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = 0f;
        }

        transform.position = startPosition;
        isActivated = false;

        if (tileRenderer != null)
            tileRenderer.enabled = false;
    }
}
