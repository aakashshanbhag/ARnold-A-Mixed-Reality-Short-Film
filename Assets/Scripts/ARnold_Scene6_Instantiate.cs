using GlobalObject.SceneTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA;

public class ARnold_Scene6_Instantiate : MonoBehaviour {

    private Vector3 PosA4;

    public GameObject Arnold_Scene;
    private GameObject Arnold_SceneT;

    public AudioClip Scene6;
    public AudioSource Main;

    // Use this for initialization
    void Start()
    {
        PosA4 = GlobalControl.Instance.PosA4;
        Instantiate_Scene6();
    }

    public void Instantiate_Scene6()
    {
        StartCoroutine(Delay_Scene6());
    }

    IEnumerator Delay_Scene6()
    {
        yield return new WaitForSeconds(3);
        Arnold_SceneT = Instantiate(Arnold_Scene, PosA4, Quaternion.identity) as GameObject;
        Main.clip = Scene6;
        Main.Play();
        //Arnold_prejumpT.transform.LookAt(Camera.main.transform);
        StartCoroutine(Destroytimer());
    }

    IEnumerator Destroytimer()
    {
        yield return new WaitForSeconds(Arnold_SceneT.GetComponent<Animation>().clip.length);// + 5);
        SceneLoader.LoadScene("ARnold_Scene7");
    }
    	
	// Update is called once per frame
	void Update () {
		
	}
}
