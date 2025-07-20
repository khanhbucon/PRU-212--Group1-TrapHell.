using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip backgroundClip;

    void Start()
    {
        audioSource.clip = backgroundClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
