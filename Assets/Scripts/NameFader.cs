using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameFader : MonoBehaviour
{
    public Image image;
    public float fadeDuration = 1.0f;
    private float timer = 0.0f;
    private bool isFading = false;

    private void Start()
    {
        // Start the timer
        timer = 0.0f;
        isFading = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1.0f && !isFading)
        {
            StartCoroutine(FadeOutImage());
        }
    }

    private IEnumerator FadeOutImage()
    {
        isFading = true;
        Color startColor = image.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0);

        float increment = Time.deltaTime / fadeDuration;

        while (image.color.a > 0)
        {
            image.color = Color.Lerp(image.color, targetColor, increment);

            yield return null;
        }
        image.gameObject.SetActive(false);
    }
}
