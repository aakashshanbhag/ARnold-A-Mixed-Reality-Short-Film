using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlPanel : MonoBehaviour {
    SpriteRenderer rend;
    Color norm;

    // Use this for initialization
    void Start () {
        rend = GetComponent<SpriteRenderer>();
        norm = rend.material.color;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GazeOn()
    {
        rend.material.SetColor("_Color", Color.white);
        Debug.Log("hey");
    }

    public void GazeOff()
    {
        rend.material.SetColor("_Color", norm);
    }

    public void ColorUp()
    {

    }

    public void ColorDown()
    {

    }

    public void SpeedUp()
    {

    }

    public void SpeedDown()
    {

    }
}
