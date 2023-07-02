using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Player2 : PlayableCharacter
{
    [SerializeField] private Animator m_crouchAnimation;
    [SerializeField] private GameObject itemPrefab;
    private bool onCrouchZone;
    private bool canUseSkill2;
    private bool skillPressed2;
    public bool skill2Used;
    [SerializeField] private SpriteRenderer detectedSpriteRenderer;
    [SerializeField] private AudioSource m_detectedSource;
    public AudioClip detectedSound;
    public bool detected;
    private void Start()
    {
        canUseSkill2 = true;
        skill2Used = false;
        detected = false;
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
    private void OnEnable()
    {
        EnemyVision.OnSetDetected += DisplaySprite;
        EnemyVision.OnLeaveDetected += HideSprite;
    }
    private void DisplaySprite()
    {
        detected = true;
    }
    private void HideSprite()
    {
        detected = false;
    }
    private void DetectedSprite()
    {
        if (detected)
        {
            Color spriteColor = detectedSpriteRenderer.color;
            spriteColor.a = 1f; // Alpha value of 1 sets transparency to 255 (fully opaque)
            detectedSpriteRenderer.color = spriteColor;
            detectedSpriteRenderer.enabled = true;
            if (!m_detectedSource.isPlaying)
            {
                m_detectedSource.PlayOneShot(detectedSound, 1F);
            }
        }
        else
        {
            Color spriteColor = detectedSpriteRenderer.color;
            spriteColor.a = 0f; // Alpha value of 0 sets transparency to 0 (fully transparent)
            detectedSpriteRenderer.color = spriteColor;
            detectedSpriteRenderer.enabled = false;
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
            GameManager.instance.Player2SkillUsed(skillPressed2);
        }
        else if (Input.GetKeyDown(KeyCode.E) && !canUseSkill2)
        {
            // Debug.Log("Cannot use that now!");
        }
        else
        {
            skillPressed2 = false;
            GameManager.instance.Player2SkillUsed(skillPressed2);
        }
    }
    private IEnumerator WaitToMove()
    {
        canMove = false;
        canRotate = false;
        yield return new WaitForSeconds(1f);
        canMove = true;
        canRotate = true;
    }

    private IEnumerator AbilityCooldown()
    {
        canUseSkill2 = false;
        skill2Used = true;
        GameManager.instance.Player2SkillUsed(skill2Used);
        yield return new WaitForSeconds(15);
        skill2Used = false;
        GameManager.instance.Player2SkillUsed(skill2Used);
        canUseSkill2 = true;
    }
    private IEnumerator DropBearTimer()
    {
        yield return new WaitForSeconds(1f);
        Vector3 spawnPosition = transform.position + transform.forward * 0.4f + transform.up * 0;
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
        yield return new WaitForSeconds(14);
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
        }
        else
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
        DetectedSprite();
    }
}
