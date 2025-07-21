using UnityEngine;

public class SlipperyGround : MonoBehaviour
{
    public GameObject targetObject;
    public float slideForce = 50f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == targetObject)
        {
            Rigidbody2D rb = targetObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(Vector2.left * slideForce, ForceMode2D.Force);
                Debug.Log("Player đang bị trượt sang trái!");
            }
        }
    }
}
