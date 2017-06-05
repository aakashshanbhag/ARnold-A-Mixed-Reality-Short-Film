using GlobalObject.SceneTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARnold_Scene2_Instantiate : MonoBehaviour {

    private Vector3 PosA1;
    private Vector3 PosA2;
    private Vector3 PosA3;
    private Vector3 PosA4; // wall position

    //Define a new GameObjects for ARnold's animations
    public GameObject Arnold_prejump;
    private GameObject Arnold_prejumpT;

    public GameObject Arnold_door;
    private GameObject Arnold_doorT;

    public GameObject Arnold_jump;
    private GameObject Arnold_jumpT;

    public GameObject Arnold_postjump;
    private GameObject Arnold_postjumpT;

    public GameObject Arnold_bellyrub;
    private GameObject Arnold_bellyrubT;

    public AudioClip Scene2;
    public AudioClip Scene2_narration;
    public AudioSource Main;

    // Use this for initialization
    void Start()
    {
        PosA3 = GlobalControl.Instance.PosA3;
        PosA4 = GlobalControl.Instance.PosA4;
        PosA1 = GlobalControl.Instance.PosA1;
        Instantiate_prejump();
    }

    public void Instantiate_prejump()
    {
        StartCoroutine(Delay_prejump());
    }

    IEnumerator Delay_prejump()
    {
        Arnold_doorT = Instantiate(Arnold_door, PosA4, Quaternion.identity) as GameObject;
        Main.clip = Scene2_narration;
        Main.Play();
        yield return new WaitForSeconds(5);
        Arnold_prejumpT = Instantiate(Arnold_prejump, PosA1, Quaternion.identity) as GameObject;
        Main.clip = Scene2;
        Main.Play();
        Arnold_prejumpT.transform.LookAt(Camera.main.transform);
        StartCoroutine(Destroytimer());
    }

    IEnumerator Destroytimer()
    {
        yield return new WaitForSeconds(Arnold_prejumpT.GetComponent<Animation>().clip.length);// + 5);
        GameObject.Destroy(Arnold_prejumpT);
        PosA2.y = PosA1.y;
        PosA2.x = PosA1.x;// - 0.5f;
        PosA2.z = PosA1.z - 0.4f;
        Arnold_jumpT = Instantiate(Arnold_jump, PosA2, Quaternion.identity) as GameObject;
        Arnold_jumpT.transform.LookAt(Camera.main.transform);
        StartCoroutine(Destroytimer1());
    }

    IEnumerator Destroytimer1()
    {
        yield return new WaitForSeconds(0.8f);
        GameObject.Destroy(Arnold_jumpT);
        Instantiate_postjump();
    }

    public void Instantiate_postjump()
    {
        PosA3.x = PosA1.x;// - 0.5f;
        PosA3.z = PosA1.z - 0.6f;
        Arnold_postjumpT = Instantiate(Arnold_postjump, PosA3, Quaternion.identity) as GameObject;
        Arnold_postjumpT.transform.LookAt(Camera.main.transform);
        StartCoroutine(Destroytimer2());
    }

    IEnumerator Destroytimer2()
    {
        yield return new WaitForSeconds(Arnold_postjumpT.GetComponent<Animation>().clip.length);// + 5);
        GameObject.Destroy(Arnold_postjumpT);
        Arnold_bellyrubT = Instantiate(Arnold_bellyrub, PosA3, Quaternion.identity) as GameObject;
        Arnold_bellyrubT.transform.LookAt(Camera.main.transform);
        StartCoroutine(Destroytimer3());
    }

    IEnumerator Destroytimer3()
    {
        yield return new WaitForSeconds(Arnold_bellyrubT.GetComponent<Animation>().clip.length);// + 5);
        SceneLoader.LoadScene("ARnold_Scene4");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
