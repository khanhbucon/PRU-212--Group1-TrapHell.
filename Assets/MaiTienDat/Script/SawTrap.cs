using UnityEngine;

public class SawTrap : MonoBehaviour
{
    public Transform leftLimit;
    public Transform rightLimit;
    public float moveSpeed = 2f;
    private bool movingRight = true;

    void Update()
    {
        Vector3 target = movingRight ? rightLimit.position : leftLimit.position;

        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(target.x, transform.position.y, transform.position.z),
            moveSpeed * Time.deltaTime
        );

        if (Mathf.Abs(transform.position.x - target.x) < 0.01f)
        {
            movingRight = !movingRight;
        }
    }

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
