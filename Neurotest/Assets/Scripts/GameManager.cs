using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

    public LineManager lr;
    public GameObject prefabPoint;
    public GameObject prefabLineManager;
    public Vector2[] pointPositions;
    public GameObject endMenu;
    public bool isDetectHit;
    public TextMeshProUGUI title;

    private List<GameObject> listPoints;
    private List<GameObject> lineManagerList;
    private LineManager currentLineManager;
    private List<Vector2> dataPositionCollected;
    private long current;
    private string currentPlayer;
    private int currentLevel;
    string savePath;
    private bool isExerciseFinished;
    private string difficulty;
    string difficultyFR = "facile";
    private float size; // Taille des points
    private string choice;
    private bool isDrawingVisible;  // Est ce qu'on affice les traits
    private bool isDrawing = false; // Booleen = vrai, si le le bouton est appuye pour dessiner

    // Use this for initialization
    void Start () {


        lineManagerList = new List<GameObject>();
        dataPositionCollected = new List<Vector2>();

        currentPlayer = PlayerPrefs.GetString("current_player");
        if (currentPlayer == "")
        {   
            Debug.Log("<color=red>Error: </color><b>current_player </b>not found.");
        } else
        {
            Debug.Log("Current player: " + currentPlayer);
        }

        difficulty = PlayerPrefs.GetString("current_difficulty");
        if (!(difficulty == "easy" || difficulty == "medium" || difficulty == "hard"))
        {
            difficulty = "hard";
            Debug.Log("<color=yellow>Warning: </color>difficulty not found, set at default: " + difficulty);
        }
        
        switch (difficulty)
        {
            case "easy":
                difficultyFR = "facile";
                break;
            case "medium":
                difficultyFR = "moyen";
                break;
            case "hard":
                difficultyFR = "difficile";
                break;
        }

        currentLevel = PlayerPrefs.GetInt(currentPlayer + "_" + difficulty + "_level");
        Debug.Log("Current level: " + currentLevel);

        savePath = Application.persistentDataPath + "/Utilisateurs/" + currentPlayer
            + "/" + difficultyFR + "/" + currentLevel +".txt";
        title.text = currentPlayer + " - " + difficultyFR + " - " + currentLevel;

        savePath = System.IO.Path.GetFullPath(savePath);

        Debug.Log("Saving path: " + savePath);

        size = PlayerPrefs.GetFloat(currentPlayer + "_option_size");
        // TODO : Mettre le test d'existence 

        choice = PlayerPrefs.GetString(currentPlayer + "_option_visible");
        if (!(choice == "true" || choice == "false"))
        {
            choice = "true";
            Debug.Log("<color=yellow>Warning: </color>option_visible not found, set at default: " + choice);
        } else
        {
            Debug.Log("Draw is visible: " + choice);
        }

        isDrawingVisible = (choice == "true");

        choice = PlayerPrefs.GetString("current_choice");
        if (!(choice == "letters" || choice == "numbers"))
        {
            choice = "numbers";
            Debug.Log("<color=yellow>Warning: </color>choice not found, set at default: " + choice);
        }
        else
        {
            Debug.Log("Letters or numbers: " + choice);
        }

        isExerciseFinished = false;
        GenerateLevelData();
        GeneratePointList();
        current = 0;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
        }
        
        if (!isExerciseFinished && Input.GetMouseButton(0) && isDrawing)
        {
            
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (isDetectHit)
            {
                DetectHit(pos);               
            }
            if (isDrawingVisible)
            {
                currentLineManager.AddPoint(pos.x, pos.y);
            }
            if (pos.y < 285) // On ne veut pas enregistrer quand on appuie sur le boutton
            {
                dataPositionCollected.Add(new Vector2(pos.x, pos.y));
            }

        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Touch detected");
            if (isDrawingVisible)
            {
                GameObject newLineManager = Instantiate(prefabLineManager);
                lineManagerList.Add(newLineManager);
                currentLineManager = newLineManager.GetComponent<LineManager>();
            }
            isDrawing = true;
        }
    }


    private void GenerateLevelData()
    {
        Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelHeight, Camera.main.pixelWidth));
        //Debug.Log(screenSize[0]);
        //Debug.Log(screenSize[1]);

        int height = (int)screenSize[0];
        int width = (int)screenSize[1];
        

        for (int i=0; i < pointPositions.Length; i++)
        {
            pointPositions[i][0] = (int)Random.Range(10, width - 10);
            pointPositions[i][1] = (int)Random.Range(10, height - 30);
        }
    }

    private void GeneratePointList()
    {
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        listPoints = new List<GameObject>();
        for (int i = 0; i < pointPositions.Length; i++)
        {
            GameObject newPoint = Instantiate(prefabPoint);
            newPoint.transform.SetPositionAndRotation(new Vector3(pointPositions[i].x, pointPositions[i].y, -20), Quaternion.identity);
            newPoint.transform.localScale = new Vector3(size, size, size);
            if (difficulty == "easy")
            {
                if (choice == "letters")
                    newPoint.name = "Point " + alphabet[i];
                else
                    newPoint.name = "Point " + i.ToString();
            } else if (difficulty == "medium")
            {
                if (choice == "letters")
                    newPoint.name = "Point " + alphabet[i];
                else
                    newPoint.name = "Point " + i.ToString();
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

    public void OnPressValidateButton()
    {
        isExerciseFinished = true;
        SaveData();
        endMenu.SetActive(true);
    }

    public void GoToNextLevel()
    {
        Debug.Log("Niveau suivant");
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    private void SaveData()
    {   
        savePath = System.IO.Path.GetFullPath(savePath);

        Debug.Log("Sauvegarde des données : " + savePath);

        //title.text = System.IO.Path.GetFullPath(savePath);

        CSVScript.SaveDataToCSV(savePath, dataPositionCollected, header: "This is my header");

    }

    public void GoBackToMenu()
    {
        PlayerPrefs.SetString("menu_return", "true");
        PlayerPrefs.SetFloat(currentPlayer + "_option_size", 1);
        PlayerPrefs.SetString(currentPlayer + "_option_visible", "true");
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
