﻿using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerHa : MonoBehaviour
{
    public GameObject mainMenuButtonUI;
    private Vector3 deathPosition; // Vị trí người chơi chết
    public static int deathCount = 0;
    public static event System.Action OnPlayerDeath;

    public TextMeshProUGUI countDeathText;
    public GameObject retryTextUI; // UI “Press any key to continue” (ẩn lúc đầu)

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;
    private bool isGrounded = false;
    private bool isControlInverted = false;
    private bool isDead = false;
    private bool isWaitingToRestart = false;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float maxJumpVelocity = 12f;

    [Header("Checkpoint Settings")]
    public Vector2 checkpointPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        checkpointPosition = transform.position;

        if (retryTextUI != null)
            retryTextUI.SetActive(false);

        if (mainMenuButtonUI != null)
            mainMenuButtonUI.SetActive(false);

    }

    void Start()
    {
        countDeathText.text = "Death Count: " + deathCount;
    }

    void Update()
    {
        if (isWaitingToRestart && Keyboard.current.rKey.wasPressedThisFrame)
        {
            ContinueAfterDeath();
            return;
        }

        if (isDead || isWaitingToRestart) return;

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        if (rb.linearVelocity.y > maxJumpVelocity)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxJumpVelocity);

        // Flip hướng
        if (moveInput.x > 0.01f)
            transform.localScale = new Vector3(4, 5, 1);
        else if (moveInput.x < -0.01f)
            transform.localScale = new Vector3(-4, 5, 1);

        UpdateAnimation();
    }

    public void OnMove(InputValue value)
    {
        if (isDead || isWaitingToRestart) return;

        Vector2 input = value.Get<Vector2>();
        if (isControlInverted) input.x = -input.x;
        moveInput = input;
    }

    public void OnJump(InputValue value)
    {
        if (isDead || isWaitingToRestart) return;

        if (value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
            Die();

        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trap"))
            Die();

        if (other.CompareTag("InvertZone"))
            SetControlInverted(true);

        if (other.CompareTag("ResetZone"))
            SetControlInverted(false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    public void Die()
    {
        if (isDead || isWaitingToRestart) return;

        isDead = true;
        deathCount++;
        countDeathText.text = "Count: " + deathCount;
        AudioManager.Instance.PlayDie();


        // Ghi lại vị trí chết
        deathPosition = transform.position;

        // Tạm thời dừng nhân vật
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<SpriteRenderer>().enabled = false; // ẩn player
        animator.enabled = false; // tắt animation
        Time.timeScale = 0f; // dừng toàn bộ thời gian game

        // Hiện UI chờ
        isWaitingToRestart = true;
        if (retryTextUI != null)
            retryTextUI.SetActive(true);
        if (mainMenuButtonUI != null)
            mainMenuButtonUI.SetActive(true);
    }

    private void RestartAfterDeath()
    {
        isDead = false;
        isWaitingToRestart = false;
        if (retryTextUI != null)
            retryTextUI.SetActive(false);
        if (mainMenuButtonUI != null)
            mainMenuButtonUI.SetActive(false);
    }

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        checkpointPosition = newCheckpoint;
    }

    public void SetControlInverted(bool inverted)
    {
        isControlInverted = inverted;
    }

    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsJumping", isJumping);
    }
    private void ContinueAfterDeath()
    {
        Debug.Log("Press R → Hồi sinh");
        AudioManager.Instance.StopDieSound();
        // Reset trạng thái
        isDead = false;
        isWaitingToRestart = false;

        OnPlayerDeath?.Invoke(); // Gửi sự kiện reset

        GetComponent<SpriteRenderer>().enabled = true;
        animator.enabled = true;

        // Reset vị trí và Rigidbody
        transform.position = checkpointPosition;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = Vector2.zero;

        if (retryTextUI != null)
            retryTextUI.SetActive(false);

        if (mainMenuButtonUI != null)
            mainMenuButtonUI.SetActive(false);

        Time.timeScale = 1f; // tiếp tục game
    }
}
