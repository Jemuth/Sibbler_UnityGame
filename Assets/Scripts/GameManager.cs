using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private CharacterSwap m_player;
    [SerializeField] private CameraSwap m_camera;
    [SerializeField] private StaminaManager m_stamina;
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
    public void CheckStamina(int p_checkStamina)
    {
        m_stamina.UseStamina(p_checkStamina);
    }
}
