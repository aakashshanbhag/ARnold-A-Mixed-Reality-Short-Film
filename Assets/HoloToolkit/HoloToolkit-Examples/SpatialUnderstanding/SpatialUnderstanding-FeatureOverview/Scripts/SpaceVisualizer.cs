// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.VR.WSA;
using System;
using HoloToolkit.Unity;
using UnityEngine.Windows.Speech;
using GlobalObject.SceneTransition;

public class SpaceVisualizer : LineDrawer
{
    // Singleton
    //public TextMesh A;
    public GameObject gObject;
    bool flag = true;

    // Consts
    const int QueryResultMaxCount = 512;
    const int DisplayResultMaxCount = 32;

    public static SpaceVisualizer Instance;

    private Vector3 PosA1; // prejump
    private Vector3 PosA2; // jump
    private Vector3 PosA3; // post jump
    private Vector3 PosA4; // for wall detection
    public AudioSource Main;

    //============== Scene-1 =================//
    public GameObject Arnold_Scene1;
    private GameObject Arnold_Scene1T;

    public GameObject Arnold_paws;
    private GameObject Arnold_pawsT;

    public GameObject EndCredits;
    private GameObject EndCreditsT;

    public AudioClip ScanTime;
    public AudioClip Scene1;
    public AudioClip Scene1_narration;

    // temporary control variables for Scene-1
    private Quaternion rot; // for holding the rotation of all ARnold animations
    private bool Jump = true; // to ensure that ARnold jumps only once
    private bool Sc1 = true;

    //============== Scene-2 =================//
    public GameObject Arnold_jump;
    private GameObject Arnold_jumpT;

    public GameObject Arnold_postjump;
    private GameObject Arnold_postjumpT;

    public GameObject Arnold_prejump;
    private GameObject Arnold_prejumpT;

    public GameObject Arnold_door;
    private GameObject Arnold_doorT;

    public GameObject Arnold_bellyrub;
    private GameObject Arnold_bellyrubT;
       
    public AudioClip Scene2;
    public AudioClip Scene2_narration;

    //============== Scene-4 =================//
    public GameObject Arnold_Scene4;
    private GameObject Arnold_Scene4T;

    public AudioClip Scene4;
    public AudioClip Scene4_narration;

    //============== Scene-5 =================//
    public GameObject Arnold_Scene5;
    private GameObject Arnold_Scene5T;

    public AudioClip Scene5;

    //============== Scene-6 =================//
    public GameObject Arnold_Scene6;
    private GameObject Arnold_Scene6T;

    public AudioClip Scene6;

    //============== Scene-7 =================//    
    public GameObject Arnold_Scene7;
    public GameObject EndCredits1;
    private GameObject Arnold_Scene7T;

    public AudioClip Scene7;
    public AudioClip Scene7_narration_before;
    public AudioClip Scene7_narration_during;
    public AudioClip EndCredits2;

    // for speech commands
    KeywordRecognizer keywordRecognizer;

    // Privates
    private List<AnimatedBox> lineBoxList = new List<AnimatedBox>();
    private SpatialUnderstandingDllTopology.TopologyResult[] resultsTopology = new SpatialUnderstandingDllTopology.TopologyResult[QueryResultMaxCount];
    private SpatialUnderstandingDllShapes.ShapeResult[] resultsShape = new SpatialUnderstandingDllShapes.ShapeResult[QueryResultMaxCount];

    // Functions
    void Start()
    {
        //    // Setup a keyword recognizer to enable resetting the target location.
        //    List<string> keywords = new List<string>();
        //    keywords.Add("Jump");
        //    keywordRecognizer = new KeywordRecognizer(keywords.ToArray());
        //    keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        //    keywordRecognizer.Start();
        //}

        //private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
        //{
        //    Query_Shape_FindShapeHalfDims("Sittable");
        //Main.Stop();
        Main.clip = ScanTime;
        Main.Play();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void ClearGeometry(bool clearAll = true)
    {
        lineBoxList = new List<AnimatedBox>();
        AppState.Instance.SpaceQueryDescription = "";

        if (clearAll && (LevelSolver.Instance != null))
        {
            LevelSolver.Instance.ClearGeometry(false);
        }
    }

    // to find shapes of different dimensions in room
    public void FindLocations(string shapeName)
    {
        //SpatialMappingManager.Instance.drawVisualMeshes = false;
        // Pin managed object memory going to native code.
        IntPtr resultsShapePtr =
        SpatialUnderstanding.Instance.UnderstandingDLL.
        PinObject(resultsShape);

        // Find the half dimensions of "Sittable" objects via the DLL.
        int shapeCount = SpatialUnderstandingDllShapes.QueryShape_FindShapeHalfDims(
            shapeName,
            resultsShape.Length, resultsShapePtr);

        //rot = Quaternion.identity;

        // Process found results.
        // for (int i = 0; i < 1; i++)
        // {
        // Create a Arnold at each "sittable" location.
        //Arnold_prejumpT =  Instantiate(Arnold_prejump, resultsShape[i].position, rot) as GameObject;
        //Arnold_prejumpT.transform.LookAt(Camera.main.transform);
        // Log the half bounds of our sittable area.
        //Console.WriteLine(resultsShape[i].halfDims.sqrMagnitude < 0.01f) ?
        //    new Vector3(0.25f, 0.025f, 0.25f) : resultsShape[i].halfDims)
        // }
        PosA1 = resultsShape[0].position;
        FindFloorLocations();
        // StartCoroutine(Destroytimer());
    }

    // find location of floor
    public void FindFloorLocations()
    {
        // Setup
        float minWidthOfWallSpace = 1.0f;
        float minHeightAboveFloor = 1.0f;

        // Pin managed object memory going to native code.
        IntPtr resultsTopologyPtr = SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(resultsTopology);

        // Find the half dimensions of "Sittable" objects via the DLL.
        //int shapeCount = SpatialUnderstandingDllShapes.QueryShape_FindShapeHalfDims(
        //    "Sittable",
        //    resultsShape.Length, resultsShapePtr);

        int locationCount = SpatialUnderstandingDllTopology.QueryTopology_FindPositionsOnFloor(
           minWidthOfWallSpace, minHeightAboveFloor,
           resultsTopology.Length, resultsTopologyPtr);

        // Process found results.
        for (int i = 0; i < 1; i++)
        {
            // Create a Arnold at each "sittable" location.
            PosA3 = resultsTopology[i].position;
            //GlobalControl.Instance.PosA3 = PosA3;
            // PosA3.x = PosA1.x;// - 0.5f;
            //  PosA3.z = PosA1.z - 0.6f;
            //  Arnold_postjumpT = Instantiate(Arnold_postjump, PosA3, rot) as GameObject;
            //  Arnold_postjumpT.transform.LookAt(Camera.main.transform);
        }
        // StartCoroutine(Destroytimer2());
    }

    public void Query_PlayspaceAlignment()
    {
        // First clear all our geo
        ClearGeometry();

        // Only if we're enabled
        if (!SpatialUnderstanding.Instance.AllowSpatialUnderstanding)
        {
            return;
        }

        // Alignment information
        SpatialUnderstandingDll.Imports.QueryPlayspaceAlignment(SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticPlayspaceAlignmentPtr());
        SpatialUnderstandingDll.Imports.PlayspaceAlignment alignment = SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticPlayspaceAlignment();

        // Box for the space
        float timeDelay = (float)lineBoxList.Count * AnimatedBox.DelayPerItem;
        lineBoxList.Add(
            new AnimatedBox(
                timeDelay,
                new Vector3(alignment.Center.x, (alignment.CeilingYValue + alignment.FloorYValue) * 0.5f, alignment.Center.z),
                Quaternion.LookRotation(alignment.BasisZ, alignment.BasisY),
                Color.magenta,
                new Vector3(alignment.HalfDims.x, (alignment.CeilingYValue - alignment.FloorYValue) * 0.5f, alignment.HalfDims.z))
        );
        AppState.Instance.SpaceQueryDescription = "Playspace Alignment OBB";
    }

    public void Query_Topology_FindPositionOnWall()
    {
        ClearGeometry();

        // Only if we're enabled
        if (!SpatialUnderstanding.Instance.AllowSpatialUnderstanding)
        {
            return;
        }

        // Setup
        float minHeightOfWallSpace = 0.5f;
        float minWidthOfWallSpace = 0.75f;
        float minHeightAboveFloor = 1.25f;
        float minFacingClearance = 1.5f;

        // Query
        IntPtr resultsTopologyPtr = SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(resultsTopology);
        int locationCount = SpatialUnderstandingDllTopology.QueryTopology_FindPositionsOnWalls(
            minHeightOfWallSpace, minWidthOfWallSpace, minHeightAboveFloor, minFacingClearance,
            resultsTopology.Length, resultsTopologyPtr);

        // Output
        HandleResults_Topology("Find Position On Wall", locationCount, new Vector3(minWidthOfWallSpace, minHeightOfWallSpace, 0.025f), Color.blue);
    }

    public void Query_Topology_FindLargePositionsOnWalls()
    {
        // Setup
        float minHeightOfWallSpace = 1.0f;
        float minWidthOfWallSpace = 1.5f;
        float minHeightAboveFloor = 1.5f;
        float minFacingClearance = 0.5f;

        // Only if we're enabled
        if (!SpatialUnderstanding.Instance.AllowSpatialUnderstanding)
        {
            return;
        }

        // Query
        IntPtr resultsTopologyPtr = SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(resultsTopology);
        int locationCount = SpatialUnderstandingDllTopology.QueryTopology_FindLargePositionsOnWalls(
            minHeightOfWallSpace, minWidthOfWallSpace, minHeightAboveFloor, minFacingClearance,
            resultsTopology.Length, resultsTopologyPtr);

        // Output
        HandleResults_Topology("Find Large Positions On Walls", locationCount, new Vector3(minWidthOfWallSpace, minHeightOfWallSpace, 0.025f), Color.yellow);
    }

    public void Query_Topology_FindLargeWall()
    {
        ClearGeometry();

        // Only if we're enabled
        if (!SpatialUnderstanding.Instance.AllowSpatialUnderstanding)
        {
            return;
        }

        // Query
        IntPtr wallPtr = SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(resultsTopology);
        int wallCount = SpatialUnderstandingDllTopology.QueryTopology_FindLargestWall(
            wallPtr);
        if (wallCount == 0)
        {
            AppState.Instance.SpaceQueryDescription = "Find Largest Wall (0)";
            return;
        }

        // Add the line boxes
        //float timeDelay = (float)lineBoxList.Count * AnimatedBox.DelayPerItem;
        //lineBoxList.Add(
        //    new AnimatedBox(
        //        timeDelay,
        //        resultsTopology[0].position,
        //        Quaternion.LookRotation(resultsTopology[0].normal, Vector3.up),
        //        Color.magenta,
        //        new Vector3(resultsTopology[0].width, resultsTopology[0].length, 0.05f) * 0.5f)
        //);
        //Vector3 tempPos = new Vector3((resultsTopology[0].width * .5f), (resultsTopology[0].length * .5f), 0.05f);
        //GameObject TempOBJ = Instantiate(gObject, tempPos, Quaternion.identity) as GameObject;
        //AppState.Instance.SpaceQueryDescription = "Find Largest Wall (1)";

        if(Sc1 == true)
        {
            PosA4 = resultsTopology[0].position;
            PosA4.y = PosA3.y;
            //GlobalControl.Instance.PosA4 = PosA4;
            Sc1 = false;
        }
        //Arnold_Scene1T = Instantiate(Arnold_Scene1, PosA4, Quaternion.identity) as GameObject;
    }

    //============== Scene-1 starts here when player says "Here Boy" =================//
    public void ARnold_Scene1()
    {
        //AppState.Instance.PrimaryText = "";
        Main.clip = Scene1_narration;
        Main.Play();
        StartCoroutine(Scene1_N());
    }

    IEnumerator Scene1_N()
    {
        yield return new WaitForSeconds(4);
        Arnold_Scene1T = Instantiate(Arnold_Scene1, PosA4, Quaternion.identity) as GameObject;
        // WorldAnchor anchor = Arnold_Scene1T.AddComponent<WorldAnchor>();
        // GlobalControl.Instance.anchor1 = anchor;
        // GlobalControl.Instance.PosA4 = anchor.transform.position;
        Main.clip = Scene1;
        Main.Play();
        Destroy(GameObject.Find("SpatialMapping"));
        Destroy(GameObject.Find("SpatialUnderstanding"));
        StartCoroutine(DestroyScene1_1());
    }

    IEnumerator DestroyScene1_1()
    {
        yield return new WaitForSeconds(Arnold_Scene1T.GetComponent<Animation>().clip.length);
        Destroy(Arnold_Scene1T);
        EndCreditsT = Instantiate(EndCredits, GazeManager.Instance.Position, Quaternion.identity) as GameObject;
        StartCoroutine(DestroyScene1_2());
    }

    //============== Scene-1 ends and after 5 seconds Scene-2 starts =================//
    IEnumerator DestroyScene1_2()
    {
        yield return new WaitForSeconds(3);
        GameObject.Destroy(EndCreditsT);
        // SceneLoader.LoadScene("ARnold_Scene2");
        StartCoroutine(DelayScene2_prejump());
    }

    IEnumerator DelayScene2_prejump()
    {
        Arnold_doorT = Instantiate(Arnold_door, PosA4, Quaternion.identity) as GameObject;
        Main.clip = Scene2_narration;
        Main.Play();
        yield return new WaitForSeconds(5);
        Arnold_prejumpT = Instantiate(Arnold_prejump, PosA1, Quaternion.identity) as GameObject;
        Main.clip = Scene2;
        Main.Play();
        Arnold_prejumpT.transform.LookAt(Camera.main.transform);
        StartCoroutine(DestroyScene2_prejump());
    }

    IEnumerator DestroyScene2_prejump()
    {
        yield return new WaitForSeconds(Arnold_prejumpT.GetComponent<Animation>().clip.length);// + 5);
        GameObject.Destroy(Arnold_prejumpT);
        PosA2.y = PosA1.y;
        PosA2.x = PosA1.x;// - 0.5f;
        PosA2.z = PosA1.z - 0.4f;
        Arnold_jumpT = Instantiate(Arnold_jump, PosA2, Quaternion.identity) as GameObject;
        Arnold_jumpT.transform.LookAt(Camera.main.transform);
        StartCoroutine(DestroyScene2_jump());
    }

    IEnumerator DestroyScene2_jump()
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
        StartCoroutine(DestroyScene2_postjump());
    }

    IEnumerator DestroyScene2_postjump()
    {
        yield return new WaitForSeconds(Arnold_postjumpT.GetComponent<Animation>().clip.length);// + 5);
        GameObject.Destroy(Arnold_postjumpT);
        Arnold_bellyrubT = Instantiate(Arnold_bellyrub, PosA3, Quaternion.identity) as GameObject;
        Arnold_bellyrubT.transform.LookAt(Camera.main.transform);
        StartCoroutine(DestroyScene2_bellyrub());
    }

    IEnumerator DestroyScene2_bellyrub()
    {
        yield return new WaitForSeconds(Arnold_bellyrubT.GetComponent<Animation>().clip.length);// + 5);
        GameObject.Destroy(Arnold_bellyrubT);
        GameObject.Destroy(Arnold_doorT);
        // SceneLoader.LoadScene("ARnold_Scene4");
        StartCoroutine(DestroyScene2());
    }

    //============== Scene-2 ends and after 5 seconds Scene-4 starts =================//
    IEnumerator DestroyScene2()
    {
        yield return new WaitForSeconds(3);
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
        Arnold_Scene4T = Instantiate(Arnold_Scene4, PosA4, Quaternion.identity) as GameObject;
        Main.clip = Scene4;
        Main.Play();
        //Arnold_prejumpT.transform.LookAt(Camera.main.transform);
        StartCoroutine(DestroyScene4_1());
    }

    IEnumerator DestroyScene4_1()
    {
        yield return new WaitForSeconds(Arnold_Scene4T.GetComponent<Animation>().clip.length);// + 5);
        GameObject.Destroy(Arnold_Scene4T);
        // SceneLoader.LoadScene("ARnold_Scene5");
        StartCoroutine(DestroyScene4_2());
    }

    //============== Scene-4 ends and after 5 seconds Scene-5 starts =================//
    IEnumerator DestroyScene4_2()
    {
        yield return new WaitForSeconds(3);
        Instantiate_Scene5();
    }

    public void Instantiate_Scene5()
    {
        StartCoroutine(Delay_Scene5());
    }

    IEnumerator Delay_Scene5()
    {
        yield return new WaitForSeconds(3);
        Arnold_Scene5T = Instantiate(Arnold_Scene5, PosA3, Quaternion.identity) as GameObject;
        //Arnold_SceneT = Instantiate(Arnold_Scene, PosA1, Quaternion.identity) as GameObject;
        Main.clip = Scene5;
        Main.Play();
        //Arnold_prejumpT.transform.LookAt(Camera.main.transform);
        StartCoroutine(DestroyScene5_1());
    }

    IEnumerator DestroyScene5_1()
    {
        yield return new WaitForSeconds(Arnold_Scene5T.GetComponent<Animation>().clip.length);// + 5);
        GameObject.Destroy(Arnold_Scene5T);
        // SceneLoader.LoadScene("ARnold_Scene6");
        StartCoroutine(DestroyScene5_2());
    }

    //============== Scene-5 ends and after 5 seconds Scene-6 starts =================//
    IEnumerator DestroyScene5_2()
    {
        yield return new WaitForSeconds(3);
        Instantiate_Scene6();
    }

    public void Instantiate_Scene6()
    {
        StartCoroutine(Delay_Scene6());
    }

    IEnumerator Delay_Scene6()
    {
        yield return new WaitForSeconds(3);
        Arnold_Scene6T = Instantiate(Arnold_Scene6, PosA4, Quaternion.identity) as GameObject;
        Main.clip = Scene6;
        Main.Play();
        //Arnold_prejumpT.transform.LookAt(Camera.main.transform);
        StartCoroutine(DestroyScene6_1());
    }

    IEnumerator DestroyScene6_1()
    {
        yield return new WaitForSeconds(Arnold_Scene6T.GetComponent<Animation>().clip.length);// + 5);
        GameObject.Destroy(Arnold_Scene6T);
        // SceneLoader.LoadScene("ARnold_Scene7");
        StartCoroutine(DestroyScene6_2());
    }

    //============== Scene-6 ends and after 5 seconds Scene-7 starts =================//
    IEnumerator DestroyScene6_2()
    {
        yield return new WaitForSeconds(3);
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
        Arnold_Scene7T = Instantiate(Arnold_Scene7, PosA4, Quaternion.identity) as GameObject;
        Main.clip = Scene7;
        Main.Play();
        //Arnold_prejumpT.transform.LookAt(Camera.main.transform);
        StartCoroutine(DestroyScene7_1());
    }

    IEnumerator DestroyScene7_1()
    {
        yield return new WaitForSeconds(30);
        Main.clip = Scene7_narration_during;
        Main.Play();
        StartCoroutine(DestroyScene7_2());

    }

    IEnumerator DestroyScene7_2()
    {
        yield return new WaitForSeconds(10);
        //yield return new WaitForSeconds(Arnold_SceneT.GetComponent<Animation>().clip.length);// + 5);
        Destroy(Arnold_Scene7T);
        GameObject EndShow1 = Instantiate(EndCredits1, Camera.main.transform.position, Quaternion.identity) as GameObject;
        Main.clip = EndCredits2;
        Main.Play();
        //SceneLoader.LoadScene("ARnold_Scene4");
    }

    public void Query_Topology_FindPositionsOnFloor()
    {
        // Setup
        float minWidthOfWallSpace = 1.0f;
        float minHeightAboveFloor = 1.0f;

        // Only if we're enabled
        if (!SpatialUnderstanding.Instance.AllowSpatialUnderstanding)
        {
            return;
        }

        // Query
        IntPtr resultsTopologyPtr = SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(resultsTopology);
        int locationCount = SpatialUnderstandingDllTopology.QueryTopology_FindPositionsOnFloor(
            minWidthOfWallSpace, minHeightAboveFloor,
            resultsTopology.Length, resultsTopologyPtr);

        // Output
        HandleResults_Topology("Find Positions On Floor", locationCount, new Vector3(minWidthOfWallSpace, 0.025f, minHeightAboveFloor), Color.red);
    }

    public void Query_Topology_FindLargestPositionsOnFloor()
    {
        // Query
        IntPtr resultsTopologyPtr = SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(resultsTopology);
        int locationCount = SpatialUnderstandingDllTopology.QueryTopology_FindLargestPositionsOnFloor(
            resultsTopology.Length, resultsTopologyPtr);

        // Output
        HandleResults_Topology("Find Largest Positions On Floor", locationCount, new Vector3(1.0f, 1.0f, 0.025f), Color.yellow);
    }

    public void Query_Topology_FindPositionsPlaceable()
    {
        // Setup
        float minHeight = 0.125f;
        float maxHeight = 1.0f;
        float minFacingClearance = 1.0f;

        // Only if we're enabled
        if (!SpatialUnderstanding.Instance.AllowSpatialUnderstanding)
        {
            return;
        }

        // Query
        IntPtr resultsTopologyPtr = SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(resultsTopology);
        int locationCount = SpatialUnderstandingDllTopology.QueryTopology_FindPositionsSittable(
            minHeight, maxHeight, minFacingClearance,
            resultsTopology.Length, resultsTopologyPtr);

        // Output
        HandleResults_Topology("Find Placeable Positions", locationCount, new Vector3(0.25f, 0.025f, 0.25f), Color.cyan);
    }

    public void Query_Shape_FindPositionsOnShape(string shapeName)
    {
        // Setup
        float minRadius = 0.1f;

        // Only if we're enabled
        if (!SpatialUnderstanding.Instance.AllowSpatialUnderstanding)
        {
            return;
        }

        // Query
        IntPtr resultsShapePtr = SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(resultsShape);
        int shapeCount = SpatialUnderstandingDllShapes.QueryShape_FindPositionsOnShape(
            shapeName, minRadius,
            resultsShape.Length, resultsShapePtr);

        // Output
        HandleResults_Shape("Find Positions on Shape '" + shapeName + "'", shapeCount, Color.cyan, new Vector3(0.1f, 0.025f, 0.1f));
    }

    private void HandleResults_Topology(string visDesc, int locationCount, Vector3 boxFullDims, Color color)
    {
        // First clear all our geo
        ClearGeometry();

        // Only if we're enabled
        if (!SpatialUnderstanding.Instance.AllowSpatialUnderstanding)
        {
            return;
        }

        // Add the line boxes (we may have more results than boxes - pick evenly across the results in that case)
        int lineInc = Mathf.CeilToInt((float)locationCount / (float)DisplayResultMaxCount);
        int boxesDisplayed = 0;
        for (int i = 0; i < locationCount; i += lineInc)
        {
            float timeDelay = (float)lineBoxList.Count * AnimatedBox.DelayPerItem;
            lineBoxList.Add(
                new AnimatedBox(
                    timeDelay,
                    resultsTopology[i].position,
                    Quaternion.LookRotation(resultsTopology[i].normal, Vector3.up),
                    Color.blue,
                    boxFullDims * 0.5f)
            );
            ++boxesDisplayed;
        }

        // Vis description
        if (locationCount == boxesDisplayed)
        {
            AppState.Instance.SpaceQueryDescription = string.Format("{0} ({1})", visDesc, locationCount);
        }
        else
        {
            AppState.Instance.SpaceQueryDescription = string.Format("{0} (found={1}, displayed={2})", visDesc, locationCount, boxesDisplayed);
        }
    }

    private void HandleResults_Shape(string visDesc, int shapeCount, Color color, Vector3 defaultHalfDims)
    {
        // First clear all our geo
        ClearGeometry();

        // Only if we're enabled
        if (!SpatialUnderstanding.Instance.AllowSpatialUnderstanding)
        {
            return;
        }

        // Alignment information
        SpatialUnderstandingDll.Imports.QueryPlayspaceAlignment(SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticPlayspaceAlignmentPtr());
        SpatialUnderstandingDll.Imports.PlayspaceAlignment alignment = SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticPlayspaceAlignment();

        // Add the line boxes (we may have more results than boxes - pick evenly across the results in that case)
        int lineInc = Mathf.CeilToInt((float)shapeCount / (float)DisplayResultMaxCount);
        int boxesDisplayed = 0;
        for (int i = 0; i < shapeCount; i += lineInc)
        {
            float timeDelay = (float)lineBoxList.Count * AnimatedBox.DelayPerItem;
            lineBoxList.Add(
                new AnimatedBox(
                    timeDelay,
                    resultsShape[i].position,
                    Quaternion.LookRotation(alignment.BasisZ, alignment.BasisY),
                    Color.blue,
                    (resultsShape[i].halfDims.sqrMagnitude < 0.01f) ? defaultHalfDims : resultsShape[i].halfDims)
            );
            ++boxesDisplayed;
        }

        // Vis description
        if (shapeCount == boxesDisplayed)
        {
            AppState.Instance.SpaceQueryDescription = string.Format("{0} ({1})", visDesc, shapeCount);
        }
        else
        {
            AppState.Instance.SpaceQueryDescription = string.Format("{0} (found={1}, displayed={2})", visDesc, shapeCount, boxesDisplayed);
        }
    }

    private bool Draw_LineBoxList()
    {
        bool needsUpdate = false;
        for (int i = 0; i < lineBoxList.Count; ++i)
        {
            needsUpdate |= Draw_AnimatedBox(lineBoxList[i]);
        }
        return needsUpdate;
    }

    private void Update_Queries()
    {
        // Queries - basics
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearGeometry();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Query_PlayspaceAlignment();
        }

        // Queries - topology
        if (Input.GetKeyDown(KeyCode.S))
        {
            Query_Topology_FindPositionOnWall();
            //FindSittableLocations();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Query_Topology_FindLargePositionsOnWalls();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Query_Topology_FindLargeWall();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Query_Topology_FindPositionsOnFloor();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Query_Topology_FindLargestPositionsOnFloor();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Query_Topology_FindPositionsPlaceable();
        }

        // Queries - shapes
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Query_Shape_FindShapeHalfDims("All Surfaces");
            //FindSittableLocations();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //Query_Shape_FindPositionsOnShape("Sittable");
            Query_Shape_FindShapeHalfDims("Sittable");
            //FindSittableLocations();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Query_Shape_FindShapeHalfDims("Chair");
            //FindSittableLocations();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Query_Shape_FindShapeHalfDims("Large Surface");
            //FindSittableLocations();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Query_Shape_FindShapeHalfDims("Large Empty Surface");
            //FindSittableLocations();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Query_Shape_FindShapeHalfDims("Couch");
            //FindSittableLocations();
        }
    }

    public void Query_Shape_FindShapeHalfDims(string shapeName)
    {
        // Only if we're enabled
        if (!SpatialUnderstanding.Instance.AllowSpatialUnderstanding)
        {
            return;
        }

        // Query
        // IntPtr resultsShapePtr = SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(resultsShape);
        //int shapeCount = SpatialUnderstandingDllShapes.QueryShape_FindShapeHalfDims(
        //     shapeName,
        //     resultsShape.Length, resultsShapePtr);

        // Output
        // HandleResults_Shape("Find Shape Min/Max '" + shapeName + "'", shapeCount, Color.blue, new Vector3(0.25f, 0.025f, 0.25f));
        if (Jump == true)
        {
            Jump = false;
            FindLocations(shapeName);
        }   
        //FindFloorLocations();
    }

    // timer to destroy ARnold postjump animation
    //IEnumerator Destroytimer2()
    //{
    //    yield return new WaitForSeconds(Arnold_postjumpT.GetComponent<Animation>().clip.length);// + 5);
    //    GameObject.Destroy(Arnold_postjumpT);
    //    BellyRub();
    //}

    //public void BellyRub()
    //{
    //    Arnold_bellyrubT = Instantiate(Arnold_bellyrub, PosA3, rot) as GameObject;
    //    Arnold_bellyrubT.transform.LookAt(Camera.main.transform);
    //    StartCoroutine(Destroytimer3());
    //}

    //IEnumerator Destroytimer3()
    //{
    //    yield return new WaitForSeconds(Arnold_bellyrubT.GetComponent<Animation>().clip.length);// + 5);
    //    //GameObject.Destroy(Arnold_bellyrubT); 
    //}
    IEnumerator Destroytimer()
    {
        yield return new WaitForSeconds(Arnold_pawsT.GetComponent<Animation>().clip.length);
        Destroy(Arnold_pawsT);
        flag = true;
    }

    public void Update()
    {
        Debug.Log(Camera.main.transform.position.y);
        // Queries
        //GlobalControl.Instance.PosCam =  Camera.main.gameObject.transform.position;

        if (SpatialUnderstanding.Instance.ScanState == SpatialUnderstanding.ScanStates.Done)
        {
            Destroy(Arnold_pawsT);
            //Update_Queries();
            Query_Shape_FindShapeHalfDims("Sittable");
            if(Jump == false)
            {
                Query_Topology_FindLargeWall();
            }  
        }

        if (SpatialUnderstanding.Instance.ScanState != SpatialUnderstanding.ScanStates.Done && flag==true)
        {
            //FindFloorLocations();
            Arnold_pawsT = Instantiate(Arnold_paws, GazeManager.Instance.Position, Quaternion.identity) as GameObject;
            flag = false;
            StartCoroutine(Destroytimer());  
        }

        // Lines: Begin
        LineDraw_Begin();

        // Drawers
        bool needsUpdate = false;
        needsUpdate |= Draw_LineBoxList();

        // Lines: Finish up
        LineDraw_End(needsUpdate);
    }
}
