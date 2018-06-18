using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using System;


public class Options : MonoBehaviour {
       
    public GameObject point;
	public GameObject menuSecond;       
	public GameObject menuOption;
	public GameObject popUp4;

	public InputField nbPoints;
	private int nbPointsInt;

    public void OnEnable()
    {
		point.SetActive(true);
        PlayerPrefs.SetFloat(PlayerPrefs.GetString("current_player") + "_option_size", 1);
		PlayerPrefs.SetInt(PlayerPrefs.GetString("current_player") + "_option_numberPoints", 25);
    }

    public void AdjustSize(float newSize)
    {
        float adjust = 4.2666f;
        point.transform.localScale = new Vector3(adjust*newSize, adjust * newSize, adjust * newSize);
        PlayerPrefs.SetFloat(PlayerPrefs.GetString("current_player")+"_option_size", newSize);
    }

    public void TraitVisible(bool visible)
    {
        if (visible == true)
        {
            PlayerPrefs.SetString(PlayerPrefs.GetString("current_player") + "_option_visible", "true");
        }
        else
        {
            PlayerPrefs.SetString(PlayerPrefs.GetString("current_player") + "_option_visible", "false");
        }
    }

	public void Retour()
	{
		try {
			nbPointsInt = int.Parse(nbPoints.text);
		}
		catch (FormatException) {
			popUp4.SetActive (true);
			nbPoints.text = "";
		}   
		catch (OverflowException) {
			popUp4.SetActive (true);
			nbPoints.text = "";
		} 

		PlayerPrefs.SetInt(PlayerPrefs.GetString("current_player") + "_option_numberPoints", nbPointsInt);
		if (nbPointsInt < 10 || nbPointsInt > 25) {
			popUp4.SetActive (true);
		} 
		else {
			point.SetActive (false);
			menuSecond.SetActive (true);
			menuOption.SetActive (false);
		}
	
	}

	void Update () 
	{
		
	}
}