using Unity.VisualScripting;
using UnityEngine;

public class TelePort : MonoBehaviour
{
    [SerializeField] GameObject portal;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (portal != null) 
        {
            transform.position = portal.GetComponent<TeleportTrigger>().TargetLocation().position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TelePort"))
        {
            portal = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("TelePort"))
        {
            portal = null;
        }
    }
}
