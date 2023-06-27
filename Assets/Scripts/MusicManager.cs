using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneMusicData
{
    public string sceneName;
    public AudioClip musicClip;
}

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;
    public SceneMusicData[] sceneMusicData;

    private void Awake()
    {
        // Check if an instance of MusicManager already exists
        if (instance != null && instance != this)
        {
            // Destroy this instance if another instance is present
            Destroy(gameObject);
            return;
        }

        // Set this instance as the active MusicManager
        instance = this;

        // Set this GameObject to not be destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);

        // Get or add the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Play the music clip if it is not already playing
        if (!audioSource.isPlaying)
        {
            // Find the associated music clip for the current scene
            string sceneName = SceneManager.GetActiveScene().name;
            AudioClip musicClip = GetMusicClipForScene(sceneName);

            if (musicClip != null)
            {
                // Set the new music clip and play it
                audioSource.clip = musicClip;
                audioSource.Play();
            }
        }
    }

    private void OnEnable()
    {
        // Subscribe to the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the associated music clip for the current scene
        string sceneName = scene.name;
        AudioClip musicClip = GetMusicClipForScene(sceneName);

        if (musicClip != null)
        {
            // Check if the music clip is different from the currently playing clip
            if (audioSource.clip != musicClip)
            {
                // Set the new music clip and play it from the saved playback position
                audioSource.clip = musicClip;
                audioSource.time = GetSavedPlaybackPosition(sceneName);
                audioSource.Play();
            }
        }
        else
        {
            // Stop the music if no music clip is associated with the current scene
            audioSource.Stop();
        }
    }

    private AudioClip GetMusicClipForScene(string sceneName)
    {
        foreach (var data in sceneMusicData)
        {
            if (data.sceneName == sceneName)
                return data.musicClip;
        }

        return null;
    }

    private float GetSavedPlaybackPosition(string sceneName)
    {
        // Retrieve the saved playback position for the specified scene
        return PlayerPrefs.GetFloat(sceneName + "_PlaybackPosition", 0f);
    }

    private void SavePlaybackPosition(string sceneName, float playbackPosition)
    {
        // Save the playback position for the specified scene
        PlayerPrefs.SetFloat(sceneName + "_PlaybackPosition", playbackPosition);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        // Save the current playback position when the MusicManager is destroyed
        string sceneName = SceneManager.GetActiveScene().name;
        SavePlaybackPosition(sceneName, audioSource.time);
    }
}
