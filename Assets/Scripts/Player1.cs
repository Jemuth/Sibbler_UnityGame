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
        if (character.gameObject.CompareTag("Enemy"))
        {
            isAtDistance = true;
        }
    }
    private void OnTriggerExit(Collider character)
    {
        if (character.gameObject.CompareTag("Enemy"))
        {
            isAtDistance = false;
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
            // Debug.Log("Bonk");
            m_batAnimation.SetBool("isUsingSkill", true);
            StartCoroutine(AbilityCooldown());
            StartCoroutine(WaitToMove());
            //EnemyVision.instance.EnemyHitChecker(true);
            GameManager.instance.IsEnemyHit(true);
        }
        else if (m_checkBatUser.isBatUser && isHitable && !isAtDistance && skillPressed)
        {
            // Debug.Log("I need to be behind a creature!");
            m_batAnimation.SetBool("isUsingSkill", false);
        }
        else
        {
            m_batAnimation.SetBool("isUsingSkill", false);
        }
    }
    protected override void OnUpdating()
    {
        UseBat();
        UseSkill();
    }
}
