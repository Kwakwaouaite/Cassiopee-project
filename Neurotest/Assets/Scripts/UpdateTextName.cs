using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateTextName : MonoBehaviour {

    public GameObject parent;

	// Use this for initialization
	void Start () {
        Debug.Log(this.GetComponent<TextMeshProUGUI>());
        //Debug.Log(this.GetComponent<TextMeshPro>());
        Debug.Log(this.GetComponent<TextMeshProUGUI>().text);
        string a = "ABCDE";
        a.Substring(2);
        this.GetComponent<TextMeshProUGUI>().text = parent.name.Substring(6); // Le substring permet d'enlever le "Point_" du nom
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
