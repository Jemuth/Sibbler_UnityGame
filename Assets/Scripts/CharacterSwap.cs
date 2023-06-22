using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Rendering;
using UnityEngine.XR;

public class CharacterSwap : MonoBehaviour
{

    public List<Transform> possibleCharacters;
    private bool p1Active = true;
    private bool canChange;
    public GameObject P1UI,P2UI;

    private void Awake()
    {
        P1UI = GameObject.Find("Player1UI");
        P2UI = GameObject.Find("Player2UI");
    }
    private void Start()
    {
        EnablePlayer(possibleCharacters[0]);
        DisablePlayer(possibleCharacters[1]);
        P1UI.transform.localScale = new Vector3(1, 1, 1);
        P2UI.transform.localScale = new Vector3(0, 0, 0);
    }
    // Determine if can change
    public void ChangeEnabled(bool changeEnabled)
    {
        canChange = changeEnabled;
    }
    private void SwapCharacters()
    {
        if (Input.GetKeyDown(KeyCode.C) && canChange)
        {
            if (p1Active)
            {
                DisablePlayer(possibleCharacters[0]);
                EnablePlayer(possibleCharacters[1]);
                p1Active = false;
                P1UI.transform.localScale = new Vector3(0, 0, 0);
                P2UI.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                DisablePlayer(possibleCharacters[1]);
                EnablePlayer(possibleCharacters[0]);
                p1Active = true;
                P2UI.transform.localScale = new Vector3(0, 0, 0);
                P1UI.transform.localScale = new Vector3(1, 1, 1);
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
