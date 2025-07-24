using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class TrapLeft : MonoBehaviour
{
    private bool isActivated = false;
    private TilemapRenderer tileRenderer;
    private Rigidbody2D rb;
    private Vector3 startPosition;

    [Header("Trap Settings")]
    public float moveSpeed = 5f;                     // Tốc độ bay
    public Vector2 moveDirection = Vector2.left;    // Hướng bay trái
    public float resetDelay = 1.5f;                  // Delay reset trap

    private void Awake()
    {
        startPosition = transform.position;
        tileRenderer = GetComponent<TilemapRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (tileRenderer != null)
            tileRenderer.enabled = false;

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;
        }

        PlayerController.OnPlayerDeath += ResetTrap;
    }

    private void OnDestroy()
    {
        PlayerController.OnPlayerDeath -= ResetTrap;
    }

    public void ActivateTrap()
    {
        if (isActivated) return;

        isActivated = true;

        if (tileRenderer != null)
            tileRenderer.enabled = true;

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic; // Nếu không dùng physics force
            rb.linearVelocity = moveDirection.normalized * moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActivated) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Trap hit Player!");

            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Die(); // Gây chết cho Player
                StartCoroutine(ResetAfterDelay()); // Reset trap sau một khoảng delay
            }
        }
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(resetDelay);
        ResetTrap();
    }

    private void ResetTrap()
    {
        Debug.Log("ResetTrap called!");

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0f;
        }

        transform.position = startPosition;
        isActivated = false;

        if (tileRenderer != null)
            tileRenderer.enabled = false;
    }
}
