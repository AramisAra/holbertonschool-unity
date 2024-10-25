using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    PlayerController playerController;
    Timer timer;
    CameraController cameraController;
    bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        timer = FindObjectOfType<Timer>();
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();

        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
                isPaused = false;
            }
            else
            {
                Pause();
                isPaused = true;
            }
        }
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        playerController.enabled = false;
        timer.enabled = false;
        cameraController.enabled = false;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        playerController.enabled = true;
        timer.enabled = true;
        cameraController.enabled = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Options()
    {
        SceneManager.LoadScene(1);
    }
}
