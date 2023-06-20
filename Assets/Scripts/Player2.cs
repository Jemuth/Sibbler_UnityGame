using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player2 : PlayableCharacter
{
    protected override void OnUpdating()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("P2 Skill");
        }
    }

}
