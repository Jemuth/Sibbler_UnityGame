using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool mustRestart;
    private bool winGame;
    private bool haveKeys;
    private bool playersOnExit;
    [SerializeField] private GameObject restartUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private List<string> sceneNames;

    private void Start()
    {
        restartUI.SetActive(false);
        winUI.SetActive(false);
    }
    public void CheckRestart(bool restart)
    {
        mustRestart = restart;
    }
    public void CheckKeys(bool checkKeys)
    {
        haveKeys = checkKeys;
    }
    public void CheckPlayersOnExit(bool checkPlayers )
    {
        playersOnExit = checkPlayers;
    }
    public void CheckWinGame()
    {
        if(haveKeys && playersOnExit)
        {
            winGame = true;
        }
    }
    public void RestartSceneUI()
    {
        if (mustRestart == true)
        {
            restartUI.SetActive(true);
        }
    }
    
    public void WinUI()
    {
        if (winGame == true)
        {
            winUI.SetActive(true);
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    public void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene + 1 <sceneNames.Count)
            SceneManager.LoadScene(sceneNames[currentScene + 1]);
        else
            Debug.Log("It's the last scene");
    }
    public void LoadScene2()
    {
        SceneManager.LoadScene("Scene2");
    }
    public void Ending()
    {
        SceneManager.LoadScene("NowLoading3");
    }
    private void Update()
    {
        RestartSceneUI();
        CheckWinGame();
        WinUI();
    }
}

