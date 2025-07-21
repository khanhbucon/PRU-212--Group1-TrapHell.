using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicAudioS;
    public AudioSource musicDieS;

    public AudioClip musicClip;
    public AudioClip musicDie;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (musicAudioS != null && musicClip != null)
        {
            musicAudioS.clip = musicClip;
            musicAudioS.loop = true;
            musicAudioS.Play();
        }
    }

    public void SetMusic(AudioClip newMusic)
    {
        if (newMusic == null || musicAudioS == null)
            return;

        if (musicAudioS.clip != newMusic)
        {
            musicAudioS.clip = newMusic;
            musicAudioS.loop = true;
            musicAudioS.Play();
        }
    }
    public void PlayDie()
    {
        if (musicDieS != null && musicDie != null)
        {
            musicDieS.clip = musicDie;
            musicDieS.Play(); // S? d?ng Play() ?? có th? Stop() sau
        }
    }

    public void StopDieSound()
    {
        if (musicDieS != null && musicDieS.isPlaying)
        {
            musicDieS.Stop();
        }
    }
}
