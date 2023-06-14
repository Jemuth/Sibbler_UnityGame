using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : GameCharacter
{
    [SerializeField] private float playerRotationSpeed = 60;
    [SerializeField] private Animator m_animator;
    [SerializeField] private PlayableCharacterData m_data;
    private float m_baseSpeed = 1;
    public StaminaBar m_staminaStatus;
    public bool runEnabled;


    private void OnApplicationFocus(bool hasFocus)
    {
        Cursor.visible = !hasFocus;
        Cursor.lockState = hasFocus ? CursorLockMode.None : CursorLockMode.Confined;
    }
    public virtual float GetCurrentSpeed()
    {
        return m_data.speedMultiplier;
    }

    protected Vector3 GetPlayerMovementInput()
    {
        var l_horizontal = Input.GetAxis("Horizontal");
        var l_vertical = Input.GetAxis("Vertical");
        return new Vector3(l_horizontal, 0, l_vertical).normalized;

    }
    private Vector3 GetPlayerRotation()
    {
        var l_mouseX = Input.GetAxis("Mouse X");
        var l_mouseY = Input.GetAxis("Mouse Y");
        return new Vector2(l_mouseX, l_mouseY);
    }
    private void MovePlayer(Vector3 p_inputMovement)
    {
        var transform1 = transform;
        transform1.position += (p_inputMovement.z * transform1.forward + p_inputMovement.x * transform1.right) *
                               (m_baseSpeed * m_data.speedMultiplier * Time.deltaTime);
        m_animator.SetFloat("Speed", p_inputMovement.magnitude);
    }
    private void RotatePlayer(Vector2 p_scrollDelta)
    {
        transform.Rotate(Vector3.up, p_scrollDelta.x * playerRotationSpeed * Time.deltaTime, Space.Self);
    }
    private void RunCooldown()
    {
        if(m_staminaStatus.currentStamina <= 0)
        {
            runEnabled = false;
        } else
        {
            runEnabled = true;
        }
    }
    private void Running()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0 && runEnabled == true)
        {
            m_animator.SetFloat("Speed", 2f);
            m_baseSpeed = 2.5f;
            StaminaBar.instance.UseStamina(2);
            // stamina -= (staminaRate * 2) * Time.deltaTime;

        }
        else
        {
            m_baseSpeed = 1f;
        }
    }
    
    void Update()
    {
        MovePlayer(GetPlayerMovementInput());
        RotatePlayer(GetPlayerRotation());
        Running();
        RunCooldown();
        // Moving();

    }
}
