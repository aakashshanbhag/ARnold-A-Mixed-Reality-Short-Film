using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorLib : MonoBehaviour {

    public Texture[] colTextures;
    public int colIndex;
    GameObject Shroomy;

    // Use this for initialization
    void Start () {
        Shroomy = GameObject.Find("shroomy_ex");
        colIndex = 0;
        GetComponent<Renderer>().material.mainTexture = colTextures[colIndex];
		
	}
	
	// Update is called once per frame
	void Update () {
        colIndex = Shroomy.GetComponent<shroomyBehavior>().colIndex;
        GetComponent<Renderer>().material.mainTexture = colTextures[colIndex];

    }
}
