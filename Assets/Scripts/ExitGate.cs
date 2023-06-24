using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGate : MonoBehaviour
{
    private bool p1OnExit;
    private bool p2OnExit;

    private void OnTriggerEnter(Collider character)
    {
        if (character.gameObject.CompareTag("P1"))
        {
            p1OnExit = true;
        }
        if (character.gameObject.CompareTag("P2"))
        {
            p2OnExit = true;
        }
    }
    private void OnTriggerExit(Collider character)
    {
        if (character.gameObject.CompareTag("P1"))
        {
            p1OnExit = false;
        }
        if (character.gameObject.CompareTag("P2"))
        {
            p2OnExit = false;
        }
    }
    private void ExitEnabled()
    {
        if (p1OnExit && p2OnExit)
        
        {
            GameManager.instance.CheckPlayersExit(true);
        }
    }
    private void Update()
    {
        ExitEnabled();
    }

}
