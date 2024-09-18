using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Material trapMat;
    public Material goalMat;
    public Toggle colorblindMode;

    void Start()
    {
        colorblindMode.isOn = PlayerPrefs.GetInt("colorblindMode", 0) == 1;
        colorblindMode.onValueChanged.AddListener(SaveToggleState);
    }
    public void PlayMaze()
    {
        if (colorblindMode.isOn)
        {
            trapMat.color = new Color32(255, 112, 0, 1);
            goalMat.color = Color.blue;
        }else
        {
            trapMat.color = Color.red;
            goalMat.color = Color.green;
        }
        SceneManager.LoadScene("maze");
    }
    public void QuitMaze()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    private void SaveToggleState(bool isOn)
    {
        PlayerPrefs.SetInt("ColorblindMode", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}
