using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Options : MonoBehaviour {
       
    public GameObject point;


    public void AdjustSize(float newSize)
    {
        int adjust = 6;
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

}


