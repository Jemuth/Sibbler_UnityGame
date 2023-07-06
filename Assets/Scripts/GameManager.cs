using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private CharacterSwap m_player;
    [SerializeField] private CameraSwap m_camera;
    [SerializeField] private StaminaManager m_runPressed1, m_runPressed2;
    [SerializeField] private UIManager m_uiManager;
    [SerializeField] private KeyCollection m_key;
    [SerializeField] private UIManager m_checkKeys;
    [SerializeField] private UIManager m_checkPlayers;
    [SerializeField] private SkillCooldownP1 m_skillUsed;
    [SerializeField] private SkillCooldownP2 m_skillUsed2;
    [SerializeField] private DetectionManager m_detected1;
    [SerializeField] private DetectionManager m_detected2;
    //[SerializeField] private Player1 m_checkDetectedP1;
    //[SerializeField] private Player2 m_checkDetectedP2;
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
    public void Player1SkillUsed(bool skillUsed)
    {
        m_skillUsed.UISkillChecker(skillUsed);
    }
    public void Player2SkillUsed(bool skillUsed)
    {
        m_skillUsed2.UISkillChecker2(skillUsed);
    }
    public void DetectionBar1(bool p_detected)
    {
        m_detected1.DetectionBarChecker(p_detected);
    }
    public void DetectionBar2(bool p_detected)
    {
        m_detected2.DetectionBarChecker(p_detected);
    }
    public void EnemyContact1(bool p_contact)
    {
        m_detected1.ContactChecker(p_contact);
    }
    public void EnemyContact2(bool p_contact)
    {
        m_detected2.ContactChecker(p_contact);
    }
    //public void CheckDetectedP1(bool p_checkDetected)
    //{
    //    m_checkDetectedP1.CheckDetected(p_checkDetected);
    //}
}
