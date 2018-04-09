using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class SignIn : MonoBehaviour {

	public GameObject username;
	public GameObject password;

	private string Username;
	private string Password;

	private string form;


	public void SignInButton() {
		bool UN = false;
		bool PW = false;
        
		if (Username != "") {
			if (!System.IO.File.Exists(@"C:/Dev/Cassiopee-project/Neurotest/Assets"+Username+".txt")) {
					UN = true;
				} else {
					Debug.LogWarning("Username Taken");
				}
		} else {
				Debug.LogWarning("Username field empty");
		}

		if (Password != "") {
			if (Password.Length > 5) {
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
			foreach(char c in Password) {
				if (Clear) {
					Password = "";
					Clear = false;
				}
				i++;
				char Encrypted = (char)(c * i);
				Password += Encrypted.ToString();
			}
			form = (Username+Environment.NewLine+Password);
			System.IO.File.WriteAllText(@"C:/Dev/Cassiopee-project/Neurotest/Assets"+Username+".txt", form);
			username.GetComponent<InputField>().text = "";
			password.GetComponent<InputField>().text = "";
			print("Register Okay");
		}

	}

	// Update is called once per frame
	void Update () {
		Username = username.GetComponent<InputField>().text;
		Password = password.GetComponent<InputField>().text;
	}
}
