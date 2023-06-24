using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player2 : PlayableCharacter
{
    [SerializeField] private Animator m_crouchAnimation;
    private bool onCrouchZone;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Tunnel"))
        {
            onCrouchZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tunnel"))
        {
            onCrouchZone = false;
        }
    }
    private void CrouchChecker()
    {
        if (onCrouchZone)
        {
            m_crouchAnimation.SetBool("IsCrouching", true);
            m_staminaStatus.canUseStamina = false;
            runEnabled = false;
            maxRunSpeed = 0;
        }
         else
        {
            m_crouchAnimation.SetBool("IsCrouching", false);
        }
        
    }
    private void Crawling()
    {
        if(onCrouchZone && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && !Input.GetKey(KeyCode.LeftShift))
        {
            m_crouchAnimation.SetBool("IsMovingCrouch", true);
        } else
        {
            m_crouchAnimation.SetBool("IsMovingCrouch", false);
        }

    }
    protected override void OnUpdating()
    {
        CrouchChecker();
        Crawling();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("P2 Skill");
        }
    }


}
