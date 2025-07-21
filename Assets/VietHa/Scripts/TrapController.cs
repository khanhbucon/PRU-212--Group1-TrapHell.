
using System.Collections;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public enum TrapType { DropDown, FlyLeft, FlyRight, None }
    public TrapType trapType = TrapType.None;

    public float trapSpeed = 5f;
    public Transform player;
    public float activationRange = 0.5f;

    [SerializeField] private GameObject trap; // GameObject bẫy
    private SpriteRenderer sprite;
    private Collider2D trapCollider;

    private Vector3 trapInitialPosition;
    private bool isActivated = false;
    private Coroutine moveCoroutine;

    void Start()
    {
        if (trap == null)
            trap = transform.Find("Trap")?.gameObject;

        if (trap == null)
        {
            Debug.LogError("Không tìm thấy Trap!");
            return;
        }

        sprite = trap.GetComponent<SpriteRenderer>();
        trapCollider = trap.GetComponent<Collider2D>();
        trapInitialPosition = trap.transform.position;

        trap.SetActive(false); // Ẩn hoàn toàn trap lúc đầu
    }

    void Update()
    {
        if (isActivated || trap == null || player == null) return;

        float distance = Vector2.Distance(trap.transform.position, player.position);

        if (distance < activationRange)
        {
            isActivated = true;
            trap.SetActive(true); // Hiện trap
            ActivateTrap();
        }
    }

    void ActivateTrap()
    {
        Vector3 direction = Vector3.zero;
        switch (trapType)
        {
            case TrapType.DropDown: direction = Vector3.down; break;
            case TrapType.FlyLeft: direction = Vector3.left; break;
            case TrapType.FlyRight: direction = Vector3.right; break;
        }

        moveCoroutine = StartCoroutine(MoveTrap(direction));
    }

    IEnumerator MoveTrap(Vector3 direction)
    {
        while (true)
        {
            trap.transform.position += direction * trapSpeed * Time.deltaTime;
            yield return null;
        }
    }

    public void ResetTrap()
    {
        isActivated = false;

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }

        trap.transform.position = trapInitialPosition;
        trap.SetActive(false); // Ẩn hoàn toàn lại trap
    }
}




