
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //[SerializeField] private string sceneToLoad;
    //[SerializeField] private Transform newPosition; // Vị trí checkpoint sẽ chuyển tới
    //[SerializeField] private GameObject platformToEnable; // Platform sẽ bật sau lần đầu chạm

    //private bool hasMoved = false;

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        if (!hasMoved)
    //        {
    //            Debug.Log("Player chạm lần đầu → Di chuyển checkpoint & bật platform");
    //            MoveCheckpoint();
    //            EnablePlatform();
    //            hasMoved = true;
    //        }
    //        else
    //        {
    //            Debug.Log("Player chạm lần hai → Chuyển scene");
    //            SceneManager.LoadScene(sceneToLoad);
    //        }
    //    }
    //}

    //void MoveCheckpoint()
    //{
    //    if (newPosition != null)
    //        transform.position = newPosition.position;
    //    else
    //        Debug.LogWarning("Chưa gán vị trí mới cho checkpoint");
    //}

    //void EnablePlatform()
    //{
    //    if (platformToEnable != null)
    //        platformToEnable.SetActive(true);
    //    else
    //        Debug.LogWarning("Chưa gán platform để bật!");
    //}
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Transform newPosition; // Checkpoint sẽ chuyển tới
    [SerializeField] private GameObject platformToEnable; // Platform bật lên sau chạm

    private bool hasMoved = false;
    private Vector3 initialPosition;
    private bool platformInitialActiveState;

    private void Awake()
    {
        initialPosition = transform.position;

        if (platformToEnable != null)
            platformInitialActiveState = platformToEnable.activeSelf;
    }

    private void OnEnable()
    {
        PlayerControllerr.OnPlayerDeath += ResetChangeScene;
    }

    private void OnDisable()
    {
        PlayerControllerr.OnPlayerDeath -= ResetChangeScene;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasMoved)
            {
                Debug.Log("Player chạm lần đầu → Di chuyển checkpoint & bật platform");
                MoveCheckpoint();
                EnablePlatform();
                hasMoved = true;
            }
            else
            {
                Debug.Log("Player chạm lần hai → Chuyển scene");
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    void MoveCheckpoint()
    {
        if (newPosition != null)
            transform.position = newPosition.position;
        else
            Debug.LogWarning("⚠️ Chưa gán vị trí mới cho checkpoint");
    }

    void EnablePlatform()
    {
        if (platformToEnable != null)
            platformToEnable.SetActive(true);
        else
            Debug.LogWarning("⚠️ Chưa gán platform để bật!");
    }

    void ResetChangeScene()
    {
        hasMoved = false;
        transform.position = initialPosition;

        if (platformToEnable != null)
            platformToEnable.SetActive(platformInitialActiveState);

        Debug.Log("🔁 ChangeScene reset thành trạng thái ban đầu");
    }

}
