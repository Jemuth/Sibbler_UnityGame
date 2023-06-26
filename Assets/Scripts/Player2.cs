using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player2 : PlayableCharacter
{
    [SerializeField] private Animator m_crouchAnimation;
    [SerializeField] private GameObject itemPrefab;
    private bool onCrouchZone;
    private bool canUseSkill2;
    private bool skillPressed2;
    private void Start()
    {
        canUseSkill2 = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tunnel"))
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
        if (onCrouchZone && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && !Input.GetKey(KeyCode.LeftShift))
        {
            m_crouchAnimation.SetBool("IsMovingCrouch", true);
        }
        else
        {
            m_crouchAnimation.SetBool("IsMovingCrouch", false);
        }
    }

    private void UseSkill2()
    {
        if (Input.GetKeyDown(KeyCode.E) && canUseSkill2)
        {
            skillPressed2 = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !canUseSkill2)
        {
            // Debug.Log("Cannot use that now!");
        }
        else
        {
            skillPressed2 = false;
        }
    }

    private IEnumerator WaitToMove()
    {
        canMove = false;
        canRotate = false;
        yield return new WaitForSeconds(1.5f);
        canMove = true;
        canRotate = true;
    }

    private IEnumerator AbilityCooldown()
    {
        canUseSkill2 = false;
        yield return new WaitForSeconds(20);
        canUseSkill2 = true;
    }
    private IEnumerator DropBearTimer()
    {
        yield return new WaitForSeconds(1.5f);
        Vector3 spawnPosition = transform.position + transform.forward * 0.4f + transform.up * 0; // Adjust the distance as per your requiremen
        Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
    }
    private void DestroyAllBears()
    {
        GameObject[] bears = GameObject.FindGameObjectsWithTag("Bear");

        foreach (GameObject bear in bears)
        {
            Destroy(bear);
        }
    }
    private IEnumerator DestroyAllBearsCoroutine()
    {
        yield return new WaitForSeconds(15);
        DestroyAllBears();
    }

    private void DropBear()
    {
        if (canUseSkill2 && skillPressed2 && !onCrouchZone)
        {
            m_crouchAnimation.SetBool("IsDrop", true);
            StartCoroutine(DropBearTimer());
            StartCoroutine(AbilityCooldown());
            StartCoroutine(WaitToMove());
            StartCoroutine(DestroyAllBearsCoroutine());
        } else
        {
            m_crouchAnimation.SetBool("IsDrop", false);
        }
    }

    protected override void OnUpdating()
    {
        CrouchChecker();
        Crawling();
        DropBear();
        UseSkill2();
    }
}
