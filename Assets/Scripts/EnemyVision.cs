using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float visionRange = 10f;
    public float visionAngle = 60f;
    public LayerMask targetLayer;
    public LayerMask obstacleLayer; // New layer mask for obstacles
    public float detectionTimeThreshold = 2f;
    private Color originalSpotlightColor;
    public float originalSpotlightIntensity;
    private bool playerDetected = false;
    private float detectionTime = 0f;
    private Light spotlight;
    [SerializeField] private GameObject m_enemy;
    private GameObject[] players;

    void Start()
    {
        spotlight = GetComponentInChildren<Light>();
        originalSpotlightColor = spotlight.color;
        originalSpotlightIntensity = spotlight.intensity;
        players = new GameObject[2];
        players[0] = GameObject.FindGameObjectWithTag("P1");
        players[1] = GameObject.FindGameObjectWithTag("P2");
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
                    // Check for obstacles between enemy and player
                    RaycastHit obstacleHit;
                    if (Physics.Raycast(enemy.transform.position, directionToPlayer, out obstacleHit, visionRange, obstacleLayer))
                    {
                        // If an obstacle is hit before the player, return false
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
        foreach (GameObject player in players)
        {
            if (IsPlayerInVision(player, m_enemy))
            {
                if (!playerDetected)
                {
                    playerDetected = true;
                    SetSpotlightColor(Color.yellow);

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

        if (playerDetected)
        {
            playerDetected = false;
            SetSpotlightColor(originalSpotlightColor);
            detectionTime = 0f;
        }
    }

    private void SetSpotlightColor(Color color)
    {
        spotlight.color = color;
    }

    private void Update()
    {
        PlayerInVision();
    }
}
