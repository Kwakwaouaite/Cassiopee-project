using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVScript : MonoBehaviour {

	public static string[,] ReadCSV(string path, int skip=0, char separator = '\t')
    {
        
        Debug.Log(path);
        string fileData = System.IO.File.ReadAllText(path);
        string[] lines = fileData.Split('\n');

        if (skip >= lines.Length)
        {
            Debug.Log("Too many data skiped");
            return new string[0,0];
        }

        int nbrColumn = lines[0 + skip].Split(separator).Length;
        string[,] data = new string[lines.Length, nbrColumn];

        for (int i = 0 + skip; i < lines.Length; i++)
        {
            string[] row = lines[i].Split(separator);
            for (int j = 0; j < row.Length; j++)
            {
                Debug.Log(i  + " " + j);
                Debug.Log(row[j]);
                data[i, j] = row[j];
                Debug.Log(data[i, j]);
            }
        }


        return data;
    }

    public static void SaveDataToCSV(string path, List<Vector2> list, string header = null, char separator = '\t')
    {
        /* public static void AppendAllLines(
            string path,
            IEnumerable<string> contents
        )*/
        //Debug.Log("Wrintpath: " + path);

        using (StreamWriter sw = new StreamWriter(path))
        {
            

            if (header != null)
            {
                sw.WriteLine(header);
                Debug.Log("Writing: " + header);
            }

            
            foreach(Vector2 vector in list)
            {
                string line = vector.x + separator.ToString() + vector.y;
                sw.WriteLine(line);
                Debug.Log("Writing: " + line);
            } 
            
            /*
            // Add some text to the file.
            sw.Write("This is the ");
            sw.WriteLine("header for the file.");
            sw.WriteLine("-------------------");
            // Arbitrary objects can also be written to the file.
            sw.Write("The date is: ");
            sw.WriteLine();*/
        }
    }
}
