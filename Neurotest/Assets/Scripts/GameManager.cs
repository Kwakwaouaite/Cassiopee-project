using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Better.StreamingAssets;

/*
 * Cette classe sert de superviseur pour chaque session de jeu, elle contient 
 * beaucoup (trop) de fonctionnalitées tel que la génération des données au 
 * début du niveau, l'enregistrement et le traçage du tracé de l'utilisateur.
 * 
 */
public class GameManager : MonoBehaviour {

    public LineManager lr;    // Le line manager de base pour le prefab.
    public GameObject prefabPoint;  // Le prefab du point.
    public GameObject prefabLineManager;    // Le prefab du line manager.
    public Vector2[] pointPositions;   // Vecteur contenant toute les posistions des points.
    public GameObject endMenu;  // Le GameObject correspondant au menu.
    public bool isDetectHit;    // Boolean pour savoir si on doit detecter le passage sur les points.
    public TextMeshProUGUI title;   // Le titre afficher en haut du niveau
    public bool isRandomPosition;   // A mettre a true pour generer la position des points.
    public int maxLevelInASerie = 30;   // Nombre max de niveau dans une serie.
    public GameObject nextLevelButton;  // Le bouton pour passer au prochain niveau.

    private List<GameObject> listPoints;    // La liste de tout les points.
    private List<GameObject> lineManagerList;   // La liste de tout les line managers.
    private LineManager currentLineManager;
    private List<Vector3> dataPositionCollected; // { x, y, time}
    private List<Vector2> pointPositionCollected; // { x, y}
    private string currentPlayer;
    private int currentLevel;
    private string playerPrefLevelPath;
    private string savePath;
    private bool isExerciseFinished;
    private string difficulty;
    private string difficultyFR = "facile";
    private string chiffreOuLettre;
    private float size; // Taille des points
    private string isDrawingVisible;
    private bool isDrawingVisibleBool;  // Est ce qu'on affice les traits
    private bool isDrawing = false; // Booleen = vrai, si le le bouton est appuye pour dessiner
    private int current;

    // Use this for initialization
    void Start () {

        lineManagerList = new List<GameObject>();
        dataPositionCollected = new List<Vector3>();

        isExerciseFinished = false;

        InitializeFromPlayerPref();

        if (!isExerciseFinished && difficulty != "brouillon") // Si on n'a pas finis la série, on charge les données
        {
            GenerateLevelData();
            GeneratePointList();
        }
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
            if (isDrawingVisibleBool)
            {
                currentLineManager.AddPoint(pos.x, pos.y);
            }
            if (pos.y < 285) // On ne veut pas enregistrer quand on appuie sur le boutton
            {
                dataPositionCollected.Add(new Vector3(pos.x, pos.y, Time.timeSinceLevelLoad));
            }

        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Touch detected");
            if (isDrawingVisibleBool)
            {
                GameObject newLineManager = Instantiate(prefabLineManager);
                lineManagerList.Add(newLineManager);
                currentLineManager = newLineManager.GetComponent<LineManager>();
            }
            isDrawing = true;
        }
    }

    private void InitializeFromPlayerPref()
    {

        currentPlayer = PlayerPrefs.GetString("current_player");
        if (currentPlayer == "")
        {
            Debug.Log("<color=red>Error: </color><b>current_player </b>not found.");
        }
        else
        {
            Debug.Log("Current player: " + currentPlayer);
        }

        difficulty = PlayerPrefs.GetString("current_difficulty");
        if (!(difficulty == "easy" || difficulty == "medium" || difficulty == "hard" || difficulty == "brouillon"))
        {
            difficulty = "hard";
            Debug.Log("<color=yellow>Warning: </color>difficulty not found, set at default: " + difficulty);
        }

        isDrawingVisible = PlayerPrefs.GetString(currentPlayer + "_option_visible");
        if (!(isDrawingVisible == "true" || isDrawingVisible == "false"))
        {
            isDrawingVisible = "true";
            Debug.Log("<color=yellow>Warning: </color>option_visible not found, set at default: " + isDrawingVisible);
        }
        else
        {
            Debug.Log("Draw is visible: " + isDrawingVisible);
        }

        isDrawingVisibleBool = (isDrawingVisible == "true");

        chiffreOuLettre = PlayerPrefs.GetString("current_choice");
        if (!(chiffreOuLettre == "lettres" || chiffreOuLettre == "nombres"))
        {
            chiffreOuLettre = "nombres";
            Debug.Log("<color=yellow>Warning: </color>choice not found, set at default: " + chiffreOuLettre);
        }
        else
        {
            Debug.Log("Letters or numbers: " + chiffreOuLettre);
        }

        switch (difficulty)
        {
            case "easy":
                difficultyFR = "facile";
                playerPrefLevelPath = currentPlayer + "_" + difficulty + "_" + chiffreOuLettre + "_level";
                break;
            case "medium":
                difficultyFR = "moyen";
                playerPrefLevelPath = currentPlayer + "_" + difficulty + "_" + chiffreOuLettre + "_level";
                break;
            case "hard":
                difficultyFR = "difficile";
                chiffreOuLettre = "";
                playerPrefLevelPath = currentPlayer + "_" + difficulty + "_level";
                break;
            case "brouillon":
                difficultyFR = "brouillon";
                chiffreOuLettre = "";
                playerPrefLevelPath = currentPlayer + "_" + difficulty + "_level";
                break;
        }

        if (PlayerPrefs.GetInt(playerPrefLevelPath) == 0)
        {
            currentLevel = 1;
        }
        else
        {
            currentLevel = PlayerPrefs.GetInt(playerPrefLevelPath);
        }

        if (currentLevel > maxLevelInASerie && difficulty != "brouillon")
        {
            title.text = "La série est finie";
            isExerciseFinished = true;
            nextLevelButton.SetActive(false);
            endMenu.SetActive(true);
            return;
        }

        Debug.Log("Current level: " + currentLevel);

        savePath = Application.persistentDataPath + "/Utilisateurs/" + currentPlayer
            + "/" + difficultyFR + "/" + chiffreOuLettre + "/" + currentLevel + ".txt";
        if (chiffreOuLettre == "")
        {
            title.text = currentPlayer + " - " + difficultyFR + " - " + currentLevel;
        } else
        {
            title.text = currentPlayer + " - " + difficultyFR + " - " + chiffreOuLettre + " - " + currentLevel;
        }
        

        savePath = System.IO.Path.GetFullPath(savePath);

        Debug.Log("Saving path: " + savePath);

        size = PlayerPrefs.GetFloat(currentPlayer + "_option_size");
        // TODO : Mettre le test d'existence 

        
    }

    private void GenerateLevelData()
    {
        Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelHeight, Camera.main.pixelWidth));

        int height = (int)screenSize[0];
        int width = (int)screenSize[1];
        
        if (!isRandomPosition)
        {
            string pointPath = Application.persistentDataPath + "/niveaux/" 
                + difficultyFR + "/" + chiffreOuLettre + "/" + currentLevel + ".txt";

            pointPath = System.IO.Path.GetFullPath(pointPath);
            Debug.Log("Looking for data at: " + pointPath);
            
            string[,] read = CSVScript.ReadCSV(pointPath);
            pointPositions = new Vector2[read.Length / 2];
            for (int i = 0; i < read.Length/2; i++)
            {
                if (read[i,0] == null)
                    break;
                pointPositions[i][0] = float.Parse(read[i, 0]);
                pointPositions[i][1] = float.Parse(read[i, 1]);
            }
        } else
        {
            int rand1 = 0;
            int rand2 = 0;

            pointPositions[0][0] = (int)UnityEngine.Random.Range(18, width - 18);
            pointPositions[0][1] = (int)UnityEngine.Random.Range(18, height - 40);

            for (int i = 1; i < pointPositions.Length; i++)
            {
                bool test = true;
                bool val = true;

                int noFreeze = 0;

                while (val)
                {
                    noFreeze++;
                    rand1 = (int)UnityEngine.Random.Range(18, width - 18);
                    rand2 = (int)UnityEngine.Random.Range(18, height - 40);
                    
                    for (int j = 0; j < i; j++)
                    {
                        if (Math.Sqrt(Math.Pow(rand1 - pointPositions[j][0], 2) + Math.Pow(rand2 - pointPositions[j][1], 2)) < 22)
                        {
                            test = false;
                        }
                       
                    }
                    if (test)
                    {
                        pointPositions[i][0] = rand1;
                        pointPositions[i][1] = rand2;
                        val = false;
                    }

                    if (noFreeze > 10000)
                    {
                        pointPositions[i][0] = rand1;
                        pointPositions[i][1] = rand2;
                        val = false;
                    }

                    PlayerPrefs.SetInt(playerPrefLevelPath, currentLevel - 1);

                }
            }
        }        
    }


    private void GeneratePointList()
    {
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        listPoints = new List<GameObject>();
        for (int i = 0; i < pointPositions.Length; i++)
        {
            if (pointPositions[i].x == 0 && pointPositions[i].y == 0)
            {
                break;
            }
            GameObject newPoint = Instantiate(prefabPoint);
            newPoint.transform.SetPositionAndRotation(new Vector3(pointPositions[i].x, pointPositions[i].y, -20), Quaternion.identity);
            newPoint.transform.localScale = new Vector3(size, size, size);
            if (difficulty == "easy")
            {
                if (chiffreOuLettre == "lettres")
                    newPoint.name = "Point " + alphabet[i];
                else
                    newPoint.name = "Point " + i.ToString();
            } else if (difficulty == "medium")
            {
                if (chiffreOuLettre == "lettres")
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
        PlayerPrefs.SetInt(playerPrefLevelPath, currentLevel + 1);
        endMenu.SetActive(true);
    }

    public void GoToNextLevel()
    {
        Debug.Log("Niveau suivant");
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void RedoLevel()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    private void SaveData()
    {
        savePath = System.IO.Path.GetFullPath(savePath);

        Debug.Log("Sauvegarde des données : " + savePath);

        //title.text = System.IO.Path.GetFullPath(savePath);



        string header = System.DateTime.Now.ToString() + "- Trait visible: " + isDrawingVisible + "- Taille: " + size*20 ;

        CSVScript.SaveDataToCSV(savePath, dataPositionCollected, header: header);

    }

    public void SaveDataBis()
    {
        int variableAure = PlayerPrefs.GetInt("variableAure");
        savePath = System.IO.Path.GetFullPath("C:/Dev/Cassiopee-project/Neurotest/Assets/Users/"+variableAure+".csv");
        PlayerPrefs.SetInt("variableAure",variableAure + 1);
        List<Vector2> pointPositionCollected = new List<Vector2>();

        for (int i = 0; i < pointPositions.Length; i++)
        {
            pointPositionCollected.Add(new Vector2(pointPositions[i].x, pointPositions[i].y));
        }

        Debug.Log("Sauvegarde des données : " + savePath);

        //title.text = System.IO.Path.GetFullPath(savePath);

        CSVScript.SaveDataToCSV(savePath, pointPositionCollected);

        GoToNextLevel();

    }

    public void GoBackToMenu()
    {
        PlayerPrefs.SetString("menu_return", "true");
        PlayerPrefs.SetString(currentPlayer + "_option_visible", "true");
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
