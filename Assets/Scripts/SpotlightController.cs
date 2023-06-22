using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightController : MonoBehaviour
{
    private Light spotlight;
    private Color originalSpotlightColor;

    public Color SpotlightColor
    {
        get { return spotlight.color; }
        set { spotlight.color = value; }
    }
    private void Start()
    {
        spotlight = GetComponent<Light>();
        originalSpotlightColor = spotlight.color;
    }
    public void ResetSpotlightColor()
    {
        spotlight.color = originalSpotlightColor;
    }
}
