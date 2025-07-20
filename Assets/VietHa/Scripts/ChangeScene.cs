using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // Nhập tên scene muốn chuyển trong Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player đã chạm checkpoint! Đang chuyển scene...");
            SceneManager.LoadScene(sceneToLoad);
        }
    }

}
