using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static event Action OnPlayerDeath;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isDead = false;
    //private bool isGrounded = false;

    [Header("Ground Check")]
    public Transform groundCheck;             // Điểm kiểm tra dưới chân
    public float groundCheckRadius = 0.1f;    // Bán kính kiểm tra
    public LayerMask groundLayer;             // Layer mặt đất
    private bool isGrounded = false;

    private bool isControlInverted = false;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float maxJumpVelocity = 12f;

    [Header("Checkpoint Settings")]
    public Vector2 checkpointPosition;

    private Animator animator;

    private bool isBouncing = false;
    private float bounceCooldown = 0.2f; // 0.2s không bị override velocity
    private float bounceTimer = 0f;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        checkpointPosition = transform.position; // Lưu checkpoint ban đầu
    }

    private void Update()
    {
        // Áp dụng di chuyển
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        // Giới hạn vận tốc nhảy tối đa
        if (rb.linearVelocity.y > maxJumpVelocity)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxJumpVelocity);
        }

        // 👉 Flip nhân vật theo hướng di chuyển (nếu có di chuyển)
        if (moveInput.x > 0.01f)
        {
            transform.localScale = new Vector3(4, 5, 1); // Quay mặt phải
        }
        else if (moveInput.x < -0.01f)
        {
            transform.localScale = new Vector3(-4, 5, 1); // Quay mặt trái
        }
        UpdateAnimation();
         // Kiểm tra chạm đất (chỉ true nếu đứng trên mặt đất)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        // Đảo chiều nếu bị invert
        if (isControlInverted)
        {
            input.x = -input.x;
        }

        moveInput = input;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            // Luôn nhảy lên dù có đảo điều khiển
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }

        // if (collision.gameObject.CompareTag("Ground"))
        // {
        //     isGrounded = true;
        // }
        // Mặt đất hiện ra nếu cần
        if (collision.gameObject.CompareTag("Ground"))
        {
            SpriteRenderer sr = collision.gameObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trap"))
        {
            Die();
        }

        if (other.CompareTag("InvertZone"))
        {
            SetControlInverted(true);
            Debug.Log("Controls Inverted!");
        }

        if (other.CompareTag("ResetZone"))
        {
            SetControlInverted(false);
            Debug.Log("Controls Reset!");
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log("Player died!");
            OnPlayerDeath?.Invoke();
            transform.position = checkpointPosition;
            rb.linearVelocity = Vector2.zero;
            isDead = false;
        }
    }

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        checkpointPosition = newCheckpoint;
    }

    public void SetControlInverted(bool isInverted)
    {
        isControlInverted = isInverted;
    }

    private void UpdateAnimation()
    {
        bool isRunning = Math.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsJumping",isJumping);
    }
} 