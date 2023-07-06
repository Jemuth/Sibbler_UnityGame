using UnityEngine;
using System.Collections;

public class EnemyVision : EnemyCharacter
{
    public delegate void SetDetected();
    public static event SetDetected OnSetDetected;
    public delegate void LeaveDetection();
    public static event LeaveDetection OnLeaveDetected;
    public float visionRange = 10f;
    public float visionAngle = 60f;
    public LayerMask targetLayer;
    public LayerMask obstacleLayer;
    public float detectionTimeThreshold = 2f;
    public float visionDisableDuration = 8f;
    public float colorTransitionDuration = 1f;
    public float transparencyTransitionDuration = 1f;
    private Color originalEmissionColor;
    private bool playerDetected = false;
    private float detectionTime = 0f;
    private float visionDisableTimer = 0f;
    private float colorTransitionTimer = 0f;
    private float transparencyTransitionTimer = 0f;
    private GameObject[] players;
    private Color targetColor;
    private Color targetTransparency;
    private Color currentColor; 
    public int enemyID;
    [SerializeField] private Material emissiveMaterial; // For minimap
    [SerializeField] private Renderer objectToChange;
    private Color originalEmissiveColor;
    [SerializeField] private SpriteRenderer stunSpriteRenderer;
    [SerializeField] private AudioSource m_lookerAudioStun;
    private bool canBeHitCheck;
    public bool canBeHit;
    public AudioClip beingStunned;
    public bool isStunned;
    [SerializeField] private EnemyCharacterData m_enemyData;
    [SerializeField] private Material detectionMaterial;
    [SerializeField] private Renderer transparencyChange;
    private Color originalTransparency;

    void Start()
    {
        originalEmissionColor = Color.yellow;
        originalTransparency = new Color(1f, 0.5f, 0f, 0f);
        isStunned = false;
        players = new GameObject[2];
        players[0] = GameObject.FindGameObjectWithTag("P1");
        players[1] = GameObject.FindGameObjectWithTag("P2");
        detectionMaterial = transparencyChange != null ? transparencyChange.GetComponentInChildren<Renderer>().material : null;
        emissiveMaterial = objectToChange != null ? objectToChange.GetComponentInChildren<Renderer>().material : null;
        originalTransparency = detectionMaterial != null ? detectionMaterial.GetColor("_BaseColor") : originalTransparency;
        originalEmissiveColor = emissiveMaterial != null ? emissiveMaterial.GetColor("_EmissionColor") : Color.yellow;
        stunSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        canBeHitCheck = m_enemyData.isHittable;
        if (stunSpriteRenderer != null)
        {
            stunSpriteRenderer.enabled = false;
        }

    }

    private bool IsPlayerInVision(GameObject player, GameObject enemy)
    {
       
        Vector3 directionToPlayer = player.transform.position - enemy.transform.position;
        float angleToPlayer = Vector3.Angle(enemy.transform.forward, directionToPlayer);

        if (angleToPlayer <= visionAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(enemy.transform.position, directionToPlayer, out hit, visionRange, targetLayer))
            {
                if (hit.collider.CompareTag("P1") || hit.collider.CompareTag("P2"))
                {
                    
                    RaycastHit obstacleHit;
                    if (Physics.Raycast(enemy.transform.position, directionToPlayer, out obstacleHit, visionRange, obstacleLayer))
                    {
                        if (obstacleHit.distance < hit.distance)
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }
        }

        return false;
    }

    private void PlayerInVision()
    {
        if (visionDisableTimer > 0f)
        {
            
            visionDisableTimer -= Time.deltaTime;
            return;
        }

        foreach (GameObject player in players)
        {
            if (IsPlayerInVision(player, gameObject))
            {
                if (!playerDetected)
                {
                    playerDetected = true;
                    ChangeEmissionColor(Color.red);
                    ChangeTransparencyColor(new Color(1f, 0f, 0f, 0.8f));
                    if(player.CompareTag("P1"))
                    {
                        GameManager.instance.DetectionBar1(true);
                    }
                    if (player.CompareTag("P2"))
                    {
                        GameManager.instance.DetectionBar2(true);
                    }
                    if (!isStunned)
                    {
                        OnSetDetected();
                    }  

                    // Perform action!
                }

                if (detectionTime < detectionTimeThreshold)
                {
                    detectionTime += Time.deltaTime;
                    if (detectionTime >= detectionTimeThreshold)
                    {
                        GameManager.instance.CheckDetected(true);
                        if (!isStunned)
                        {
                            OnSetDetected();
                        }
                    }
                }
                return;
            }
        }

        if (playerDetected && !isStunned)
        {
            playerDetected = false;
            GameManager.instance.DetectionBar1(false);
            GameManager.instance.DetectionBar2(false);
            OnLeaveDetected();
            StartColorTransition(originalEmissionColor);
            StartTransparencyTransition(originalTransparency);
            detectionTime = 0f;
        }
    }
    //private void CheckPlayerDetected(bool p_checkDetected)
    //{
    //    playerDetected = p_checkDetected;
    //    GameManager.instance.CheckDetectedP1(p_checkDetected);
    //}
    private void ChangeEmissionColor(Color color)
    {
        if (emissiveMaterial != null)
        {
            emissiveMaterial.SetColor("_EmissionColor", color);
        }
    }
    private void ChangeTransparencyColor(Color color)
    {
        if (detectionMaterial != null)

            detectionMaterial.SetColor("_BaseColor", color);
    }

    private void StartColorTransition(Color targetColor)
    {
        this.targetColor = targetColor;
        colorTransitionTimer = colorTransitionDuration;
    }
    private void StartTransparencyTransition(Color targetTransparency)
    {
        this.targetTransparency = targetTransparency;
        transparencyTransitionTimer = transparencyTransitionDuration;
    }

    private void UpdateColorTransition()
    {
        if (colorTransitionTimer > 0f)
        {
            colorTransitionTimer -= Time.deltaTime;
            float t = 1f - (colorTransitionTimer / colorTransitionDuration);
            Color newEmissionColor = Color.Lerp(originalEmissiveColor, targetColor, t);
            ChangeEmissionColor(newEmissionColor);
        }
    }
    private void UpdateTransparencyTransition()
    {
        if (transparencyTransitionTimer > 0f)
        {
            transparencyTransitionTimer -= Time.deltaTime;
            float t = 1f - (transparencyTransitionTimer / transparencyTransitionDuration);
            Color newTransparencyColor = Color.Lerp(originalTransparency, targetTransparency, t);
            ChangeTransparencyColor(newTransparencyColor);
        }
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
    public void StunEnemy()
    {
        if (!isStunned && canBeHit)
        {
            isStunned = true;
        }
    }

    public void VisionReduced()
    {
        if (visionDisableTimer <= 0f && isStunned)
        {
            visionDisableTimer = visionDisableDuration;

            if (stunSpriteRenderer != null)
            {
                stunSpriteRenderer.enabled = true;
            }
        }
        else
        {
            isStunned = false;
        }
    }
    public IEnumerator PlayStun()
    {
        yield return new WaitForSeconds(0.3f);
        m_lookerAudioStun.PlayOneShot(beingStunned, 0.5f);
    }
    public void PlayStunSound()
    {
        if (isStunned == true)
        {
            StartCoroutine(PlayStun());
            
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Quaternion rightRotation = Quaternion.Euler(0f, visionAngle * 0.5f, 0f);
        Quaternion leftRotation = Quaternion.Euler(0f, -visionAngle * 0.5f, 0f);

        Vector3 rightDirection = rightRotation * transform.forward;
        Vector3 leftDirection = leftRotation * transform.forward;

        Gizmos.DrawLine(transform.position, transform.position + rightDirection * visionRange);
        Gizmos.DrawLine(transform.position, transform.position + leftDirection * visionRange);

        // Gizmo visual
        int segments = 36; // 
        float angleIncrement = visionAngle / segments;
        Vector3 previousDirection = rightDirection;

        for (int i = 1; i <= segments; i++)
        {
            float angle = angleIncrement * i;
            Quaternion rotation = Quaternion.Euler(0f, -angle, 0f);
            Vector3 direction = rotation * rightDirection;

            Gizmos.DrawLine(transform.position, transform.position + previousDirection * visionRange);
            Gizmos.DrawLine(transform.position, transform.position + direction * visionRange);

            previousDirection = direction;
        }
    }

    private void Update()
    {
        PlayStunSound();
        PlayerInVision();
        VisionReduced();
        UpdateColorTransition();
        UpdateTransparencyTransition();
        ConditionChecker();
        //CheckPlayerDetected(playerDetected);
        if (visionDisableTimer <= 0f && stunSpriteRenderer != null && stunSpriteRenderer.enabled)
        {
            stunSpriteRenderer.enabled = false;
        }
    }
}