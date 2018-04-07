using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour {


    LineRenderer line;
    int pointCntr;
    Vector3[] positions;

    // Use this for initialization
    void Start () {
        line = transform.GetComponent<LineRenderer>();
        Debug.Log("plop");
        line.positionCount = 5;
        line.SetPosition(0, new Vector3(-1, 1, -21));
        line.SetPosition(1, new Vector3(1, 1, -21));
        line.SetPosition(2, new Vector3(1, -1, -21));
        line.SetPosition(3, new Vector3(-1, -1, -21));
        line.SetPosition(4, new Vector3(-1, 1, -21));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddPoint(float x , float y)
    {

        //line.SetVertexCount(pointCntr);
        pointCntr++;

        line.positionCount = pointCntr;
        line.SetPosition(pointCntr - 1, new Vector3(x, y, -21));
        //line.SetPositions(positions);
    }
}
