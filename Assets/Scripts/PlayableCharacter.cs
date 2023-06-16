using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayableCharacter : GameCharacter
{
    [SerializeField] private float playerRotationSpeed = 60;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayableCharacterData m_data;
    // Velocity and acceleration parameters for animation only
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    private float acceleration = 4f;
    private float deceleration = 3f;
    private float maxRunVelocity = 2f;
    private float maxWalkVelocity = 0.5f;
    int VelocityZHash;
    int VelocityXHash;
    private float currentMaxVelocity;
    bool forwardPressed;
    bool backPressed;
    bool leftPressed;
    bool rightPressed;
    // Run button for both input and animation
    bool runPressed;
    // Movement speed parameters
    [SerializeField] private float maxWalkSpeed;
    [SerializeField] private float maxRunSpeed;
    [SerializeField] private float currentSpeed;
    private float accelerationTime;
    private float decelerationTime;
    private float initialSpeed = 0;
    private float elapsedAcceleration;
    private float elapsedDeceleration;
    private float accelerationPercentage;
    private float decelerationPercentage;
    // Stamina bar and run bool for enabling runnning
    [SerializeField] private StaminaBar m_staminaStatus;
    private bool runEnabled;
    // Bool for changing character
    private bool canChange;
    private void Awake()
    {
        animator.SetBool("isTired", false);
    }
    private void Start()
    {
        currentSpeed = initialSpeed;
        SpeedParameters();
        VelocityHash();
        canChange = true;
    }
    // Enable change only when speed equals 0 to avoid moving in placing
    public void CanChange()
    {
        if ((Input.GetAxis("Vertical") ==0 || Input.GetAxis("Horizontal") == 0) && currentSpeed == 0)
        {
            canChange = true;
            GameManager.instance.ChangeEnabler(canChange);
            GameManager.instance.CameraChange(canChange);

        } else
        {
            canChange = false;
            GameManager.instance.ChangeEnabler(canChange);
            GameManager.instance.CameraChange(canChange);
        }
    }
    private void VelocityHash()
    {
        animator = GetComponent<Animator>();
        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        Cursor.visible = !hasFocus;
        Cursor.lockState = hasFocus ? CursorLockMode.None : CursorLockMode.Confined;
    }
    // Player movement, speed and rotation
    private void SpeedParameters()
    {
        // Parameters from scriptable object
        accelerationTime = m_data.acceleration;
        decelerationTime = m_data.deceleration;
        maxWalkSpeed = m_data.walkSpeed;
        maxRunSpeed = m_data.runSpeed;
        // Lerp duration
        elapsedAcceleration += Time.deltaTime;
        accelerationPercentage = elapsedAcceleration / accelerationTime;
        elapsedDeceleration += Time.deltaTime;
        decelerationPercentage = elapsedDeceleration / decelerationTime;
    }
    private void SpeedLerp()
    {
        //Speed Lerp for walking and running
        float movementInputZ = Input.GetAxis("Vertical");
        float movementInputX = Input.GetAxis("Horizontal");
        if ((movementInputZ != 0 || movementInputX !=0) && !runPressed)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxWalkSpeed, accelerationPercentage);
        } else if ((movementInputZ != 0 || movementInputX != 0) && runPressed && runEnabled)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxRunSpeed, accelerationPercentage);
        } else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, initialSpeed, decelerationPercentage);
        }
        if (currentSpeed < 0.1f) 
        {
            currentSpeed = initialSpeed;
        }
    }
    private Vector3 GetMovementInput()
    {
        var l_horizontal = Input.GetAxis("Horizontal");
        var l_vertical = Input.GetAxis("Vertical");
        return new Vector3(l_horizontal, 0, l_vertical).normalized;
    }
    private void Move(Vector3 p_inputMovement)
    {
        var transform1 = transform;
        transform1.position += (p_inputMovement.z * transform1.forward + p_inputMovement.x * transform1.right) *
                               (currentSpeed * Time.deltaTime);
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
    // Animation settings
    // Setting animation inputs
    private void AnimationInputs()
    {
        forwardPressed = Input.GetAxis("Vertical") > 0;
        backPressed = Input.GetAxis("Vertical") < 0;
        leftPressed = Input.GetAxis("Horizontal") < 0;
        rightPressed = Input.GetAxis("Horizontal") > 0;
        runPressed = Input.GetKey(KeyCode.LeftShift);
    }
    // Change velocity if run button pressed. String to hash velocity values
    private void CurrentVelocity()
    {
        currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;
        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);
    }
    // Increase or decrease velocity according to inputs
    private void ChangeVelocity()
    {
        // Increase velocityZ going forward
        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        // Decrease velocityZ if forward is not pressed
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }
        // Increase velocityZ going backwards
        if (backPressed && velocityZ > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        // Decrease velocityZ if backrward is not pressed
        if (!backPressed && velocityZ < 0.0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }
        // If forward and back are pressed at the same time while walking, just walk forward
        if (backPressed && forwardPressed && !runPressed)
        {
            velocityZ = 0.5f;
        }
        // If forward and back are pressed at the same time while running, just run forward
        if (backPressed && forwardPressed && runPressed)
        {
            velocityZ = 2f;
        }
        // Increase velocityX going right
        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }
        // Increase velocityX going left
        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        // Increase velocityX if left is not pressed and velocityX < 0
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        // Decrease velocityX if right is not pressed and velocityX > 0
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
        // If left and right are pressed at the same time while walking, just walk right
        if (leftPressed && rightPressed && !runPressed)
        {
            velocityX = 0.5f;
        }
        // If left and right are pressed at the same time while running, just run right
        if (leftPressed && rightPressed && runPressed)
        {
            velocityX = 2f;
        }
    }
    // Reset or lock velocities for animation
    private void LockOrResetVelocity()
    {
        // Reset velocities
        // Reset velocityZ
        if (!forwardPressed && !backPressed && velocityZ != 0.0f && (velocityZ > -0.05f && velocityZ < 0.05f))
        {
            velocityZ = 0.0f;
        }
        // Reset velocityX
        if (!leftPressed && !rightPressed && velocityZ != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }
        // Locking speeds acording to max velocity
        // Lock forward speed
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        // Decelerate to walk speed
        else if (forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            // Round to the current max velocity if within offset
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        // Round to the current max velocity if within offset
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {
            velocityZ = currentMaxVelocity;
        }
        // Lock backward speed
        if (backPressed && runPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ = -currentMaxVelocity;
        }
        // Decelerate to walk speed
        else if (backPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * deceleration;
            // Round to the current max velocity if within offset
            if (velocityZ < -currentMaxVelocity && velocityZ > (-currentMaxVelocity - 0.05f))
            {
                velocityZ = -currentMaxVelocity;
            }
        }
        // Round to the current max velocity if within offset
        else if (backPressed && velocityZ > -currentMaxVelocity && velocityZ < (-currentMaxVelocity + 0.05f))
        {
            velocityZ = -currentMaxVelocity;
        }
        // Locking left speed
        if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }
        // Decelerate to walk speed
        else if (leftPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
            // Round to the current max velocity if within offset
            if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
            {
                velocityX = -currentMaxVelocity;
            }
        }
        // Round to the current max velocity if within offset
        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
        {
            velocityX = -currentMaxVelocity;
        }
        // Locking right speed
        if (rightPressed && runPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        // Decelerate to walk speed
        else if (rightPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            // Round to the current max velocity if within offset
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f))
            {
                velocityX = currentMaxVelocity;
            }
        }
        // Round to the current max velocity if within offset
        else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
        {
            velocityX = currentMaxVelocity;
        }
    }
    // Stamina and run settings
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
    private void RunningAnimation()
    {
        if (runPressed && !runEnabled)
        {
            maxRunVelocity = 0f;
            animator.SetBool("isTired", true);
            
        } else
        {
            maxRunVelocity = 2f;
            animator.SetBool("isTired", false);

        }
    }
    private void UseStamina()
    {
        if ((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) && runEnabled == true && runPressed)
        {
            
            StaminaBar.instance.UseStamina(2);

        }
    }
    void Update()
    {
        // Animation inputs and velocity changes
        AnimationInputs();
        CurrentVelocity();
        ChangeVelocity();
        LockOrResetVelocity();
        RunningAnimation();
        // Player speed movement
        SpeedLerp();
        Move(GetMovementInput());
        RotatePlayer(GetPlayerRotation());
        // Stamina bar
        UseStamina();
        RunCooldown();
        // Character changer
        CanChange();
    }
}
