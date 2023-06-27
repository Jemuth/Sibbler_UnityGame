using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLoad : MonoBehaviour
{
    public float fadeDuration = 10f;  // Duration of the fade in seconds

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(FadeOut());
    }

    private System.Collections.IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color originalColor = image.color;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            image.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the image is completely transparent at the end
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}
