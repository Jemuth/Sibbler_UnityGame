using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGate : MonoBehaviour
{
    private void OnTriggerStay(Collider character)
    {
        if (character.gameObject.CompareTag("P1"))
        {
            Debug.Log("YA");
        }
    }
}
