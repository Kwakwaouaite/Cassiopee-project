using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class LogIn : MonoBehaviour {

    public InputField username;
    public InputField password;

    private String[] lines;
    private string decryptedPass;

    private string usernamestring;
    private string passwordstring;

    public void LogInButton()
    {
        bool UN = false;
        bool PW = false;

        if (usernamestring != "")
        {
            if (!System.IO.File.Exists(@"C:/Dev/Cassiopee-project/Neurotest/Assets/Users/" + usernamestring + "/")) {
                UN = true;
                print("Le nom est " + usernamestring);
                lines = System.IO.File.ReadAllLines(@"C:/Dev/Cassiopee-project/Neurotest/Assets/Users/" + usernamestring + "/" + usernamestring + ".txt");
            }
            else {
                Debug.LogWarning("Username incorrect");
            }
        }
        else {
            Debug.LogWarning("Username field empty");
        }

        if (passwordstring != "") {
            if (System.IO.File.Exists(@"C:/Dev/Cassiopee-project/Neurotest/Assets/Users/" + usernamestring + "/" + usernamestring + ".txt")) {

                int i = 1;
                foreach (char c in lines[1]) {
                    i++;
                    char Decrypted = (char)(c / i);
                    decryptedPass += Decrypted.ToString();
                }

                if (passwordstring == decryptedPass) {
                    PW = true;
                } else {
                    Debug.LogWarning("Password Is invalid");
                }
            } else {
                Debug.LogWarning("Password Is invalid");
            }

        } else {
            Debug.LogWarning("Password field empty");
        }

        if (UN == true && PW == true) {
            print("LogIn Okay");
        }
    }


        // Update is called once per frame
        void Update () {
        usernamestring = username.text;
        passwordstring = password.text;
    }
}
