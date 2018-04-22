using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class SignIn : MonoBehaviour {

	public InputField username;

	private string usernamestring;


	public void SignInButton() {
		bool UN = false;
        
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

	

		if (UN == true) {

            System.IO.Directory.CreateDirectory(@"C:/Dev/Cassiopee-project/Neurotest/Assets/Users/" + usernamestring + "/");

            print("Register Okay");
		}

	}

	// Update is called once per frame
	void Update () {
        usernamestring = username.text;
	}
}
