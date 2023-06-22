using UnityEngine.SceneManagement;
using UnityEngine;


public class EnemyVision : MonoBehaviour
{
    public float visionRange = 10f;          // The maximum distance the enemy can see
    public float visionAngle = 60f;          // The field of view angle of the enemy
    public LayerMask targetLayer;            // The layer that the player is on
    public Transform player1, player2;                 // Reference to the player's transform
    // public Light spotlight;
    public float detectionTimeThreshold = 2f;
    // public GameObject restartUI;
    private Color originalSpotlightColor;
    public float originalSpotlightIntensity;
    private bool playerDetected = false;
    private float detectionTime = 0f;
    public bool showRestartUI;
    private Light spotlight;
    public GameObject restartUI;

    void Start()
    {
        spotlight = GetComponentInChildren<Light>();

        originalSpotlightColor = spotlight.color;
        originalSpotlightIntensity = spotlight.intensity;
        restartUI.SetActive(false);
    }
    private void PlayerInVision()
    {
        if (IsPlayer1InVision() || IsPlayer2InVision())
        {
            if (!playerDetected)
            {
                playerDetected = true;
                SetSpotlightColor(Color.yellow);


                // Perform actions when the player is detected, like attacking or chasing
            }
            if (detectionTime < detectionTimeThreshold && !showRestartUI)
            {
                detectionTime += Time.fixedDeltaTime;
                if (detectionTime >= detectionTimeThreshold)
                {
                    Debug.Log("You lose!");
                    showRestartUI = true;
                    restartUI.SetActive(true);
                }
            }
        }
        else
        {
            if (playerDetected)
            {
                playerDetected = false;
                SetSpotlightColor(originalSpotlightColor);
                detectionTime = 0f;
            }
        }
    }
    
    private void Update()
    {
        PlayerInVision();
    }

    private bool IsPlayer1InVision()
    {
        Vector3 directionToPlayer = player1.transform.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleToPlayer <= visionAngle * 0.5f)
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position, directionToPlayer, visionRange);
            bool playerHit = false;

            foreach (RaycastHit hit in hits)
            {
                // Ignore triggers and the enemy's own collider
                if (hit.collider.isTrigger || hit.collider.gameObject == gameObject)
                {
                    continue;
                }

                // Check if the obstacle is between the enemy and the player
                if (hit.collider.CompareTag("Obstacles"))
                {
                    return false;
                }

                // Check if the player is visible
                if (hit.collider.CompareTag("P1Collider"))
                {
                    playerHit = true;
                }
            }

            return playerHit;
        }

        return false;
    }
    private bool IsPlayer2InVision()
    {
        Vector3 directionToPlayer = player2.transform.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleToPlayer <= visionAngle * 0.5f)
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position, directionToPlayer, visionRange);
            bool playerHit = false;

            foreach (RaycastHit hit in hits)
            {
                // Ignore triggers and the enemy's own collider
                if (hit.collider.isTrigger || hit.collider.gameObject == gameObject)
                {
                    continue;
                }

                // Check if the obstacle is between the enemy and the player
                if (hit.collider.CompareTag("Obstacles"))
                {
                    return false;
                }

                // Check if the player is visible
                if (hit.collider.CompareTag("P1Collider"))
                {
                    playerHit = true;
                }
            }

            return playerHit;
        }

        return false;
    }
    private void SetSpotlightColor(Color color)
    {
        spotlight.color = color;
    }


    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
