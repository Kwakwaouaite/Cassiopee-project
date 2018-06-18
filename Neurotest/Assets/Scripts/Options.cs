using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;


public class Options : MonoBehaviour {
       
    public GameObject point;
	public GameObject menuSecond;       
	public GameObject menuOption;

	public InputField nbPoints;
	private int nbPointsInt;

    public void OnEnable()
    {
		point.SetActive(true);
        PlayerPrefs.SetFloat(PlayerPrefs.GetString("current_player") + "_option_size", 1);
		nbPoints.text = (PlayerPrefs.GetInt(PlayerPrefs.GetString("current_player") + "_option_numberPoints")).ToString();
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
		point.SetActive(false);
		menuSecond.SetActive(true);
		menuOption.SetActive(false);
	}

	void Update () 
	{
		nbPointsInt = int.Parse(nbPoints.text);
		PlayerPrefs.SetInt(PlayerPrefs.GetString("current_player") + "_option_numberPoints", nbPointsInt);
	}
}