using UnityEngine;

public class VerticalMover : MonoBehaviour
{
    public float moveSpeed = 2f;               // Tốc độ di chuyển
    public Transform upperLimit;              // Giới hạn trên
    public Transform lowerLimit;              // Giới hạn dưới
    public bool moveUpFirst = true;           // Hướng di chuyển ban đầu

    private bool movingUp;

    private void Start()
    {
        movingUp = moveUpFirst;
    }

    private void Update()
    {
        // Chọn target là upper hoặc lower limit
        Vector3 target = movingUp ? upperLimit.position : lowerLimit.position;

        // Di chuyển từng bước tới target, chỉ thay đổi Y
        Vector3 newPosition = Vector3.MoveTowards(
            transform.position,
            new Vector3(transform.position.x, target.y, transform.position.z),
            moveSpeed * Time.deltaTime
        );

        transform.position = newPosition;

        // Nếu khoảng cách theo trục Y rất nhỏ → coi như đã đến
        if (Mathf.Abs(transform.position.y - target.y) < 0.01f)
        {
            movingUp = !movingUp;
        }
        Debug.Log("Target Y = " + target.y + ", Current Y = " + transform.position.y);

    }
}
