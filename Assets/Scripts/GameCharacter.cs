using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameCharacter : MonoBehaviour
{
    [SerializeField] private string m_name;
    [SerializeField] private string m_id;

    public string GetName() => m_name;
    public string GetID() => m_id;
}
