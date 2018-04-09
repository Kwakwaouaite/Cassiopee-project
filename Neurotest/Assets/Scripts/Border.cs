using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour {

    public GameObject center;

	// Use this for initialization
	void Start () {
        transform.SetPositionAndRotation(center.transform.position + Vector3.forward, center.transform.rotation);
        gameObject.GetComponent<Renderer>().material.color = Color.black;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
