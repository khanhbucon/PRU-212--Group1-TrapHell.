using UnityEngine;

public class HorizontalMover : MonoBehaviour
{
    public float moveSpeed = 2f;               // Tốc độ di chuyển
    public Transform leftLimit;               // Giới hạn trái
    public Transform rightLimit;              // Giới hạn phải
    public bool moveRightFirst = true;        // Hướng ban đầu

    private bool movingRight;

    private void Start()
    {
        movingRight = moveRightFirst;
    }

    private void Update()
    {
        // Chọn target là trái hoặc phải
        Vector3 target = movingRight ? rightLimit.position : leftLimit.position;

        // Di chuyển từng bước đến target, chỉ thay đổi trục X
        Vector3 newPosition = Vector3.MoveTowards(
            transform.position,
            new Vector3(target.x, transform.position.y, transform.position.z),
            moveSpeed * Time.deltaTime
        );

        transform.position = newPosition;

        // Đảo chiều nếu đã đến giới hạn
        if (Mathf.Abs(transform.position.x - target.x) < 0.01f)
        {
            movingRight = !movingRight;
        }

        Debug.Log("Target X = " + target.x + ", Current X = " + transform.position.x);
    }
}
