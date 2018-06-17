using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MainMenu : MonoBehaviour {

    public GameObject firstmenu;
    public GameObject secondmenu;
    public GameObject levelbis;
    public GameObject level;

    public TextMeshProUGUI title;

    // Use this for initialization
    void Start()
    {
        InitializeLevelDirectory();
        if (PlayerPrefs.GetString("menu_return") == "true" )
        {
            PlayerPrefs.SetString("menu_return", "false");
            secondmenu.SetActive(true);
            firstmenu.SetActive(false);
        }
    }

	public void StartBacASable()
	{
		PlayerPrefs.SetString("current_difficulty", "brouillon");
		SceneManager.LoadScene("Game");
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
    
    private void InitializeLevelDirectory ()
    {

        if (!System.IO.Directory.Exists(Application.persistentDataPath + "/niveaux"))
        {
            string pathToPersistent = System.IO.Path.GetFullPath(Application.persistentDataPath + "/niveaux");

            System.IO.Directory.CreateDirectory(pathToPersistent + "/facile/lettres");
            System.IO.Directory.CreateDirectory(pathToPersistent + "/facile/nombres");
            System.IO.Directory.CreateDirectory(pathToPersistent + "/moyen/lettres");
            System.IO.Directory.CreateDirectory(pathToPersistent + "/moyen/nombres");
            System.IO.Directory.CreateDirectory(pathToPersistent + "/difficile");

            BetterStreamingAssets.Initialize();


            string localPath;

            foreach (string difficulty in new string[3] { "facile", "moyen", "difficile" })
            {
                if (difficulty == "facile" || difficulty == "moyen")
                {
                    foreach (string type in new string[2] { "nombres", "lettres" })
                    {
                        localPath = "/" + difficulty + "/" + type + "/";
                        Debug.Log("Writing : " + localPath);
                        CopyLevelToPersistent(localPath);
                    }
                }
            }
        }
        
    }

    private void CopyLevelToPersistent(string localPath)
    {
        string pathToPersistent = System.IO.Path.GetFullPath(Application.persistentDataPath + "/niveaux");
        for (int i = 1; i<= 30; i++)
        {
            byte[] data = BetterStreamingAssets.ReadAllBytes("/niveaux/" + localPath + i + ".txt");
            System.IO.File.WriteAllBytes(pathToPersistent + localPath + i + ".txt", data);
        }
    }
}
