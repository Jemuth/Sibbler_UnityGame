using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyCharacter : GameCharacter
{
    [SerializeField] private EnemyCharacterData m_enemyData;
    private bool isHitable;
    private bool canBeHit;

    private void Start()
    {
        isHitable = m_enemyData.isHittable;
    }
    public void CanHitEnemy(bool p_canHit)
    {
        canBeHit = p_canHit;
        GameManager.instance.CheckEnemyDistance(p_canHit);
    }
    private void OnTriggerEnter(Collider character)
    {
        if (isHitable && character.gameObject.CompareTag("P1Collider"))
        {
            canBeHit = true;
        }
    }
    private void OnTriggerExit(Collider character)
    {
        if (isHitable && character.gameObject.CompareTag("P1Collider"))
        {
            canBeHit = false;
        }
    }
    private void Update()
    {
        CanHitEnemy(canBeHit);
    }
}
