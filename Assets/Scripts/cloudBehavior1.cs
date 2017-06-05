using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using UnityEngine.VR.WSA.Input;


public class cloudBehavior1 : MonoBehaviour {
    //Renderer rend;
    //public Material[] mats;
    GestureManipulator gestureManipulator;
    public AudioSource selectSFX;
    public AudioClip rainSFX;
    public bool spawned;
    public GameObject hole;
    public ParticleSystem rain;
    public bool isSelected;
    bool manipulating;

    // Use this for initialization
    void Start () {

        //rend = GetComponent<Renderer>();
        gestureManipulator = GetComponent<GestureManipulator>();
        isSelected = false;

    }
	
	// Update is called once per frame
	void Update () {

        if (gestureManipulator.Manipulating){
            IsManipulating();
            manipulating = true;
        }

	}

    public void GazeOn()
    {
            //rend.sharedMaterial = mats[1];
        
    }

    public void GazeOff()
    {
        
            //rend.sharedMaterial = mats[0];
    }

    public void RainCloud()
    {

        //rend.sharedMaterial = mats[2];

            isSelected = true;
            Invoke("UnSelect", 5);

            rain.Play();
            selectSFX.clip = rainSFX;
            selectSFX.Play();
            StartCoroutine(Swoosh());
    }

    IEnumerator Swoosh()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        Destroy(GameObject.Find("Painting"));
    }

    public void Selector()
    {
        if (!manipulating)
        {
            //rend.sharedMaterial = mats[2];
            isSelected = true;
            Invoke("UnSelect", 5);
            //GameObject shatterGo = Instantiate(Resources.Load("Shatter Particle System")) as GameObject;
            //ParticleSystem shatter = shatterGo.GetComponent<ParticleSystem>();
            //shatter.Play();

            //shatter.transform.position = GameObject.Find("Cube").transform.position;

            selectSFX.Play();
            //b = true;
        }


        

    }

    public void IsManipulating()
    {
        //rend.sharedMaterial = mats[3];
        isSelected = false;
    }

    public void EndManipulating()
    {
        Invoke("UnSelect", 1);
    }
    

    void UnSelect()
    {
        isSelected = false;

       // rend.sharedMaterial = mats[0];
        manipulating = false;
        
    }



    public void SpawnHole()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast((transform.position), (new Vector3(0, -1, 0) * 100), out hit))
        {
            Debug.Log(hit.point);
            hole.transform.position = new Vector3(hit.point.x, (hit.point.y-.05f), hit.point.z);
            hole.SetActive(true);
            Destroy(this.gameObject, 2);
        }

    }

}
