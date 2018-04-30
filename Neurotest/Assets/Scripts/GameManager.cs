using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public LineManager lr;
    public GameObject prefabPoint;
    public Vector2[] pointPositions;
    public float size; // Taille des points
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
        generateLevelData();
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
                DetectHit(pos);               
            }
            lr.AddPoint(pos.x, pos.y);
            
        }
	}

    private void generateLevelData()
    {
        Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelHeight, Camera.main.pixelWidth));
        Debug.Log(screenSize[0]);
        Debug.Log(screenSize[1]);

        int height = (int)screenSize[0];
        int width = (int)screenSize[1];

        //pointNumber = (int)Random.Range(5, 15);
        Debug.Log(pointNumber);
        for (int i=0; i < pointPositions.Length; i++)
        {
            pointPositions[i][0] = (int)Random.Range(10, width - 10);
            pointPositions[i][1] = (int)Random.Range(10, height - 30);
        }
    }

    private void generatePointList()
    {
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        listPoints = new List<GameObject>();
        string difficulty = PlayerPrefs.GetString("current_difficulty");
        if (!(difficulty == "easy" || difficulty == "medium" || difficulty == "hard"))
        {
            difficulty = "hard";
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

    private void DetectHit(Vector3 pos)
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

    public void GoTonextLevel()
    {
        Debug.Log("Niveau suivant");
        SaveData();
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    private void SaveData()
    {
        //TODO
        Debug.Log("Faire la sauvegarde de donnees");
    }
}
