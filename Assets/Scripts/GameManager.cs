using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private CharacterSwap m_player;
    [SerializeField] private CameraSwap m_camera;
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
}
