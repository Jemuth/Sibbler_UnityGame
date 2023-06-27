using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private CharacterSwap m_player;
    [SerializeField] private CameraSwap m_camera;
    [SerializeField] private StaminaManager m_runPressed1, m_runPressed2;
    [SerializeField] private Player1 m_checkHit;
    [SerializeField] private UIManager m_uiManager;
    [SerializeField] private KeyCollection m_key;
    [SerializeField] private UIManager m_checkKeys;
    [SerializeField] private UIManager m_checkPlayers;
    public EnemyVision[] enemyCharacterScripts;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void ChangeEnabler(bool p_change)
    {
        m_player.ChangeEnabled(p_change);
    }
    public void CameraChange(bool p_change)
    {
        m_camera.CameraChangeEnabled(p_change);
    }
    public void IsRunPressed(bool p_runPressed)
    {
        m_runPressed1.RunPressed(p_runPressed);
        m_runPressed2.RunPressed(p_runPressed);
    }
    public void CheckEnemyHitable(bool m_checkEnemyHit)
    {
        m_checkHit.CheckHitable(m_checkEnemyHit);
    }
    public void CheckDetected(bool m_checkDetected)
    {
        m_uiManager.CheckRestart(m_checkDetected);
    }
    public void CheckKeyCollected(bool m_keyCollected)
    {
        m_key.CollectKey(m_keyCollected);
    }
    public void CheckAllKeys(bool m_currentKeys)
    {
        m_checkKeys.CheckKeys(m_currentKeys);
    }
    public void CheckPlayersExit(bool m_playersOnExit)
    {
        m_checkPlayers.CheckPlayersOnExit(m_playersOnExit);
    }
    public void PlayerHitEnemy(int enemyID)
    {
        foreach (EnemyVision enemy in enemyCharacterScripts)
        {
            if (enemy.enemyID == enemyID)
            {
                enemy.EnemyHitChecker(true);
            }
        }
    }
}
