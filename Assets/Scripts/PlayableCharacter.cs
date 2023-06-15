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

    // Velocity and acceleration parameters
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    private float acceleration = 4f;
    private float deceleration = 3f;
    private float maxRunVelocity = 2f;
    private float maxWalkVelocity = 0.5f;
    int VelocityZHash;
    int VelocityXHash;

    // Run and stamina bar parameters
    [SerializeField] private float maxWalkSpeed;
    [SerializeField] private float maxRunSpeed;
    private float initialSpeed;
    [SerializeField] private float currentWalkSpeed;
    [SerializeField] private float currentRunSpeed;
    private bool isWalking;
    private bool isRunning;
    private float accelerationTime;
    public StaminaBar m_staminaStatus;
    public bool runEnabled;

    private void Awake()
    {
        animator.SetBool("isTired", false);
    }
    private void Start()
    {
        //Animator parameters
        animator = GetComponent<Animator>();
        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");
        

        //Speed parameters
        initialSpeed = 0;
        accelerationTime = m_data.acceleration;
        maxWalkSpeed = (0.5f * m_data.speedMultiplier);
        maxRunSpeed = (2f + m_data.speedMultiplier);
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
    
    // Actual player movement
    public void PlayerMovement()
    {
        // Speed Lerp

        float movementInputZ = Input.GetAxisRaw("Vertical");
        float movementInputX = Input.GetAxisRaw("Horizontal");
        //Walk Lerp
        if ((movementInputZ != 0 || movementInputX !=0) && !Input.GetKey(KeyCode.LeftShift))
        {
            isWalking = true;
            currentWalkSpeed = Mathf.Lerp(currentWalkSpeed, maxWalkSpeed, accelerationTime * Time.deltaTime);
        } else
        {
            isWalking = false;
            currentWalkSpeed = 0f;
        }
        //Run Lerp
        if ((movementInputZ != 0 || movementInputX != 0) && Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            currentRunSpeed = Mathf.Lerp(currentRunSpeed, maxRunSpeed, accelerationTime * Time.deltaTime);
        }
        else
        {
            isRunning = false;
            currentRunSpeed = 0f;
        }
        // Walking
        // Forward and back
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            
            transform.position += transform.forward * currentWalkSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            
            transform.position -= transform.forward * currentWalkSpeed * Time.deltaTime;
        }
        // If forward and back are pressed at the same time, just walk forward
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W))
        {
            
            transform.position += transform.forward * currentWalkSpeed * Time.deltaTime;
        }
        // Left and right
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            
            transform.position += transform.right * currentWalkSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            
            transform.position -= transform.right * currentWalkSpeed * Time.deltaTime;
        }
        // If left and right are pressed at the same time, just walk right
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            
            transform.position += transform.right * currentWalkSpeed * Time.deltaTime;
        }
        // Running
        // Run forward and backward
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S) && runEnabled)
        {
            transform.position += transform.forward * currentRunSpeed * Time.deltaTime;
        } 
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.W) && runEnabled)
        {
            transform.position -= transform.forward * currentRunSpeed * Time.deltaTime;
        }
        // If forward and back are pressed at the same time, just run forward
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && runEnabled)
        {
            transform.position += transform.forward * currentRunSpeed * Time.deltaTime;
        }
        // Run left and right
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.A) && runEnabled)
        {
            transform.position += transform.right * currentRunSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.D) && runEnabled)
        {
            transform.position -= transform.right * currentRunSpeed * Time.deltaTime;
        }
        // If left and right are pressed at the same time, just run right
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) && runEnabled)
        {
            transform.position += transform.right * currentRunSpeed * Time.deltaTime;
        }
    }
    // Only animation velocity!
    private void ChangeVelocity(bool forwardPressed, bool rightPressed, bool leftPressed, bool backPressed, bool runPressed, float currentMaxVelocity)
    {
        // Increase velocity according to inputs
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

    // Only applies for animation!
    private void LockOrResetVelocity(bool forwardPressed, bool rightPressed, bool leftPressed, bool backPressed, bool runPressed, float currentMaxVelocity)
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
    private void RunningAnimation()
    {
        if ((!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.LeftShift) && !runEnabled)
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
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && runEnabled == true && Input.GetKey(KeyCode.LeftShift))
        {
            
            StaminaBar.instance.UseStamina(2);

        }
    }

    void Update()
    {
        // Animation and inputs
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool backPressed = Input.GetKey(KeyCode.S);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;
        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);
        ChangeVelocity(forwardPressed, rightPressed, leftPressed, backPressed, runPressed, currentMaxVelocity);
        LockOrResetVelocity(forwardPressed, rightPressed, leftPressed, backPressed, runPressed, currentMaxVelocity);
        RunningAnimation();
        // Player movement
        PlayerMovement();
        RotatePlayer(GetPlayerRotation());
        // Stamina bar
        UseStamina();
        RunCooldown();

    }
}
