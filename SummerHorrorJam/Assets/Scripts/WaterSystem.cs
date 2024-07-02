using UnityEngine;

public class WaterSystem : MonoBehaviour
{
    public float moveSpeed = 0.5f; // Speed of movement
    private Vector3 targetPosition; // The target position to move towards
    private bool isMoving = false; // Whether the object is currently moving

    void Start()
    {
        // Initialize the target position to the current position
        targetPosition = transform.position;
    }

    void Update()
    {
        // Check for input and set the target position
        if (Input.GetKeyDown(KeyCode.U))
        {
            targetPosition = new Vector3(transform.position.x, 11f, transform.position.z);
            isMoving = true;
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            targetPosition = new Vector3(transform.position.x, 1f, transform.position.z);
            isMoving = true;
        }

        // Move towards the target position if the object should be moving
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the object has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }
}