using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class GameManager : MonoBehaviour {

    public LineManager lr;
    public GameObject prefabPoint;
    public Vector2[] pointPositions;
    public float size;
    public bool isDetectHit;

    private List<GameObject> listPoints;
    private long current;
    private string currentPlayer;

    // Use this for initialization
    void Start () {

        currentPlayer = PlayerPrefs.GetString("current_player");
        if (currentPlayer == "")
        {
            Debug.Log("<color=red>Error: </color><b>current_player </b>not found.");
        }
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
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        listPoints = new List<GameObject>();
        string difficulty = PlayerPrefs.GetString("current_difficulty");
        if (!(difficulty == "easy" || difficulty == "medium" || difficulty == "hard"))
        {
            difficulty = "easy";
            Debug.Log("<color=yellow>Warning: </color>difficulty not found, set at default: " + difficulty);
        }
        for (int i = 0; i < pointPositions.Length; i++)
        {
            GameObject newPoint = Instantiate(prefabPoint);
            newPoint.transform.SetPositionAndRotation(new Vector3(pointPositions[i].x, pointPositions[i].y, -20), Quaternion.identity);
            if (difficulty == "easy")
            {
                newPoint.name = "Point " + i.ToString();
            } else if (difficulty == "medium")
            {
                newPoint.name = "Point " + alphabet[i];
            } else
            {
                if (i%2 == 0)
                {
                    newPoint.name = "Point " + ((i/2) + 1).ToString();
                } else
                {
                    newPoint.name = "Point " + alphabet[(i-1)/ 2];
                }
            }
            
            newPoint.SetActive(true);
            listPoints.Add(newPoint);
        }
    }
}
