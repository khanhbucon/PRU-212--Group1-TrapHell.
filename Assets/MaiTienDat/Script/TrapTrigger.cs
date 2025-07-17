using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public GameObject trap;        // Bẫy (phải gán prefab hoặc object trong scene)
    public Transform target;       // Vị trí đích
    public float force = 10f;      // Lực đẩy

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated && other.CompareTag("Player"))
        {
            activated = true;

            // Gọi hàm hiện hình ảnh bẫy
            Trap trapScript = trap.GetComponent<Trap>();
            if (trapScript != null)
            {
                trapScript.ActivateTrap(); // Hiện bẫy
            }

            // Đẩy bẫy về phía target
            Rigidbody2D rb = trap.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;

                Vector2 direction = (target.position - trap.transform.position).normalized;
                rb.AddForce(direction * force, ForceMode2D.Impulse);
            }
        }
    }
}

