
using UnityEngine;

public class DieZoneUppp : MonoBehaviour
{
    //public float riseSpeed = 1f; // tốc độ di chuyển lên

    //private void Update()
    //{
    //    // Di chuyển die zone lên mỗi frame
    //    transform.position += Vector3.up * riseSpeed * Time.deltaTime;
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        Debug.Log("Player đã chạm vào DieZone!");

    //        PlayerControllerr player = collision.GetComponent<PlayerControllerr>();
    //        if (player != null)
    //        {
    //            player.Die();
    //        }
    //        else
    //        {
    //            Destroy(collision.gameObject);
    //        }
    //    }
    //}
    public float riseSpeed = 1f;

    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void OnEnable()
    {
        PlayerControllerr.OnPlayerDeath += ResetZone;
    }

    private void OnDisable()
    {
        PlayerControllerr.OnPlayerDeath -= ResetZone;
    }

    private void Update()
    {
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player đã chạm vào DieZone!");

            PlayerControllerr player = collision.GetComponent<PlayerControllerr>();
            if (player != null)
            {
                player.Die(); // Gọi Die(), các sự kiện reset khác cũng sẽ được gọi
            }
        }
    }

    private void ResetZone()
    {
        transform.position = initialPosition;
    }
}
