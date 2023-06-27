using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleLoad : MonoBehaviour
{
    public float fadeDuration = 2f;  // Duration of the fade in seconds
    public float initialDelay = 5f;  // Delay before starting the fade in seconds

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);  // Set initial transparency to 0
        StartCoroutine(StartFadeIn());
    }

    private IEnumerator StartFadeIn()
    {
        yield return new WaitForSeconds(initialDelay);
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color originalColor = image.color;

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            float alpha = EaseInOut(t);  // Use easing function instead of Mathf.Lerp
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            image.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the image is completely visible at the end
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }

    private float EaseInOut(float t)
    {
        // Apply an easing function here to control the transparency transition
        // Example: Use ease-in-out quadratic function for a slower transition
        return t < 0.5f ? 2f * t * t : 1f - Mathf.Pow(-2f * t + 2f, 2f) / 2f;
    }
}
