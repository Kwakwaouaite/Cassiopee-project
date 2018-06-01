using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject firstmenu;
    public GameObject secondmenu;
    public GameObject levelbis;
    public GameObject level;



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
        PlayerPrefs.SetString("current_difficulty", "easy");
        level.SetActive(false);
        levelbis.SetActive(true);
    }

    public void StartGameMedium()
    {
        PlayerPrefs.SetString("current_difficulty", "medium");
        level.SetActive(false);
        levelbis.SetActive(true); ;
    }

    public void StartGameHard()
    {
        PlayerPrefs.SetString("current_difficulty", "hard");
        SceneManager.LoadScene("Game");
    }

    public void QuitGame() {
        PlayerPrefs.SetString("menu_return", "false");
        Application.Quit();
	}

    public void StartGameLetters()
    {
        PlayerPrefs.SetString("current_choice", "lettres");
        SceneManager.LoadScene("Game");
    }

    public void StartGameNumbers()
    {
        PlayerPrefs.SetString("current_choice", "nombres");
        SceneManager.LoadScene("Game");
    }
}
