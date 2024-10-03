using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float distanceFromTarget = 5f; // Distance behind the player
    public float heightOffset = 2f; // Height above the player
    public float smoothTime = 0.3f; // Time to smooth the movement
    public float rotationSmoothTime = 0.1f; // Time to smooth the rotation
    public float mouseSensitivity = 100f; // Sensitivity for mouse rotation
    public float maxVerticalAngle = 80f; // Limit for how far the camera can look up or down

    private Vector3 currentVelocity = Vector3.zero;
    private float _pitch = 0f; // Vertical rotation (X-axis)

    private float cameraStart = 179.0f;

    void Start() {
        transform.rotation = Quaternion.Euler(transform.rotation.x, cameraStart, transform.rotation.z);
    }
    void LateUpdate()
    {
        HandleMouseRotation();

        // Calculate desired position behind the player
        Vector3 targetPosition = player.position - player.forward * distanceFromTarget + Vector3.up * heightOffset;

        // Smoothly move the camera to the desired position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // Rotate the camera to look at the player
        transform.LookAt(player);
    }

    private void HandleMouseRotation()
    {
        // Only rotate the camera if the right mouse button is held down
        if (Input.GetMouseButton(1)) // 1 is for right mouse button
        {
            // Get the mouse input (X-axis for horizontal rotation, Y-axis for vertical rotation)
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Update the pitch (vertical rotation) based on mouse input and clamp it
            _pitch -= mouseY;
            _pitch = Mathf.Clamp(_pitch, -maxVerticalAngle, maxVerticalAngle); // Clamp to prevent flipping

            // Apply horizontal rotation to the player (yaw) and vertical rotation (pitch) to the camera
            player.Rotate(Vector3.up * mouseX); // Horizontal rotation on player
        }

        // Apply the pitch (vertical rotation) to the camera's local rotation
        Quaternion cameraRotation = Quaternion.Euler(_pitch, player.eulerAngles.y, 0);
        transform.rotation = cameraRotation;
    }

    // This method calculates movement direction relative to the camera
    // public Vector3 CalculateCameraRelativeMovement(float horizontalMove, float verticalMove)
    // {
    //     // Get the camera's forward and right directions
    //     Vector3 cameraForward = transform.forward;
    //     Vector3 cameraRight = transform.right;

    //     // We only care about movement on the XZ plane, so zero out the Y component
    //     cameraForward.y = 0;
    //     cameraRight.y = 0;

    //     // Normalize directions
    //     cameraForward.Normalize();
    //     cameraRight.Normalize();

    //     // Calculate movement direction relative to the camera
    //     Vector3 moveDirection = cameraForward * verticalMove + cameraRight * horizontalMove;
    //     moveDirection.Normalize(); // Normalize to avoid faster diagonal movement

    //     return moveDirection;
    // }
}