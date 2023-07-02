using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter(Collider character)

    {
        if (character.gameObject.CompareTag("P1"))
        {
            GameManager.instance.CheckKeyCollected(true);
            Destroy(gameObject);
        }
        if (character.gameObject.CompareTag("P2"))
        {
            GameManager.instance.CheckKeyCollected(true);
            Destroy(gameObject);
        }
    }
}
