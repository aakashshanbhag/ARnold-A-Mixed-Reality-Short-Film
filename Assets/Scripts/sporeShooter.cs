using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sporeShooter : MonoBehaviour {

    public GameObject spore;
    public int speed;
    public int lifetime;
    public bool canFire;
    GameObject bulletClone;

    // Use this for initialization
    void Start () {
        canFire = true;	
	}
	
	// Update is called once per frame
	void Update () {


		
	}

    public void Fire()
    {
        if (canFire)
        {
            GetComponent<AudioSource>().Play();
            bulletClone = Instantiate(spore, transform.position, transform.rotation) as GameObject;

            bulletClone.GetComponent<Rigidbody>().AddForce(bulletClone.transform.forward * speed);

            Destroy(bulletClone, lifetime);
            
        }
        canFire = false;


    }
}
