using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip musicClip;

    private void Awake()
    {
        // Check if the MusicManager already exists
        MusicManager[] musicManagers = FindObjectsOfType<MusicManager>();
        if (musicManagers.Length > 1)
        {
            // Destroy this instance if there is already another MusicManager present
            Destroy(gameObject);
            return;
        }

        // Set this GameObject to not be destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);

        // Play the music clip if it is not already playing
        if (!audioSource.isPlaying)
        {
            audioSource.clip = musicClip;
            audioSource.Play();
        }
    }
}
