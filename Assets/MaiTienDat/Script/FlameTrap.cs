using UnityEngine;

public class FlameTrap : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControllerr player = other.GetComponent<PlayerControllerr>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}
