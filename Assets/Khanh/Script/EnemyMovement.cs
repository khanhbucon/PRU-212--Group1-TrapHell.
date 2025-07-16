using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float moveDistance = 5f;

    private Rigidbody2D myRigidbody;
    private Vector2 startingPosition;
    private bool movingRight = true;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    void Update()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        float currentX = transform.position.x;
        float leftLimit = startingPosition.x - moveDistance;
        float rightLimit = startingPosition.x + moveDistance;

        if (movingRight && currentX >= rightLimit)
        {
            movingRight = false;
            FlipEnemyFacing();
        }
        else if (!movingRight && currentX <= leftLimit)
        {
            movingRight = true;
            FlipEnemyFacing();
        }

        float direction = movingRight ? 1f : -1f;
        myRigidbody.linearVelocity = new Vector2(direction * moveSpeed, myRigidbody.linearVelocity.y);
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    // 🛑 Khi player chạm enemy → gọi hàm chết
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Giả sử player có script tên là "PlayerHealth" và có hàm Die()
            other.GetComponent<PlayerControllerr>()?.Die();
        }
    }
}
