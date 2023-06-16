using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Rendering;

public class CharacterSwap : MonoBehaviour
{
    public List<Transform> possibleCharacters;
    private bool p1Active = true;
    private bool canChange;
    //[SerializeField] private CinemachineVirtualCamera cam1, cam2;
    private void Start()
    {
        EnablePlayer(possibleCharacters[0]);
        DisablePlayer(possibleCharacters[1]);
       // camera1 = cam1.GetComponent<CinemachineVirtualCamera>();
        //camera2 = cam2.GetComponent<CinemachineVirtualCamera>();
        //camera1.SetActive(true);
    }
    public void ChangeEnabled(bool changeEnabled)
    {
        canChange = changeEnabled;
    }
    private void SwapCharacters()
    {
        if (Input.GetKeyDown(KeyCode.C) && canChange)
        {
            if(p1Active)
            {
                DisablePlayer(possibleCharacters[0]);
                EnablePlayer(possibleCharacters[1]);
                p1Active = false;
               //camera1.SetActive(false);
                //camera2.SetActive(true);
                Debug.Log("Player 2 Active");
            } else
            {
                DisablePlayer(possibleCharacters[1]);
                EnablePlayer(possibleCharacters[0]);
                p1Active=true;
                //camera1.SetActive(true);
                //camera2.SetActive(false);
                Debug.Log("Player 1 Active");
            }
        }
    }
    private void EnablePlayer(Transform player)
    {
        player.GetComponent<PlayableCharacter>().enabled = true;
    }
    private void DisablePlayer(Transform player)
    {
       player.GetComponent<PlayableCharacter>().enabled = false;
    }
    private void Update()
    {
        SwapCharacters();
    }
}
