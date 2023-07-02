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
    private AudioSource[] audioSources;
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

        // Create audio sources for each track
        audioSources = new AudioSource[sceneMusicData.Length];

        for (int i = 0; i < sceneMusicData.Length; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].playOnAwake = false;
            audioSources[i].loop = true;
            audioSources[i].clip = sceneMusicData[i].musicClip;
            audioSources[i].volume = 0f;

            if (sceneMusicData[i].sceneName == SceneManager.GetActiveScene().name)
                audioSources[i].volume = 0.1f;
            else
                audioSources[i].Stop(); // Stop audio sources for non-active scenes
        }

        // Play the music clips
        PlayMusicClips();
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
        // Set the volume of the music tracks for the new scene
        string sceneName = scene.name;
        SetMusicVolume(sceneName);
    }

    private void SetMusicVolume(string sceneName)
    {
        // Set the volume of the music tracks based on the current scene
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (sceneMusicData[i].sceneName == sceneName)
                audioSources[i].volume = 0.1f;
            else
                audioSources[i].volume = 0f;
        }
    }

    private void PlayMusicClips()
    {
        // Play the music clips
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].Play();
        }
    }

    private void OnDestroy()
    {
        // Save the current playback positions when the MusicManager is destroyed
        for (int i = 0; i < audioSources.Length; i++)
        {
            string sceneName = sceneMusicData[i].sceneName;
            SavePlaybackPosition(sceneName, audioSources[i].time);
        }
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
}
