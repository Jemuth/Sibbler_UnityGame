using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyCharacter : GameCharacter
{
    [SerializeField] private EnemyCharacterData m_enemyData;
    private bool isHitable;
    private bool atDistance;
    private bool canBeHit;

    private void Start()
    {
        isHitable = m_enemyData.isHittable;
    }
    private void OnTriggerEnter(Collider character)
    {
        if (character.gameObject.CompareTag("P1Collider"))
        {
            atDistance = true;
        }
    }
    private void OnTriggerExit(Collider character)
    {
        if (character.gameObject.CompareTag("P1Collider"))
        {
            atDistance = false;
        }
    }
    private void ConditionChecker()
    {
        if (isHitable && atDistance)
        {
            canBeHit = true;
        }
        else
        {
            canBeHit = false;
        }
    }
    public void CanHitEnemy(bool p_canHit)
    {
        if(canBeHit)
        {
            canBeHit = p_canHit;
            GameManager.instance.CheckEnemyDistance(p_canHit);
        } else
        {
            canBeHit = p_canHit;
            GameManager.instance.CheckEnemyDistance(p_canHit);
        }
        
    }
    private void Update()
    {
        ConditionChecker();
        CanHitEnemy(canBeHit);
    }
}
