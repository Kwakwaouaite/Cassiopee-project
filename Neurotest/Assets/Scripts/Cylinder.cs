using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void ChangeColor()
    {
        var couleur = gameObject.GetComponent<Renderer>();
        Debug.Log("test");
        if (couleur.material.color == Color.green)
        {
            couleur.material.color = Color.red;
        }
        else
        {
            couleur.material.color = Color.green;
        }
    }

    // Update is called once per frame
    void Update () {
    }
}
