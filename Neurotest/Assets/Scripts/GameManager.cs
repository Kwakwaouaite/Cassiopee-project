using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public LineManager lr;
    public GameObject prefabPoint;
    public Vector2[] pointPositions;

    private List<GameObject> listPoints;

    // Use this for initialization
    void Start () {
        listPoints = new List<GameObject>();
        for (int i =0; i < pointPositions.Length; i++)
        {
            GameObject newPoint = Instantiate(prefabPoint);
            newPoint.transform.SetPositionAndRotation(pointPositions[i], Quaternion.identity);
            listPoints.Add(newPoint);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
        {
            RaycastHit hit;
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position[0], Input.GetTouch(0).position[1], 10));
            
            if (Physics.Raycast(pos, Vector3.back, out hit, 10))
            {
                Debug.Log("hit !");
                
                Debug.Log(hit.transform.position);
                GameObject game = hit.collider.gameObject;
                game.GetComponent<Renderer>().material.color = Color.green;
                
            }

            Debug.Log(Input.GetTouch(0).position);

            //lr.AddPoint(1.0f, 2.0f);
            lr.AddPoint(pos.x, pos.y);
            /*
            Debug.Log("---------");
            Debug.Log(Input.GetTouch(0).position);
            Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position[0], Input.GetTouch(0).position[1], 10)));
            */

            Debug.DrawRay(pos, Vector3.back * 10);
        }
	}
}
