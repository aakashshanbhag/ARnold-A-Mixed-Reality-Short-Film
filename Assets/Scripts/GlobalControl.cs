using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    public Vector3 PosA1; // prejump animation postion chair
    public Vector3 PosA3; // post jump animation position floor
    public Vector3 PosA4; // wall position
    public Vector3 PosCam; // track camera position

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        PosCam = Camera.main.transform.position;
    }
}

