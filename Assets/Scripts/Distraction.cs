using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distraction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer prefabSpriteRenderer;

    private void OnTriggerEnter(Collider other)
    {
        EnemyPatrol enemy = other.GetComponent<EnemyPatrol>();
        if (enemy != null)
        {
            prefabSpriteRenderer.enabled = true;
        } else
        {
            prefabSpriteRenderer.enabled = false;
        }
    }
}
