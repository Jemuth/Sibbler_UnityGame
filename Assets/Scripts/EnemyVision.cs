using UnityEngine;
using System.Collections;

public class EnemyVision : EnemyCharacter
{
    public float visionRange = 10f;
    public float visionAngle = 60f;
    public LayerMask targetLayer;
    public LayerMask obstacleLayer;
    public float detectionTimeThreshold = 2f;
    public float visionDisableDuration = 10f;
    public float colorTransitionDuration = 1f; // Duration for transitioning color back to original
    private Color originalEmissionColor;
    private bool playerDetected = false;
    private float detectionTime = 0f;
    private float visionDisableTimer = 0f;
    private float colorTransitionTimer = 0f; // Timer for color transition
    private GameObject[] players;
    private Color targetColor; // Target color for color transition
    private Color currentColor; // Current color during color transition
    public int enemyID;
    private bool isHit;
    [SerializeField] private Material emissiveMaterial;
    [SerializeField] private Renderer objectToChange;
    private Color originalEmissiveColor;

    void Start()
    {
        originalEmissionColor = Color.yellow;
        players = new GameObject[2];
        players[0] = GameObject.FindGameObjectWithTag("P1");
        players[1] = GameObject.FindGameObjectWithTag("P2");
        currentColor = Color.white;
        emissiveMaterial = objectToChange != null ? objectToChange.GetComponentInChildren<Renderer>().material : null;
        originalEmissiveColor = emissiveMaterial != null ? emissiveMaterial.GetColor("_EmissionColor") : Color.yellow;

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
                    ChangeEmissionColor(Color.red);

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

        if (playerDetected && !isHit)
        {
            playerDetected = false;
            StartColorTransition(originalEmissionColor);
            detectionTime = 0f;
        }
    }
    private void ChangeEmissionColor(Color color)
    {
        if (emissiveMaterial != null)
        {
            emissiveMaterial.SetColor("_EmissionColor", color);
        }
    }

    private void StartColorTransition(Color targetColor)
    {
        this.targetColor = targetColor;
        colorTransitionTimer = colorTransitionDuration;
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

    public void EnemyHitChecker(bool enemyHit)
    {
        isHit = enemyHit;
    }

    public void VisionReduced()
    {
        if (visionDisableTimer <= 0f && isHit)
        {
            visionDisableTimer = visionDisableDuration;
        }
        else
        {
            isHit = false;
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

        // Draw the vision cone
        int segments = 36; // Number of segments to approximate the circle
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
        PlayerInVision();
        VisionReduced();
        UpdateColorTransition();
    }
}