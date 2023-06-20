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
    public void CheckEnemyDistance(bool m_checkEnemyHit)
    {
        m_checkHit.DistanceChecker(m_checkEnemyHit);
    }

}
