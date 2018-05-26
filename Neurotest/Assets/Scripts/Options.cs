using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Options : MonoBehaviour {
       
    public GameObject point;

    // Use this for initialization
    void Start()
    {

    }

    public void AdjustSize(float newSize)
    {
        int adjust = 6;
        point.transform.localScale = new Vector3(adjust*newSize, adjust * newSize, adjust * newSize);
        PlayerPrefs.SetFloat(PlayerPrefs.GetString("current_player")+"_option_size", newSize);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}


