using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public class LogIn : MonoBehaviour {

    public InputField username;

    public GameObject login;
    public GameObject secondmenu;

    public GameObject popup;
    public GameObject popup2;

    private string usernamestring;

    public void LogInButton()
    {
        if (usernamestring != "")
        {
            if (System.IO.Directory.Exists(@"C:/Dev/Cassiopee-project/Neurotest/Assets/Users/" + usernamestring + "/")) {
                username.text = "";
                print("LogIn Okay");
                Launch(usernamestring);
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

    public void Launch(String usernamestring)
    {
        login.SetActive(false);
        secondmenu.SetActive(true);


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
