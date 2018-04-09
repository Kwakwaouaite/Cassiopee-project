using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour {

    public GameObject parent;

	// Use this for initialization
	void Start () {

    }

    public void ChangeColor()
    {
        var couleur = gameObject.GetComponent<Renderer>();
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
