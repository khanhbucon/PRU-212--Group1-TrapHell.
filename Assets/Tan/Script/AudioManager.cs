using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioSource vfxAudioSource;

    public AudioClip musicClip;
    public AudioClip dieClip;
    public AudioClip teleClip;
    public AudioClip jumpClip;
    public AudioClip winClip;

    void Start()
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
    }
    public void PlaySFX(AudioClip sfxClip, float volume = 1f)
    {
        vfxAudioSource.clip = sfxClip;
        vfxAudioSource.PlayOneShot(sfxClip, volume);
    }
}
