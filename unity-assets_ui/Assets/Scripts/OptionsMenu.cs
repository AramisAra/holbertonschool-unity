using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invertToggleY;
    void Start()
    {
        if (PlayerPrefs.HasKey("InvertY"))
        {
            invertToggleY.isOn = PlayerPrefs.GetInt("InvertY") == 1;
        }
        else
        {
            invertToggleY.isOn = false;
        }
    }
    public void Apply()
    {
        PlayerPrefs.SetInt("InvertY", invertToggleY.isOn ? 1 : 0);
        PlayerPrefs.Save();

        if (PlayerPrefs.HasKey("PreviousScene"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("PreviousScene"));
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    public void Back()
    {
        if (PlayerPrefs.HasKey("PreviousScene"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("PreviousScene"));
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
