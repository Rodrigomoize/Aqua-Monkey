using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float currentSpeed;

    public float speed = 12.0f;
    public float sprintSpeed = 18.0f;

    public float recoverySpeed = 6.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 3.0f;
    public bool canJump = true;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public bool isGrounded;
    public bool isMoving;

    private Vector3 lastPosition = Vector3.zero;

    // Movement threshold
    public float movementThreshold = 0.01f;

    public float footstepWalkInterval = 0.8f;
    public float footstepRunInterval = 0.4f;
    private bool isPlayingFootstep = false;

    public Transform cameraTransform;

    // Stamina system
    public float maxStamina = 100.0f;
    public float currentStamina;
    public float sprintStaminaCost = 10.0f;
    public float staminaRecoveryRate = 5.0f;
    public float cooldownDuration = 3.0f;
    public bool isRecoveringStamina = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Ensure cameraTransform is set
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        // Initialize stamina
        currentStamina = maxStamina;
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset velocity Y
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate move direction relative to the camera's forward and right directions
        Vector3 move = cameraTransform.right * x + cameraTransform.forward * z;

        // Sprint functionality
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && !isRecoveringStamina)
        {
            currentSpeed = sprintSpeed;
            currentStamina -= sprintStaminaCost * Time.deltaTime;

            if (currentStamina <= 0)
            {
                StartCoroutine(StaminaCooldown());
            }
        }
        else if(currentStamina <= 0 && isRecoveringStamina)
        {
            currentSpeed = recoverySpeed;
        }
        else
        {
            currentSpeed = speed;
            if (!isRecoveringStamina && currentStamina < maxStamina)
            {
                currentStamina += staminaRecoveryRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            }
        }

        // Move player
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jump functionality
        if (canJump && Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply velocity
        controller.Move(velocity * Time.deltaTime);

        // Check if moving
        if (Vector3.Distance(lastPosition, transform.position) > movementThreshold && isGrounded)
        {
            isMoving = true;
            if (!isPlayingFootstep)
            {
                StartCoroutine(PlayFootsteps());
            }
        }
        else
        {
            isMoving = false;
        }
        lastPosition = transform.position;
    }

    private IEnumerator PlayFootsteps()
    {
        isPlayingFootstep = true;

        while (isMoving)
        {
            // Play footstep sound with slight pitch and volume variation
            AudioManager.Instance.audioSourcePlayerMovement.pitch = Random.Range(0.8f, 1.2f);
            AudioManager.Instance.audioSourcePlayerMovement.volume = Random.Range(0.25f, 0.35f);
            AudioManager.Instance.audioSourcePlayerMovement.Play();
            // Determine the interval based on current speed
            float footstepInterval = (currentSpeed == sprintSpeed) ? footstepRunInterval : footstepWalkInterval;

            // Wait for the interval before playing the next footstep
            yield return new WaitForSeconds(footstepInterval);
        }

        isPlayingFootstep = false;
    }

    private IEnumerator StaminaCooldown()
    {
        isRecoveringStamina = true;
        currentSpeed = speed;

        yield return new WaitForSeconds(cooldownDuration);

        isRecoveringStamina = false;
    }
}