using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Image fader;
    public float fadeDuration = 1f;

    private void Start()
    {
        fader.enabled = false;
    }
    private IEnumerator FadeOut()
    {
        fader.enabled = true;
        float elapsedTime = 0f;
        Color originalColor = fader.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            fader.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fader.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("NowLoading2");
    }
    public void LoadGameplayScene()
    {
        StartCoroutine(FadeOut());
    }
    public void ExitGame() //For quitting editor/build!
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
