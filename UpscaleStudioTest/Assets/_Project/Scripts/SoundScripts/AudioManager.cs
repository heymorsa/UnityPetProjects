using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void PlaySound(AudioClip clip)
    {
        if (instance != null && clip != null)
        {
            instance.audioSource.PlayOneShot(clip);
        }
    }

    public static void PlaySoundAtPosition(AudioClip clip, Vector3 position)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, position);
        }
    }
}