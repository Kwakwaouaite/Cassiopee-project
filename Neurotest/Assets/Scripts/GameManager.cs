using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public LineManager lr;
    public GameObject prefabPoint;
    public Vector2[] pointPositions;
    public float size;
    public bool isDetectHit;

    private List<GameObject> listPoints;
    private long current;

    // Use this for initialization
    void Start () {

        generatePointList();
        current = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
        {
            
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position[0], Input.GetTouch(0).position[1], 10));
            if (isDetectHit)
            { 
                RaycastHit hit;
                if (Physics.Raycast(pos, Vector3.back, out hit, 10))
                {
                    Debug.Log("hit !");

                    Transform game = hit.collider.transform;

                    Debug.Log(game.parent.name);

                    Debug.Log(hit.transform.position);
                    if (game.parent.name == "Point " + current.ToString())
                    {
                        game.GetComponent<Renderer>().material.color = Color.green;
                        current++;
                    }
                    Debug.DrawRay(pos, Vector3.back * 10);
                }
                
                
                
            }
            lr.AddPoint(pos.x, pos.y);
            
        }
	}

    private void generatePointList()
    {
        listPoints = new List<GameObject>();
        for (int i = 0; i < pointPositions.Length; i++)
        {
            GameObject newPoint = Instantiate(prefabPoint);
            newPoint.transform.SetPositionAndRotation(new Vector3(pointPositions[i].x, pointPositions[i].y, -20), Quaternion.identity);
            newPoint.name = "Point " + i.ToString();
            newPoint.SetActive(true);
            listPoints.Add(newPoint);
        }
    }
}
