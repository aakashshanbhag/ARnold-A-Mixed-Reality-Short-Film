using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChanger : MonoBehaviour {

    public GameObject Shroomy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ColorCycle()
    {
        Shroomy.GetComponent<shroomyBehavior>().ColorListener();
    }
}
