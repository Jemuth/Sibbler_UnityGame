using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : GameCharacter
{
    private bool isPlayer1Inside;
    private bool isPlayer2Inside;
    private bool wasPlayer1Inside;
    private bool wasPlayer2Inside;
    [SerializeField] private SpriteRenderer closeSpriteRenderer;
    [SerializeField] private AudioSource m_closeSource;
    public AudioClip closeSound;

    private void Start()
    {
        closeSpriteRenderer.enabled = false;
    }
    public void StunEnemyCharacter()
    {
        StartCoroutine(EnemyStun());
    }

    public IEnumerator EnemyStun()
    {
        GameManager.instance.EnemyContact1(false);
        GameManager.instance.EnemyContact2(false);
        yield return new WaitForSeconds(8);
        if(isPlayer1Inside)
        {
            GameManager.instance.EnemyContact1(true);
        }
        if (isPlayer2Inside)
        {
            GameManager.instance.EnemyContact2(true);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("P1"))
        {
            isPlayer1Inside = true;
            closeSpriteRenderer.enabled = true;
            if (!m_closeSource.isPlaying) { 
                m_closeSource.PlayOneShot(closeSound, 0.4F);
            }
        }
        if (other.CompareTag("P2"))
        {
            isPlayer2Inside = true;
            closeSpriteRenderer.enabled = true;
            if (!m_closeSource.isPlaying)
            {
                m_closeSource.PlayOneShot(closeSound, 0.4F);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("P1"))
        {
            isPlayer1Inside = false;
            closeSpriteRenderer.enabled = false;
        }
        if (other.CompareTag("P2"))
        {
            isPlayer2Inside = false;
            closeSpriteRenderer.enabled = false;
        }
    }
    private void RaiseDetectionP1()
    {
        if (isPlayer1Inside != wasPlayer1Inside)
        {
            if (isPlayer1Inside)
            {
                GameManager.instance.EnemyContact1(true);
            }
            else
            {
                GameManager.instance.EnemyContact1(false);
            }

            wasPlayer1Inside = isPlayer1Inside;
        }
    }
    private void RaiseDetectionP2()
    {
        if (isPlayer2Inside != wasPlayer2Inside)
        {
            if (isPlayer2Inside)
            {
                GameManager.instance.EnemyContact2(true);
            }
            else
            {
                GameManager.instance.EnemyContact2(false);
            }

            wasPlayer2Inside = isPlayer2Inside;
        }
    }

    private void Update()
    {
        RaiseDetectionP1();
        RaiseDetectionP2();
    }
}
