using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip musicClip;
    private AudioSource musicSource;

    void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.playOnAwake = true;
        musicSource.volume = 0.5f;
        musicSource.Play();
    }
}
