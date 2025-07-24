using UnityEngine;

public class TrapTriggerLeft : MonoBehaviour
{
    public GameObject trap;        // Gán object bẫy
    public float force = 10f;      // Dùng nếu muốn đẩy bằng lực (ít dùng)

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated && other.CompareTag("Player"))
        {
            activated = true;

            // Nếu trap dùng TrapLeft (bay theo hướng), gọi ActivateTrap
            var trapLeft = trap.GetComponent<TrapLeft>();
            if (trapLeft != null)
            {
                trapLeft.ActivateTrap();
                return;
            }

            // Nếu trap dùng Trap (rơi xuống), gọi ActivateTrap
            var trapFalling = trap.GetComponent<Trap>();
            if (trapFalling != null)
            {
                trapFalling.ActivateTrap();
                return;
            }

            // Nếu muốn dùng AddForce tới target (không nên nếu trap đã có logic riêng)
            Rigidbody2D rb = trap.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;

                // Ví dụ: đẩy sang trái
                Vector2 direction = Vector2.left; 
                rb.linearVelocity = direction.normalized * force;
            }
        }
    }
}
