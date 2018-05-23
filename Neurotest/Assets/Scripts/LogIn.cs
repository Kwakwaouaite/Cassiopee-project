using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
//using System.Windows.Forms;

public class LogIn : MonoBehaviour {

    public InputField username;

    public GameObject login;
    public GameObject secondmenu;

    public GameObject popup;
    public GameObject popup2;

    private string usernamestring;

    private string path;


    public void LogInButton()
    {
        path = Application.persistentDataPath + "/Users/";

        if (usernamestring != "")
        {
            if (System.IO.Directory.Exists(path + usernamestring + "/")) {
                PlayerPrefs.SetString("current_player", usernamestring);

                username.text = "";

                login.SetActive(false);
                secondmenu.SetActive(true);

            }
            else {
                popup.SetActive(true);
                username.text = "";
            }
        }
        else {
            popup2.SetActive(true);
        }
    }



    public void Keyboard()
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

   // Update is called once per frame
    void Update () {
        usernamestring = username.text;

    }
}
