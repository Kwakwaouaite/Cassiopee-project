using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;


public class SignIn : MonoBehaviour {

	public InputField username;

    private string path;

    private string usernamestring;

    public GameObject signin;
    public GameObject secondmenu;

    public GameObject popup2;
    public GameObject popup3;

    public void SignInButton() {

        path = Application.persistentDataPath + "/Utilisateurs/";

        if (usernamestring != "") {
			if (!System.IO.Directory.Exists(path + usernamestring + "/")) {
                // création des dossiers
                System.IO.Directory.CreateDirectory(path + usernamestring + "/");
                System.IO.Directory.CreateDirectory(path + usernamestring + "/facile/");
                System.IO.Directory.CreateDirectory(path + usernamestring + "/facile/lettres");
                System.IO.Directory.CreateDirectory(path + usernamestring + "/facile/nombres");
                System.IO.Directory.CreateDirectory(path + usernamestring + "/moyen/");
                System.IO.Directory.CreateDirectory(path + usernamestring + "/moyen/lettres");
                System.IO.Directory.CreateDirectory(path + usernamestring + "/moyen/nombres");
                System.IO.Directory.CreateDirectory(path + usernamestring + "/difficile");
                System.IO.Directory.CreateDirectory(path + usernamestring + "/brouillon");

                // creation et mise à jour des sharedpref
                PlayerPrefs.SetInt(usernamestring+"_easy_level", 0);
                PlayerPrefs.SetInt(usernamestring + "_medium_level", 0);
                PlayerPrefs.SetInt(usernamestring + "_hard_level", 0);
                PlayerPrefs.SetFloat(usernamestring + "_option_size", 1);
                PlayerPrefs.SetString(usernamestring + "_option_visible", "true");
				PlayerPrefs.SetInt (usernamestring + "_option_numberPoints", 25);
                

                PlayerPrefs.SetString("current_player", usernamestring);
                
                // clean inputfield
                username.text = "";

                // changement de scene
                signin.SetActive(false);
                secondmenu.SetActive(true);
            }
            else {
                popup3.SetActive(true);
                username.text = "";
            }
        } else {
            popup2.SetActive(true);
        }

    }

	// Update is called once per frame
	void Update () {
        usernamestring = username.text;
	}
}
