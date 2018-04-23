using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class SignIn : MonoBehaviour {

	public InputField username;

	private string usernamestring;

    public GameObject signin;
    public GameObject secondmenu;

    public GameObject popup2;
    public GameObject popup3;

    public void SignInButton() {
        
		if (usernamestring != "") {
			if (!System.IO.Directory.Exists(@"C:/Dev/Cassiopee-project/Neurotest/Assets/Users/" + usernamestring + "/")) {
                System.IO.Directory.CreateDirectory(@"C:/Dev/Cassiopee-project/Neurotest/Assets/Users/" + usernamestring + "/");
                username.text = "";
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
