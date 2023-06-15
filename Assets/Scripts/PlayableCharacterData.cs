using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayableCharacterData")]
public class PlayableCharacterData : ScriptableObject
{
    public float maxStamina;
    public float speedMultiplier;
    public float acceleration;
}

