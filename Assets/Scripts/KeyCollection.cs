using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyCollection : MonoBehaviour
{
    public int totalKeys = 5;  // Total number of keys in the level
    public TMP_Text keysLeftText; // UI text element to display the number of keys left
    public GameObject exitMessage; // UI message element to display the exit message

    private int keysCollected = 0; // Number of keys collected

    // Called when a player collects a key
    private void Start()
    {
        UpdateUI();
    }
    public void CollectKey(bool m_keyCollected)
    {
        if(m_keyCollected == true) 
        { 
        keysCollected++;
        UpdateUI();
        }
        if (keysCollected >= totalKeys)
        {
            // All keys collected, show exit message
            exitMessage.SetActive(true);
            GameManager.instance.CheckAllKeys(true);
        } 
    }

    // Updates the UI to display the number of keys left
    private void UpdateUI()
    {
        int keysLeft = totalKeys - keysCollected;
        keysLeftText.text = "Keys Left: " + keysLeft.ToString();
    }
}
