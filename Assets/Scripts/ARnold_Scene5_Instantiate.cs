using GlobalObject.SceneTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARnold_Scene5_Instantiate : MonoBehaviour {

    private Vector3 PosA3;
    //private Vector3 PosA1;

    public GameObject Arnold_Scene;
    private GameObject Arnold_SceneT;

    public AudioClip Scene5;
    public AudioSource Main;

    // Use this for initialization
    void Start()
    {
        PosA3 = GlobalControl.Instance.PosA3;
        //PosA1 = GlobalControl.Instance.PosA1;
        Instantiate_Scene5();
    }

    public void Instantiate_Scene5()
    {
        StartCoroutine(Delay_Scene5());
    }

    IEnumerator Delay_Scene5()
    {
        yield return new WaitForSeconds(3);
        Arnold_SceneT = Instantiate(Arnold_Scene, PosA3, Quaternion.identity) as GameObject;
        //Arnold_SceneT = Instantiate(Arnold_Scene, PosA1, Quaternion.identity) as GameObject;
        Main.clip = Scene5;
        Main.Play();
        //Arnold_prejumpT.transform.LookAt(Camera.main.transform);
        StartCoroutine(Destroytimer());
    }

    IEnumerator Destroytimer()
    {
        yield return new WaitForSeconds(Arnold_SceneT.GetComponent<Animation>().clip.length);// + 5);
        SceneLoader.LoadScene("ARnold_Scene6");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
