using GlobalObject.SceneTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARnold_Scene4_Instantiate : MonoBehaviour {

    private Vector3 PosA4;

    public GameObject Arnold_Scene;
    private GameObject Arnold_SceneT;

    public AudioClip Scene4;
    public AudioClip Scene4_narration;
    public AudioSource Main;

    // Use this for initialization
    void Start()
    {
        PosA4 = GlobalControl.Instance.PosA4;
        Instantiate_Scene4();
    }

    public void Instantiate_Scene4()
    {
        StartCoroutine(Delay_Scene4());
    }

    IEnumerator Delay_Scene4()
    {
        Main.clip = Scene4_narration;
        Main.Play();
        yield return new WaitForSeconds(3);
        Arnold_SceneT = Instantiate(Arnold_Scene, PosA4, Quaternion.identity) as GameObject;
        Main.clip = Scene4;
        Main.Play();
        //Arnold_prejumpT.transform.LookAt(Camera.main.transform);
        StartCoroutine(Destroytimer());
    }

    IEnumerator Destroytimer()
    {
        yield return new WaitForSeconds(Arnold_SceneT.GetComponent<Animation>().clip.length);// + 5);
        SceneLoader.LoadScene("ARnold_Scene5");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
