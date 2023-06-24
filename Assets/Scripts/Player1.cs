using System.Collections;
using UnityEngine;


public class Player1 : PlayableCharacter
{
    [SerializeField] private Animator m_batAnimation;
    [SerializeField] private PlayableCharacterData m_checkBatUser;

    // For skills animation
    private bool skillPressed;
    private bool canUseSkill;
    private bool isHitable;
    private bool isAtDistance;
    public bool batUsed;
    public int currentEnemyID;
    void Start()
    {
        canUseSkill = true;
    }


    private void UseSkill()
    {
        if (Input.GetKeyDown(KeyCode.E) && canUseSkill)
        {
            skillPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !canUseSkill)
        {
            // Debug.Log("Cannot use that now!");
        }
        else
        {
            skillPressed = false;
        }
    }
    private IEnumerator WaitToMove()
    {
        canMove = false;
        canRotate = false;
        yield return new WaitForSeconds(0.8f);
        canMove = true;
        canRotate = true;
    }
    private IEnumerator AbilityCooldown()
    {
        canUseSkill = false;
        yield return new WaitForSeconds(4);
        canUseSkill = true;
    }
    //Specific skills
    //P1 Skills
    private void OnTriggerEnter(Collider character)
    {
        EnemyVision enemy = character.gameObject.GetComponent<EnemyVision>();
        if (enemy != null)
        {
            isAtDistance = true;
            currentEnemyID = enemy.enemyID; // Store the ID of the colliding enemy
        }
    }
    private void OnTriggerExit(Collider character)
    {
        EnemyCharacter enemy = character.gameObject.GetComponent<EnemyVision>();
        if (enemy != null)
        {
            isAtDistance = false;
            currentEnemyID = -1; // Reset the current enemy ID when no longer colliding
        }
    }
    public void CheckHitable(bool m_canUseBat)
    {
        isHitable = m_canUseBat;
    }
    public void UseBat()
    {
        if (m_checkBatUser.isBatUser && isHitable && isAtDistance && skillPressed)
        {
            m_batAnimation.SetBool("isUsingSkill", true);
            StartCoroutine(AbilityCooldown());
            StartCoroutine(WaitToMove());
            GameManager.instance.PlayerHitEnemy(currentEnemyID);
        }
        else if (m_checkBatUser.isBatUser && isHitable && !isAtDistance && skillPressed)
        {
            m_batAnimation.SetBool("isUsingSkill", false);
            GameManager.instance.PlayerHitEnemy(-1); // Reset the enemy ID when not at distance
        }
        else
        {
            m_batAnimation.SetBool("isUsingSkill", false);
            GameManager.instance.PlayerHitEnemy(-1); // Reset the enemy ID when not hitting
        }
    }
    protected override void OnUpdating()
    {
        UseBat();
        UseSkill();
    }
}
