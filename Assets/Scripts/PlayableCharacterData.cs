using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayableCharacterData")]
public class PlayableCharacterData : ScriptableObject
{
    public float maxStamina;
    public float walkSpeed;
    public float runSpeed;
    public float acceleration;
    public float deceleration;
    public float regenTime;
}

