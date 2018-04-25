using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour {

    public void AdjustSize(float newSize)
    {
        PlayerPrefs.SetFloat(PlayerPrefs.GetString("current_player")+"_option_size", newSize);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
