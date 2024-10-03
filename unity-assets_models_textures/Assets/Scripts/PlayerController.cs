using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpPower = 10f;
    public float rotationSpeed = 720f; // Speed of rotation per second
    public bool isOnGround;

    private CharacterController conn;
    private float ySpeed;

    void Start()
    {
        conn = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get input from player
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        // Movement along the local X and Z axes
        Vector3 moveDirection = transform.right * horizontalMove + transform.forward * verticalMove;
        moveDirection.Normalize(); // Normalize to prevent faster diagonal movement

        // Move the character
        conn.SimpleMove(moveDirection * speed);

        // Rotate the character to face the movement direction, if moving
        if (moveDirection != Vector3.zero)
        {
            // Rotate the player smoothly towards the direction of movement
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Apply gravity and handle jumping
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (conn.isGrounded)
        {
            ySpeed = -0.5f; // Reset vertical speed if grounded
            isOnGround = true;

            // Handle jump input
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpPower; // Apply jump power
                isOnGround = false;
            }
        }

        // Apply movement in the Y-axis (for jump and gravity)
        Vector3 velocity = new Vector3(0, ySpeed, 0);
        conn.Move(velocity * Time.deltaTime);
    }
}