﻿//using UnityEngine;

//public class TrapController : MonoBehaviour
//{
//    public enum TrapType { DropDown, FlyLeft, FlyRight, None }
//    public TrapType trapType = TrapType.None;
//    public float trapSpeed = 5f;
//    public Transform player;       // Gán Player từ Inspector
//    public float activationRange = 0.2f; // Khoảng cách để kích hoạt
//    private bool isActivated = false;
//    [SerializeField] private GameObject trap;
//    private Rigidbody2D rb;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        if (trap == null)
//            trap = transform.Find("Trap").gameObject;
//        rb = trap.GetComponent<Rigidbody2D>();
//        trap.SetActive(false);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (!isActivated && Vector2.Distance(trap.transform.position, player.position) < activationRange)
//        {
//            isActivated = true;
//            if (trap != null)
//                trap.SetActive(true);
//            ActiveTrap();

//        }
//    }
//    void ActiveTrap()
//    {
//        switch (trapType)
//        {
//            case TrapType.DropDown:
//                rb.linearVelocity = Vector2.down * trapSpeed;
//                break;
//            case TrapType.FlyLeft:
//                rb.linearVelocity = Vector2.left * trapSpeed;
//                break;
//            case TrapType.FlyRight:
//                rb.linearVelocity = Vector2.right * trapSpeed;
//                break;
//            case TrapType.None:
//                break;
//        }
//    }
//}
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public enum TrapType { DropDown, FlyLeft, FlyRight, None }
    public TrapType trapType = TrapType.None;
    public float trapSpeed = 5f;
    public Transform player;
    public float activationRange = 0.5f;

    private bool isActivated = false;
    [SerializeField] private GameObject trap;
    private SpriteRenderer sprite;
    private Collider2D trapCollider;

    void Start()
    {
        if (trap == null)
            trap = transform.Find("Trap")?.gameObject;

        if (trap == null)
        {
            Debug.LogError("Không tìm thấy Trap! Đảm bảo có GameObject tên 'Trap' là con của TrapUp");
            return;
        }

        sprite = trap.GetComponent<SpriteRenderer>();
        trapCollider = trap.GetComponent<Collider2D>();

        if (sprite != null) sprite.enabled = false;
        if (trapCollider != null) trapCollider.enabled = false;
    }

    void Update()
    {
        if (isActivated || trap == null || player == null)
            return;

        float distance = Vector2.Distance(trap.transform.position, player.position);

        if (distance < activationRange)
        {
            Debug.Log(" Player gần trap, kích hoạt bẫy!");
            isActivated = true;

            if (sprite != null) sprite.enabled = true;
            if (trapCollider != null) trapCollider.enabled = true;

            ActiveTrap();
        }
    }

    void ActiveTrap()
    {
        Vector3 direction = Vector3.zero;
        switch (trapType)
        {
            case TrapType.DropDown:
                direction = Vector3.down;
                break;
            case TrapType.FlyLeft:
                direction = Vector3.left;
                break;
            case TrapType.FlyRight:
                direction = Vector3.right;
                break;
        }

        StartCoroutine(MoveTrap(direction));
    }

    System.Collections.IEnumerator MoveTrap(Vector3 direction)
    {
        while (true)
        {
            trap.transform.position += direction * trapSpeed * Time.deltaTime;
            yield return null;
        }
    }
}




