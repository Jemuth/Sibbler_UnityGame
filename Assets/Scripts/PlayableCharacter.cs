using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : GameCharacter
{
    [SerializeField] private float playerRotationSpeed = 60;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayableCharacterData m_data;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2f;
    public float deceleration = 2f;
    public float maxWalkVelocity = 0.5f;
    public float maxRunVelocity;
    float currentMaxVelocity;

    public StaminaBar m_staminaStatus;
    public bool runEnabled;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        Cursor.visible = !hasFocus;
        Cursor.lockState = hasFocus ? CursorLockMode.None : CursorLockMode.Confined;
    }
    public virtual float GetCurrentSpeed()
    {
        return m_data.speedMultiplier;
    }

    private void PlayerMovement()
    {
        // Inputs
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool backPressed = Input.GetKey(KeyCode.S);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        // set current maxVelocity
  
        if(!backPressed && runPressed && m_staminaStatus.currentStamina > m_data.maxStamina * 5 / 100)
        {
            currentMaxVelocity = maxRunVelocity;
            maxRunVelocity = 2f;
        } else
        {
            currentMaxVelocity = maxWalkVelocity;
            maxRunVelocity = 0.5f;
        }

        // Decelarate low stamina

        if(runEnabled == false && velocityZ > 0.5f)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        if (runEnabled == false && velocityX > 0.5f)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        if (runEnabled == false && velocityX < -0.5f)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        // Increase velocity according to inputs

        // Forward velocity
        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        // Back Velocity
        if (backPressed && velocityZ > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        // Left velocity
        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        // Right velocity
        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }
        // Decrease velocityZ
        if (!forwardPressed && !backPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }
     
        // Reset velocityZ
        if (!forwardPressed && !backPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }
        // Increase velocityX if left is not pressed and velocityX < 0
        if(!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        // Decrease velocityX if right is not pressed and velocityX > 0
        if(!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
        // reset velocityx
        if(!leftPressed && !rightPressed && velocityZ != 0.0f &&(velocityX > -0.05f && velocityX < 0.05f))
        { 
            velocityX = 0.0f; 
        }
        // lock forward
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        // decelerate to walk speed
        else if (forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            // round to the currentmaxvelocity
            if(velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        // round to the currentmaxvelocity
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {
            velocityZ = currentMaxVelocity;
        }

        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);
    }
    
    private Vector3 GetPlayerRotation()
    {
        var l_mouseX = Input.GetAxis("Mouse X");
        var l_mouseY = Input.GetAxis("Mouse Y");
        return new Vector2(l_mouseX, l_mouseY);
    }
   
    private void RotatePlayer(Vector2 p_scrollDelta)
    {
        transform.Rotate(Vector3.up, p_scrollDelta.x * playerRotationSpeed * Time.deltaTime, Space.Self);
    }
    private void RunCooldown()
    {
        if (m_staminaStatus.currentStamina < m_data.maxStamina * 5 / 100)
        {
            runEnabled = false;
        }
        else
        {
            runEnabled = true;
        }
    }
    private void UseStamina()
    {
        if (Input.GetKey(KeyCode.LeftShift) && runEnabled == true && velocityZ > 0)
        {
            
            StaminaBar.instance.UseStamina(2);

        }
    }

    void Update()
    {
        PlayerMovement();
        RotatePlayer(GetPlayerRotation());
        UseStamina();
        RunCooldown();

    }
}
