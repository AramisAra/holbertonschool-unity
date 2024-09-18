using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerPosition, Diference;

    // Start is called before the first frame update
    void Start()
    {
        if (!player)
        {
            Debug.Log("No player selected");
        }
        playerPosition = player.transform.position;
        Diference = playerPosition - transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        playerPosition = player.transform.position;
        transform.position = playerPosition - Diference;
    }
}
