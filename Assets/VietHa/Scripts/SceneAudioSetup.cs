using UnityEngine;

public class SceneAudioSetup : MonoBehaviour
{
    public AudioClip sceneMusic;

    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusic(sceneMusic);
        }
    }
}
