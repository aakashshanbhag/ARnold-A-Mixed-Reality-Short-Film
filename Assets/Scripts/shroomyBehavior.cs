using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;

public class shroomyBehavior : MonoBehaviour {

    public GameObject target;
    public GameObject shroomHeld;
    public GameObject ReturnNode;
    Vector3 moveTarget;
    public float speed;
    bool retrieve;
    bool returning;
    bool hasShroom;
    public AudioSource shroomySFX;
    public AudioClip popUp;
    public AudioClip chomp;
    public int colIndex;


    //GameObject droppedShroom;

    // Use this for initialization
    void Start () {
        GetComponentInChildren<ParticleSystem>().Stop();
        colIndex = 0;
        retrieve = false;
        returning = false;

	}

    // Update is called once per frame
    void Update() {
        if (retrieve)
        {
            Retriever();
        }
        if (returning)
        {
            Return();
            ReturnNode.GetComponent<BoxCollider>().enabled = true;

        }
        if (hasShroom) {
            Camera.main.GetComponent<sporeShooter>().canFire = false;
        }
        else
        {
            Camera.main.GetComponent<sporeShooter>().canFire = true;
        }
	}

    public void WakeUp()
    {
        returning = true;
        shroomySFX.clip = popUp;
        shroomySFX.Play();
        GetComponentInChildren<ParticleSystem>().Play();
        Camera.main.GetComponent<sporeShooter>().enabled = true;



    }

    public void StartFetch()
    {
        retrieve = true;
    }



    void Retriever()
    {
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, speed);
        GetComponent<lookAt>().enabled = false;
        GetComponent<SphereBasedTagalong>().enabled = false;
        hasShroom = true;

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == target)
        {
            Debug.Log("Collide");
            Destroy(target.gameObject);
            target = null;
            shroomHeld.SetActive(true);
            retrieve = false;
            returning = true;
            shroomySFX.clip = chomp;
            shroomySFX.Play();


        }

        if(other.gameObject == ReturnNode)
        {
            GetComponent<lookAt>().enabled = true;
            GetComponent<SphereBasedTagalong>().enabled = true;
            returning = false;
            ReturnNode.GetComponent<BoxCollider>().enabled = false;
            if (hasShroom)
            {
                DropIt();
                hasShroom = false;
            }
            
        }
    }

    void Return()
    {

        Vector3 dir = ReturnNode.transform.position - transform.position;
        dir.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        transform.position = Vector3.MoveTowards(this.transform.position, ReturnNode.transform.position, speed);
        


    }


    public void DropIt()
    {

        GameObject droppedShroom = Instantiate(shroomHeld, shroomHeld.transform) as GameObject;
        droppedShroom.transform.parent = null;
        droppedShroom.GetComponentInChildren<MeshCollider>().enabled = true;
        droppedShroom.GetComponent<Rigidbody>().useGravity = true;
        Destroy(droppedShroom, 10);

        target = GameObject.Find("mushroom(Clone)");
        if(target != null)
        {
            StartFetch();
        }
        shroomHeld.SetActive(false);
    }

    public void ColorListener()
    {
        colIndex += 1;
        if(colIndex > 3)
        {
            colIndex = 0;
        }

    }
}
