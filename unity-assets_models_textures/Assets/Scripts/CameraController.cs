using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 100;
    Vector3 offset;


    void Awake()
    {
        offset = player.position - transform.position;
    }

    void Update()
    {
        Quaternion rotation = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
        rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotationSpeed, Vector3.left);
        offset = rotation * offset;
        transform.position = player.position - offset;
        transform.LookAt(player);
    }
}
