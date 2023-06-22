using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool mustRestart;
    [SerializeField] private GameObject restartUI;
    private void Start()
    {
        restartUI.SetActive(false);
    }
    public void CheckRestart(bool restart)
    {
        mustRestart = restart;
    }
    public void RestartSceneUI()
    {
        if (mustRestart == true)
        {
            Debug.Log("CAUGHT");
            restartUI.SetActive(true);
        }
    }
    public void RestartScene()
    {
        SceneManager.LoadScene("Scene1");
    }
    private void Update()
    {
        RestartSceneUI();
    }
}

