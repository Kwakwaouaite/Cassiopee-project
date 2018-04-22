using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class LogIn : MonoBehaviour {

    public InputField username;

    private string usernamestring;

    public void LogInButton()
    {
        bool UN = false;

        if (usernamestring != "")
        {
            if (!System.IO.File.Exists(@"C:/Dev/Cassiopee-project/Neurotest/Assets/Users/" + usernamestring + "/")) {
                UN = true;
                print("Le nom est " + usernamestring);
            }
            else {
                Debug.LogWarning("Username incorrect");
            }
        }
        else {
            Debug.LogWarning("Username field empty");
        }


        if (UN == true) {
            print("LogIn Okay");
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
