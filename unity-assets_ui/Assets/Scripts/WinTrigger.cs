using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinTrigger : MonoBehaviour
{
    public GameObject player;
    // public Text timerText;
    public GameObject winCanvas;
    public GameObject MainCamara;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        winCanvas.SetActive(true);
        player.GetComponent<Timer>().enabled = false;
        player.GetComponent<PauseMenu>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;
        MainCamara.GetComponent<CameraController>().enabled = false;
        player.GetComponent<Timer>().Win();
    }
}
