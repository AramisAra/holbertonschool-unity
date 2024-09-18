using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private bool isTeleport = false;
    private string TeleporterName;
    public float moveSpeed, floatpoint;
    private float speedY, speedX;
    public int health = 5;
    private float MovementX, MovementY;
    private int score = 0;
    private Vector3 otherTeleporter;
    public TMP_Text scoreText;
    public TMP_Text healthText;
    public TMP_Text WinLoseText;
    public GameObject WinLoseBG;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
        if (health == 0)
        {
            WinLoseText.text = "Game Over!";
            WinLoseText.color = Color.white;
            WinLoseBG.GetComponent<Image>().color = Color.red;
            WinLoseBG.SetActive(true);
            StartCoroutine(LoadScene(3));
        }
    }


    
    // Update is called once per frame
    void FixedUpdate()
    {
        speedX = Input.GetAxis("Horizontal") * moveSpeed;
        speedY = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 playerVelocity = new Vector3(speedX, 0, speedY);
        rb.velocity = Vector3.Lerp(rb.velocity, playerVelocity, floatpoint);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score++;
            SetScoreText();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
        }

        if (other.CompareTag("Goal"))
        {
            WinLoseBG.GetComponent<Image>().color = Color.green;
            WinLoseText.color = Color.black;
            WinLoseText.text = "You Win!";
            WinLoseBG.SetActive(true);
            StartCoroutine(LoadScene(3));
        }

        if (other.CompareTag("Teleporter") && isTeleport == false)
        {
            if (other.name == "Teleporter")
            {
                transform.position = GameObject.Find("Teleporter (1)").transform.position;
                TeleporterName = "Teleporter (1)";
            }
            else
            {
                transform.position = GameObject.Find("Teleporter").transform.position;
                TeleporterName = "Teleporter";
            }
            isTeleport = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (TeleporterName == other.name)
            isTeleport = false;
    }

    void SetScoreText()
    {
        scoreText.text = $"Score: {score.ToString()}";
    }
    void SetHealthText()
    {
        healthText.text = $"Health: {health.ToString()}";
    }
    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("maze");
    }
}