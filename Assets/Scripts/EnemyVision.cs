using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class EnemyVision : MonoBehaviour
{
    public float visionRange = 10f;
    public float visionAngle = 60f;
    public LayerMask targetLayer;
    public LayerMask obstacleLayer;
    public float detectionTimeThreshold = 2f;
    public float visionDisableDuration = 5f;
    public float colorTransitionDuration = 1f; // Duration for transitioning color back to original
    private Color originalSpotlightColor;
    private bool playerDetected = false;
    private float detectionTime = 0f;
    private float visionDisableTimer = 0f;
    private float colorTransitionTimer = 0f; // Timer for color transition
    private Light spotlight;
    private bool enemyHit;
    private GameObject[] players;
    private Color targetColor; // Target color for color transition
    private Color currentColor; // Current color during color transition


    void Start()
    {
        spotlight = GetComponentInChildren<Light>();
        originalSpotlightColor = spotlight.color;
        players = new GameObject[2];
        players[0] = GameObject.FindGameObjectWithTag("P1");
        players[1] = GameObject.FindGameObjectWithTag("P2");
        currentColor = originalSpotlightColor;
    }

    private bool IsPlayerInVision(GameObject player, GameObject enemy)
    {
        // Check if player is within vision range and angle
        Vector3 directionToPlayer = player.transform.position - enemy.transform.position;
        float angleToPlayer = Vector3.Angle(enemy.transform.forward, directionToPlayer);

        if (angleToPlayer <= visionAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(enemy.transform.position, directionToPlayer, out hit, visionRange, targetLayer))
            {
                if (hit.collider.CompareTag("P1") || hit.collider.CompareTag("P2"))
                {
                    // Check for obstacles between enemy and player
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
            // Vision is disabled, reduce the timer and return
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
                    SetSpotlightColor(Color.red);

                    // Perform actions when detected goes here
                }

                if (detectionTime < detectionTimeThreshold)
                {
                    detectionTime += Time.deltaTime;
                    if (detectionTime >= detectionTimeThreshold)
                    {
                        GameManager.instance.CheckDetected(true);
                    }
                }
                return;
            }
        }

        if (playerDetected && !enemyHit)
        {
            playerDetected = false;
            StartColorTransition(originalSpotlightColor);
            detectionTime = 0f;
        }
    }

    private void SetSpotlightColor(Color color)
    {
        spotlight.color = color;
    }

    private void StartColorTransition(Color m_targetColor)
    {
        targetColor = m_targetColor;
        currentColor = spotlight.color;
        colorTransitionTimer = colorTransitionDuration;
    }

    private void UpdateColorTransition()
    {
        if (colorTransitionTimer > 0f)
        {
            colorTransitionTimer -= Time.deltaTime;
            float t = 1f - (colorTransitionTimer / colorTransitionDuration);
            spotlight.color = Color.Lerp(currentColor, targetColor, t);
        }
    }

    public void EnemyHitChecker(bool m_enemyHit)
    {
        enemyHit = m_enemyHit;
        if (visionDisableTimer <= 0f && enemyHit)
        {
            visionDisableTimer = visionDisableDuration;
        } else
        {
            enemyHit = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle * 0.5f, 0) * transform.forward;
        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle * 0.5f, 0) * transform.forward;

        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * visionRange);
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * visionRange);
    }

    private void Update()
    {
       PlayerInVision();
       EnemyHitChecker(enemyHit);
       UpdateColorTransition();
    }

   
}