using UnityEngine;
using UnityEngine.Tilemaps;

public class ResettableGround : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;
    private TilemapCollider2D tilemapCollider;

    private void Awake()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
        tilemapCollider = GetComponent<TilemapCollider2D>();

        // 👉 Ẩn tilemap + tắt collider để không chặn player
        tilemapRenderer.enabled = false;
        if (tilemapCollider != null)
            tilemapCollider.enabled = false;
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerDeath += ResetGround;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= ResetGround;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ShowGround();
        }
    }

    private void ShowGround()
    {
        tilemapRenderer.enabled = true;
        if (tilemapCollider != null)
            tilemapCollider.enabled = true;
    }

    private void ResetGround()
    {
        tilemapRenderer.enabled = false;
        if (tilemapCollider != null)
            tilemapCollider.enabled = false;
    }
}
