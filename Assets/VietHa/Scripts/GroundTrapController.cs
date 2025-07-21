//using UnityEngine;
//using UnityEngine.Tilemaps;

//public class GroundTrapController : MonoBehaviour
//{
//    public enum GroundTrapType { Disappear, MoveLeft, MoveRight, MoveUp, None }
//    public GroundTrapType trapType = GroundTrapType.None;

//    public float trapSpeed = 5f;
//    private bool isActivated = false;
//    private Rigidbody2D rb;

//    [SerializeField] private Collider2D trapCollider; // ← gán collider nằm ở con

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();

//        if (trapType == GroundTrapType.Disappear && trapCollider != null)
//        {
//            trapCollider.enabled = true;
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (isActivated) return;

//        if (collision.CompareTag("Player"))
//        {
//            Debug.Log("Player đã vào trigger!");
//            isActivated = true;
//            ActivateGroundTrap();
//        }
//    }

//    void ActivateGroundTrap()
//    {
//        switch (trapType)
//        {
//            case GroundTrapType.Disappear:
//                if (trapCollider != null)
//                    //Destroy(trapCollider.gameObject);
//                    trapCollider.gameObject.GetComponent<TilemapRenderer>().enabled = false;
//                break;

//            case GroundTrapType.MoveLeft:
//                rb.bodyType = RigidbodyType2D.Kinematic;
//                rb.linearVelocity = Vector2.left * trapSpeed;
//                break;

//            case GroundTrapType.MoveRight:
//                rb.bodyType = RigidbodyType2D.Kinematic;
//                rb.linearVelocity = Vector2.right * trapSpeed;
//                break;

//            case GroundTrapType.MoveUp:
//                rb.bodyType = RigidbodyType2D.Kinematic;
//                rb.linearVelocity = Vector2.up * trapSpeed;
//                break;

//            case GroundTrapType.None:
//                break;
//        }
//    }
//    //public enum GroundTrapType { Disappear, MoveLeft, MoveRight, MoveUp, None }
//    //public GroundTrapType trapType = GroundTrapType.None;

//    //public float trapSpeed = 5f;
//    //private bool isActivated = false;

//    //private Rigidbody2D rb;
//    //private Vector3 initialPosition;
//    //private RigidbodyType2D initialBodyType;
//    //private TilemapRenderer tilemapRenderer;

//    //[SerializeField] private Collider2D trapCollider; // ← Gán collider nằm ở con

//    //private void Awake()
//    //{
//    //    rb = GetComponent<Rigidbody2D>();
//    //    tilemapRenderer = GetComponentInChildren<TilemapRenderer>();

//    //    // Ghi nhớ trạng thái ban đầu
//    //    initialPosition = transform.position;
//    //    if (rb != null) initialBodyType = rb.bodyType;
//    //}

//    //private void Start()
//    //{
//    //    if (trapType == GroundTrapType.Disappear && trapCollider != null)
//    //    {
//    //        trapCollider.enabled = true;
//    //    }

//    //    // Đăng ký sự kiện chết
//    //    PlayerControllerr.OnPlayerDeath += ResetTrap;
//    //}

//    //private void OnDestroy()
//    //{
//    //    // Gỡ đăng ký khi object bị huỷ
//    //    PlayerControllerr.OnPlayerDeath -= ResetTrap;
//    //}

//    //private void OnTriggerEnter2D(Collider2D collision)
//    //{
//    //    if (isActivated || !collision.CompareTag("Player")) return;

//    //    Debug.Log("Player đã vào trigger!");
//    //    isActivated = true;
//    //    ActivateGroundTrap();
//    //}

//    //private void ActivateGroundTrap()
//    //{
//    //    switch (trapType)
//    //    {
//    //        case GroundTrapType.Disappear:
//    //            if (tilemapRenderer != null)
//    //                tilemapRenderer.enabled = false;
//    //            if (trapCollider != null)
//    //                trapCollider.enabled = false;
//    //            break;

//    //        case GroundTrapType.MoveLeft:
//    //            rb.bodyType = RigidbodyType2D.Kinematic;
//    //            rb.linearVelocity = Vector2.left * trapSpeed;
//    //            break;

//    //        case GroundTrapType.MoveRight:
//    //            rb.bodyType = RigidbodyType2D.Kinematic;
//    //            rb.linearVelocity = Vector2.right * trapSpeed;
//    //            break;

//    //        case GroundTrapType.MoveUp:
//    //            rb.bodyType = RigidbodyType2D.Kinematic;
//    //            rb.linearVelocity = Vector2.up * trapSpeed;
//    //            break;

//    //        case GroundTrapType.None:
//    //            break;
//    //    }
//    //}

//    //private void ResetTrap()
//    //{
//    //    isActivated = false;

//    //    if (rb != null)
//    //    {
//    //        rb.bodyType = initialBodyType;
//    //        rb.linearVelocity = Vector2.zero;
//    //        transform.position = initialPosition;
//    //    }

//    //    if (trapType == GroundTrapType.Disappear)
//    //    {
//    //        if (tilemapRenderer != null)
//    //            tilemapRenderer.enabled = true;

//    //        if (trapCollider != null)
//    //            trapCollider.enabled = true;
//    //    }
//    //}
//}

using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundTrapController : MonoBehaviour
{
    public enum GroundTrapType { Disappear, MoveLeft, MoveRight, MoveUp, None }
    public GroundTrapType trapType = GroundTrapType.None;

    public float trapSpeed = 5f;

    private bool isActivated = false;
    private Vector3 initialPosition;
    private Rigidbody2D rb;
    private RigidbodyType2D initialBodyType;

    private TilemapRenderer tilemapRenderer;
    [SerializeField] private Collider2D trapCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tilemapRenderer = GetComponent<TilemapRenderer>();
        initialPosition = transform.position;

        if (rb != null)
        {
            initialBodyType = rb.bodyType;
        }
    }

    private void Start()
    {
        if (trapType == GroundTrapType.Disappear && trapCollider != null)
        {
            trapCollider.enabled = true;
        }

        PlayerControllerr.OnPlayerDeath += ResetTrap;
    }

    private void OnDestroy()
    {
        PlayerControllerr.OnPlayerDeath -= ResetTrap;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivated || !collision.CompareTag("Player")) return;

        isActivated = true;
        ActivateGroundTrap();
    }

    private void ActivateGroundTrap()
    {
        switch (trapType)
        {
            case GroundTrapType.Disappear:
                if (tilemapRenderer != null)
                    tilemapRenderer.enabled = false;
                if (trapCollider != null)
                    trapCollider.enabled = false;
                break;

            case GroundTrapType.MoveLeft:
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.linearVelocity = Vector2.left * trapSpeed;
                break;

            case GroundTrapType.MoveRight:
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.linearVelocity = Vector2.right * trapSpeed;
                break;

            case GroundTrapType.MoveUp:
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.linearVelocity = Vector2.up * trapSpeed;
                break;

            case GroundTrapType.None:
                break;
        }
    }

    private void ResetTrap()
    {
        isActivated = false;

        if (rb != null)
        {
            rb.bodyType = initialBodyType;
            rb.linearVelocity = Vector2.zero;
            transform.position = initialPosition;
        }

        if (trapType == GroundTrapType.Disappear)
        {
            if (tilemapRenderer != null)
                tilemapRenderer.enabled = true;
            if (trapCollider != null)
                trapCollider.enabled = true;
        }
    }
}
