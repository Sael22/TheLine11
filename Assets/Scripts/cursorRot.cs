using UnityEngine;

public class cursorRot : MonoBehaviour
{

    public Transform playerBody; // Reference to the player's body
    public Camera playerCamera; // Reference to the camera
    public float mouseSensitivity = 100f; // Mouse sensitivity
    public float rotationSpeed = 5f; // Speed of rotation

    public float maxVerticalAngle = 80f; // Max vertical angle for camera pitch
    public float minVerticalAngle = -30f; // Min vertical angle for camera pitch
    public float cameraDistance = 5f; // How far the camera is from the player
    public float cameraHeight = 2f; // How high the camera is from the player

    private float xRotation = 0f; // Vertical camera rotation (pitch)
    private float yRotation = 0f; // Horizontal player body rotation (yaw)

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
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Adjust vertical rotation (camera pitch)
        xRotation -= mouseY * mouseSensitivity * Time.deltaTime; // Vertical mouse movement
        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle); // Clamp vertical rotation

        // Apply vertical rotation to the camera
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Adjust horizontal rotation (player body rotation)
        yRotation += mouseX * mouseSensitivity * Time.deltaTime; // Horizontal mouse movement
        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f); // Apply rotation to the player body

        // Rotate camera around the player horizontally (Y-axis)
        Vector3 cameraOffset = new Vector3(0f, cameraHeight, -cameraDistance); // Adjusted position with cameraHeight
        Quaternion rotation = Quaternion.Euler(0f, yRotation, 0f); // Camera's horizontal rotation
        playerCamera.transform.position = playerBody.position + rotation * cameraOffset;
        playerCamera.transform.LookAt(playerBody.position); // Ensure the camera looks at the player
    }

    void MovePlayer()
    {
        // Get movement input (WASD or arrow keys)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = (transform.right * horizontal + transform.forward * vertical).normalized;

        // Move the player
        playerBody.Translate(moveDirection * rotationSpeed * Time.deltaTime, Space.World);
    }
}


