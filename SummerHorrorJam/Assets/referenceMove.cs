using UnityEngine;

public class referenceMove : MonoBehaviour
{
    // Reference to the player's transform
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Find the player GameObject and get its transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            // Calculate the target position with an offset of -500 on the X-axis
            Vector3 targetPosition = playerTransform.position + new Vector3(-500f, 0f, 0f);

            // Teleport the object to the target position
            transform.position = targetPosition;
        }
    }
}