using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA;

public class ARnold_Scene7_Instantiate : MonoBehaviour
{

    private Vector3 PosA4;

    public GameObject Arnold_Scene;
    public GameObject EndCredits1;
    private GameObject Arnold_SceneT;

    public AudioClip Scene7;
    public AudioClip Scene7_narration_before;
    public AudioClip Scene7_narration_during;
    public AudioClip EndCredits;
    public AudioSource Main;

    // Use this for initialization
    void Start()
    {
        PosA4 = GlobalControl.Instance.PosA4;
        Instantiate_Scene7();
    }

    public void Instantiate_Scene7()
    {
        StartCoroutine(Delay_Scene7());
    }

    IEnumerator Delay_Scene7()
    {
        Main.clip = Scene7_narration_before;
        Main.Play();
        yield return new WaitForSeconds(6);
        Arnold_SceneT = Instantiate(Arnold_Scene, PosA4, Quaternion.identity) as GameObject;
        Main.clip = Scene7;
        Main.Play();
        //Arnold_prejumpT.transform.LookAt(Camera.main.transform);
        StartCoroutine(Destroytimer());
    }

    IEnumerator Destroytimer()
    {
        yield return new WaitForSeconds(30);
        Main.clip = Scene7_narration_during;
        Main.Play();
        StartCoroutine(Destroytimer11());
        
    }

    IEnumerator Destroytimer11()
    {
        yield return new WaitForSeconds(10);
        //yield return new WaitForSeconds(Arnold_SceneT.GetComponent<Animation>().clip.length);// + 5);
        Destroy(Arnold_SceneT);
        GameObject EndShow1 = Instantiate(EndCredits1, Camera.main.transform.position, Quaternion.identity) as GameObject;
        Main.clip = EndCredits;
        Main.Play();
        //SceneLoader.LoadScene("ARnold_Scene4");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
