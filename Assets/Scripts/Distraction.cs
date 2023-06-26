using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distraction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer prefabSpriteRenderer;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is an enemy
        EnemyPatrol enemy = other.GetComponent<EnemyPatrol>();
        if (enemy != null)
        {
            // Change the sprite renderer's sprite to the distracted sprite
            prefabSpriteRenderer.enabled = true;
        } else
        {
            prefabSpriteRenderer.enabled = false;
        }
    }
}
