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
