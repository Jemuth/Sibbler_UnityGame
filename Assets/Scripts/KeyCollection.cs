using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyCollection : MonoBehaviour
{
    public int totalKeys = 5;  
    public TMP_Text keysLeftText; 
    public GameObject exitMessage; 
    [SerializeField] private Animator m_openDoor;
    [SerializeField] private AudioSource m_keySource;
    public AudioClip keyCollected;

    private int keysCollected = 0; 

    private void Start()
    {
        UpdateUI();
    }
    public void CollectKey(bool m_keyCollected)
    {
        if(m_keyCollected == true) 
        { 
        keysCollected++;
        m_keySource.PlayOneShot(keyCollected, 1F);
        UpdateUI();
        }
        if (keysCollected >= totalKeys)
        {
            
            exitMessage.SetActive(true);
            GameManager.instance.CheckAllKeys(true);
            m_openDoor.SetBool("isExit", true);
        } 
    }

    private void UpdateUI()
    {
        int keysLeft = totalKeys - keysCollected;
        keysLeftText.text = keysLeft.ToString();
    }
}
