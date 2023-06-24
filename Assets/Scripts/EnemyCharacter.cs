using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyCharacter : GameCharacter
{
    [SerializeField] private EnemyCharacterData m_enemyData;
    // [SerializeField] private Animator lookerAnimator;
    private bool canBeHitCheck;
    private bool canBeHit;

    private void Start()
    {
        canBeHitCheck = m_enemyData.isHittable;
    }
    private void ConditionChecker()
    {
        if (canBeHitCheck)
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
            GameManager.instance.CheckEnemyHitable(p_canHit);
        } else
        {
            canBeHit = p_canHit;
            GameManager.instance.CheckEnemyHitable(p_canHit);
        }
        
    }

    private void Update()
    {
        ConditionChecker();
        CanHitEnemy(canBeHit);
    }
}
