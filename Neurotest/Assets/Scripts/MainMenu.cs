using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject firstmenu;
    public GameObject secondmenu;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetString("menu_return") == "true" )
        {
            PlayerPrefs.SetString("menu_return", "false");
            secondmenu.SetActive(true);
            firstmenu.SetActive(false);
        }
    }

    public void StartGameEasy()
    {
        PlayerPrefs.SetString("menu_return", "true");
        PlayerPrefs.SetString("current_difficulty", "easy");
        SceneManager.LoadScene("Game");
    }

    public void StartGameMedium()
    {
        PlayerPrefs.SetString("menu_return", "true");
        PlayerPrefs.SetString("current_difficulty", "medium");
        SceneManager.LoadScene("Game");
    }

    public void StartGameHard()
    {
        PlayerPrefs.SetString("menu_return", "true");
        PlayerPrefs.SetString("current_difficulty", "hard");
        SceneManager.LoadScene("Game");
    }

    public void QuitGame() {
        PlayerPrefs.SetString("menu_return", "false");
        Application.Quit();
	}
}
