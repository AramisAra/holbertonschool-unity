using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 10;
    public Transform cam;
    public LayerMask groundLayer;
    public bool onGround;
    float gravity = 9.8f;
    Vector2 _move;
    Vector3 move, start;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        start = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3.up * -0.4f), 0.7f);
    }

    void FixedUpdate()
    {
        onGround = Physics.CheckSphere(transform.position + (Vector3.up * -0.4f), 0.7f, groundLayer);

        if (!onGround)
            rb.AddForce(Vector3.up * -gravity, ForceMode.Acceleration);
        if (_move.x != 0 || _move.y != 0)
        {
            move = cam.forward * _move.y + cam.right * _move.x;
            move.y = 0;
            move.Normalize();

            Vector3 horizontalVelocity = move * speed;
            rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
        }

        if (transform.position.y < -10)
            transform.position = new Vector3(start.x, start.y * 20, start.z);
    }
    public void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
        if (_move.x == 0 && _move.y == 0)
            rb.velocity = Vector3.zero;
    }
    public void OnJump()
    {
        Vector2 moveDir = _move;

        if (onGround)
        {
            if (moveDir != Vector2.zero)
            {
                Vector3 jumpDir = cam.forward * moveDir.y + cam.right * moveDir.x;
                jumpDir.y = 0;
                jumpDir.Normalize();
                Quaternion targetR = Quaternion.LookRotation(jumpDir);
                transform.rotation = targetR;
                rb.AddForce(transform.forward + Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
