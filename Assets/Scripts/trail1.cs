using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trail1 : MonoBehaviour {
    public GameObject paintingTwo;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(Destroytimer());
        Debug.Log("Here");
    }
	
	IEnumerator Destroytimer()
    {
        yield return new WaitForSeconds(paintingTwo.GetComponent<Animation>().clip.length);
        Destroy(paintingTwo);
        Debug.Log("Destroyed");
    }
}
