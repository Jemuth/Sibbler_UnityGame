using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player1 : PlayableCharacter
{
    [SerializeField] private Animator m_batAnimation;
    [SerializeField] private PlayableCharacterData m_checkBatUser;
    private ItemObject itemObject;
    // For skills animation
    private bool skillPressed;
    private bool canUseSkill;
    private bool isAtDistance;
    public bool batUsed;
   
    void Start()
    {
        canUseSkill = true;
    }
    
    //Picking items
    
    public void OnTriggerEnter(Collider collision)
    {
        if(collision.TryGetComponent<ItemObject>(out ItemObject item))
        {
            Debug.Log("Hi");
            item.OnHandlePickupItem();
            
        }
    }
    // Skill usage and cooldowns
    // Skills and skills animation cooldowns


    private void UseSkill()
    {
        if (Input.GetKeyDown(KeyCode.E) && canUseSkill)
        {
            skillPressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !canUseSkill)
        {
            Debug.Log("Cannot use that now!");
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
    public void DistanceChecker(bool m_canUseBat)
    {
        isAtDistance = m_canUseBat;
    }
    public void UseBat()
    {
        
        if (m_checkBatUser.isBatUser && isAtDistance && skillPressed)
        {
            Debug.Log("Bonk");
            m_batAnimation.SetBool("isUsingSkill", true);
            StartCoroutine(AbilityCooldown());
            StartCoroutine(WaitToMove());
        }
        else if (m_checkBatUser.isBatUser && !isAtDistance && skillPressed)
        {
            Debug.Log("I need to be behind a creature!");
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
