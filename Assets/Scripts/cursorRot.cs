using UnityEngine;

public class CursorRot : MonoBehaviour
{
    public Transform playerBody; // Reference to the player body (parent of the camera)
    public float mouseSensitivity = 100f; // Mouse sensitivity
    public float rotationSpeed = 5f; // Speed of movement

    public float maxVerticalAngle = 80f; // Max vertical angle for camera pitch
    public float minVerticalAngle = -30f; // Min vertical angle for camera pitch

    private float xRotation = 0f; // Vertical camera rotation (pitch)

    void Start()
    {
        // Lock and hide the cursor in the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Handle camera rotation based on mouse movement
        HandleCameraRotation();

        // Move player
        MovePlayer();
    }

    void HandleCameraRotation()
    {
        // Get the mouse input for X and Y axis
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust vertical rotation (camera pitch)
        xRotation -= mouseY; // Vertical mouse movement
        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle); // Clamp vertical rotation

        // Apply rotation to the camera (local pitch) and player body (yaw)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Camera pitch
        playerBody.Rotate(Vector3.up * mouseX); // Player yaw
    }

    void MovePlayer()
    {
        // Get movement input (WASD or arrow keys)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = (playerBody.right * horizontal + playerBody.forward * vertical).normalized;

        // Move the player
        playerBody.Translate(moveDirection * rotationSpeed * Time.deltaTime, Space.World);
    }
}
