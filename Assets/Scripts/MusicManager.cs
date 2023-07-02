using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
    private string previousSceneName;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);

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
                audioSources[i].Stop(); 
        }

        PlayMusicClips();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        previousSceneName = scene.name;
        string sceneName = scene.name;
        SetMusicVolume(sceneName);
    }

    private void SetMusicVolume(string sceneName)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (sceneMusicData[i].sceneName == sceneName)
            {
                audioSources[i].volume = 0.1f;
                if (previousSceneName == "Scene1" && sceneName == "Scene2")
                    StartCoroutine(Crossfade(audioSources[i]));
                else
                    audioSources[i].Play();
            }
            else
            {
                audioSources[i].volume = 0f;
                audioSources[i].Stop();
            }
        }
    }

    private void PlayMusicClips()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].Play();
        }
    }

    private IEnumerator Crossfade(AudioSource audioSource)
    {
        const float crossfadeDuration = 2f;
        float timer = 0f;
        float startVolume = audioSource.volume;

        while (timer < crossfadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timer / crossfadeDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = 0.1f;
        audioSource.Play();
    }

    private void OnDestroy()
    {
        if (audioSources != null)
        {
            for (int i = 0; i < audioSources.Length; i++)
            {
                if (sceneMusicData != null && i < sceneMusicData.Length)
                {
                    string sceneName = sceneMusicData[i].sceneName;
                    SavePlaybackPosition(sceneName, audioSources[i].time);
                }
            }
        }
    }

    private float GetSavedPlaybackPosition(string sceneName)
    {
        return PlayerPrefs.GetFloat(sceneName + "_PlaybackPosition", 0f);
    }

    private void SavePlaybackPosition(string sceneName, float playbackPosition)
    {
        PlayerPrefs.SetFloat(sceneName + "_PlaybackPosition", playbackPosition);
        PlayerPrefs.Save();
    }
}
