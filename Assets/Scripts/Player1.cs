using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;


public class Player1 : PlayableCharacter
{
    [SerializeField] private Animator m_batAnimation;
    [SerializeField] private PlayableCharacterData m_checkBatUser;
    [SerializeField] private AudioSource m_p1Audio;
    public AudioClip batSwing;
    [SerializeField] private AudioSource m_detectedSource;
    public AudioClip detectedSound;
    [SerializeField] private SpriteRenderer detectedSpriteRenderer;

    // For skills animation
    private bool skillPressed;
    private bool canUseSkill;
    public bool batUsed;
    public int currentEnemyID;
    public bool skill1Used;
    public bool detected;
    void Start()
    {
        canUseSkill = true;
        skill1Used = false;
        detected = false;
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
                spriteColor.a = 1f;
                detectedSpriteRenderer.color = spriteColor;
                detectedSpriteRenderer.enabled = true;
                if (!m_detectedSource.isPlaying)
                {
                    m_detectedSource.PlayOneShot(detectedSound, 0.4F);
                }  
            }
            else
            {
                Color spriteColor = detectedSpriteRenderer.color;
                spriteColor.a = 0f;
                detectedSpriteRenderer.color = spriteColor;
                detectedSpriteRenderer.enabled = false;
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
        skill1Used = true;
        GameManager.instance.Player1SkillUsed(skill1Used);
        yield return new WaitForSeconds(8f);
        canUseSkill = true;
        skill1Used = false;
        GameManager.instance.Player1SkillUsed(skill1Used);
    }

    //Specific skills
    //P1 Skills
    private void OnTriggerStay(Collider character)
    {
        EnemyVision enemyVision = character.gameObject.GetComponent<EnemyVision>();
        EnemyCharacter enemyCharacter = character.gameObject.GetComponent<EnemyCharacter>();
        if (enemyVision != null && skillPressed)
        {
                enemyVision.StunEnemy();
        }
        if (enemyCharacter != null && skillPressed)
        {
            enemyCharacter.StunEnemyCharacter();
        }

    }
    
    public void UseBat()
    {
        if (m_checkBatUser.isBatUser && skillPressed)
        {
            m_p1Audio.PlayOneShot(batSwing, 1F);
            m_batAnimation.SetBool("isUsingSkill", true);
            StartCoroutine(AbilityCooldown());
            StartCoroutine(WaitToMove());
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
        DetectedSprite();
    }
}
