using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class SignIn : MonoBehaviour {

	public InputField username;
	public InputField password;

	private string usernamestring;
	private string passwordstring;

	private string form;


	public void SignInButton() {
		bool UN = false;
		bool PW = false;
        
		if (usernamestring != "") {
			if (!System.IO.File.Exists(@"C:/Dev/Cassiopee-project/Neurotest/Assets/Users/" + usernamestring + "/")) {
				UN = true;
                print("Le nom est "+usernamestring);
			} else {
				Debug.LogWarning("Username Taken");
			}
		} else {
				Debug.LogWarning("Username field empty");
		}

		if (passwordstring != "") {
			if (passwordstring.Length > 5) {
					PW = true;
			} else {
					Debug.LogWarning("Password must be at least a 6 characters long");
			}
		} else {
				Debug.LogWarning("Password field empty");
		}

		if (UN == true && PW == true) {
			bool Clear = true;
			int i = 1;
			foreach(char c in passwordstring) {
				if (Clear) {
                    passwordstring = "";
					Clear = false;
				}
				i++;
				char Encrypted = (char)(c * i);
                passwordstring += Encrypted.ToString();
			}
			form = (usernamestring + Environment.NewLine+ passwordstring);
            System.IO.Directory.CreateDirectory(@"C:/Dev/Cassiopee-project/Neurotest/Assets/Users/" + usernamestring + "/");
            System.IO.File.WriteAllText(@"C:/Dev/Cassiopee-project/Neurotest/Assets/Users/"+ usernamestring + "/" + usernamestring +".txt", form);
            //username.GetComponentsInChildren<Text>().Set("");
            //password.GetComponentsInChildren<Text>().Set("");
            print(usernamestring);

            print("Register Okay");
		}

	}

	// Update is called once per frame
	void Update () {
        usernamestring = username.text;
        passwordstring = password.text;
	}
}
