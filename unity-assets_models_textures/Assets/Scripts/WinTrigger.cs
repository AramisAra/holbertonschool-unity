using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinTrigger : MonoBehaviour
{
    public TextMeshProUGUI winText;
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
        other.GetComponent<Timer>().enabled = false;
        winText.fontSize = 60;
        winText.color = Color.green;
    }
}
