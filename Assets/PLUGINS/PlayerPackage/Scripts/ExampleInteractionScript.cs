using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleInteractionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Activated()
    {
        var objectColor = GetComponent<MeshRenderer>().material.color;
        if (objectColor == Color.blue)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else if (objectColor == Color.green)
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
}
