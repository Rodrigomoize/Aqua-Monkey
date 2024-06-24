using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealisticCameraMovement : MonoBehaviour
{
    public Transform playerBody; // Reference to the player's body
    public Transform cameraTransform; // Reference to the camera

    public float mouseSensitivity = 100.0f;
    public float rotationSmoothTime = 0.12f;

    // Head position offset
    public Vector3 headPositionOffset = new Vector3(0f, 1.82f, 0f); // Adjust this offset as needed

    // Head bobbing variables
    public float headBobFrequency = 2.0f;
    public float headBobAmount = 0.05f;
    private float headBobTimer = 0.0f;

    // Leaning variables
    public float leanAngle = 30.0f;
    private float currentLeanAngle = 0.0f;

    private Vector2 currentRotation;
    private Vector2 rotationSmoothVelocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentRotation.y = playerBody.eulerAngles.y;

        // Position the camera on the player's head initially
        cameraTransform.position = playerBody.position + headPositionOffset;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        currentRotation.x -= mouseY;
        currentRotation.y += mouseX;

        // Clamp vertical rotation
        currentRotation.x = Mathf.Clamp(currentRotation.x, -90f, 90f);

        // Smooth rotation
        Vector2 smoothRotation = Vector2.SmoothDamp(new Vector2(cameraTransform.localEulerAngles.x, playerBody.localEulerAngles.y), currentRotation, ref rotationSmoothVelocity, rotationSmoothTime);
        
        // Apply rotation to camera and player body
        cameraTransform.localRotation = Quaternion.Euler(smoothRotation.x, 0f, 0f);
        playerBody.localRotation = Quaternion.Euler(0f, smoothRotation.y, 0f);

        // Head bobbing
        HandleHeadBob();

        // Leaning mechanic
        HandleLean();
    }

    void HandleHeadBob()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        // Calculate head bob frequency based on movement speed
        float bobSpeed = Mathf.Sqrt(horizontalMovement * horizontalMovement + verticalMovement * verticalMovement);

        // Update head bob timer based on movement speed
        headBobTimer += bobSpeed * headBobFrequency * Time.deltaTime;

        // Calculate vertical head bob offset
        float headBobOffset = Mathf.Sin(headBobTimer) * headBobAmount;

        // Apply head bob to camera's local position
        Vector3 newPos = cameraTransform.localPosition;
        newPos.y = headPositionOffset.y + headBobOffset; // Adjust vertical position to include head position offset
        cameraTransform.localPosition = newPos;
    }

    void HandleLean()
    {
        float leanInput = Input.GetAxis("Lean");

        // Smoothly adjust current lean angle
        currentLeanAngle = Mathf.Lerp(currentLeanAngle, leanInput * leanAngle, Time.deltaTime * 10f);

        // Apply lean angle to camera's local rotation around the z-axis
        cameraTransform.localRotation = Quaternion.Euler(cameraTransform.localRotation.eulerAngles.x, cameraTransform.localRotation.eulerAngles.y, -currentLeanAngle);
    }
}