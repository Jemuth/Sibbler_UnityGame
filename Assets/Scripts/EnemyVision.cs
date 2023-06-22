using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float visionRange = 10f;          // The maximum distance the enemy can see
    public float visionAngle = 60f;          // The field of view angle of the enemy
    public LayerMask targetLayer;            // The layer that the player is on
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
        // playerCaught = false;

        // Find all player objects using their tags
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
                if (hit.collider.CompareTag("P1Collider"))
                {
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

                    // Perform actions when the player is detected, like attacking or chasing
                }

                if (detectionTime < detectionTimeThreshold)
                {
                    detectionTime += Time.deltaTime;
                    if (detectionTime >= detectionTimeThreshold)
                    {
                        
                        GameManager.instance.CheckDetected(true);
                        Debug.Log("Caught");
                    }
                }

                // Exit the loop if at least one player is detected
                return;
            }
        }

        // If no player is detected, reset the detection state
        if (playerDetected)
        {
            playerDetected = false;
            SetSpotlightColor(originalSpotlightColor);
            detectionTime = 0f;
        }

    }

    //private void PlayerDetectedChecker()
    //{
    //    GameManager.instance.CheckDetected(playerCaught);
    //}

    private void SetSpotlightColor(Color color)
    {
        spotlight.color = color;
    }

    private void Update()
    {
        PlayerInVision();
    }
}
