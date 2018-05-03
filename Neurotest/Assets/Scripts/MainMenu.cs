using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void StartGameEasy()
    {
        PlayerPrefs.SetString("current_difficulty", "easy");
        SceneManager.LoadScene("Game");
    }

    public void StartGameMedium()
    {
        PlayerPrefs.SetString("current_difficulty", "medium");
        SceneManager.LoadScene("Game");
    }

    public void StartGameHard()
    {
        PlayerPrefs.SetString("current_difficulty", "hard");
        SceneManager.LoadScene("Game");
    }

    public void QuitGame() {
		Application.Quit();
	}
}
