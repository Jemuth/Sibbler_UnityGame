using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyCollection : MonoBehaviour
{
    public int totalKeys = 5;  
    public TMP_Text keysLeftText; 
    public GameObject exitMessage; 
    [SerializeField] private Animator m_openDoor;

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
        keysLeftText.text = "Keys Left: " + keysLeft.ToString();
    }
}
