using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distraction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer prefabSpriteRenderer;
    [SerializeField] private AudioSource m_distractionSource;

    private void OnTriggerEnter(Collider other)
    {
        EnemyPatrol enemy = other.GetComponent<EnemyPatrol>();
        if (enemy != null)
        {
            prefabSpriteRenderer.enabled = true;
            if (!m_distractionSource.isPlaying)
            {
                m_distractionSource.Play();
            }
        } else
        {
            prefabSpriteRenderer.enabled = false;
        }
    }
}
