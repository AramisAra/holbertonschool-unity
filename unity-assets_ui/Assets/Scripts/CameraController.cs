using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 100;
    Vector3 offset;
    public bool isInverted;


    void Awake()
    {
        offset = player.position - transform.position;
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("InvertY"))
        {
            isInverted = PlayerPrefs.GetInt("InvertY") == 1;
        }
        else
        {
            isInverted = false;
        }
    }

    void Update()
    {
        Quaternion rotation = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
        rotation *= ChangeInvertY();
        offset = rotation * offset;
        transform.position = player.position - offset;
        transform.LookAt(player);
    }
    Quaternion ChangeInvertY()
    {
        if (isInverted)
        {
            return Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * rotationSpeed, Vector3.left);
        }
        else
        {
            return Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotationSpeed, Vector3.left);
        }
    }
}
