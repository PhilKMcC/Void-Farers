using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public GameObject kamikazePrefab;
    public GameObject shootingPrefab;
    public GameObject mainCamera;

    public float asteroidFrequency = 3;
    public float kamikazeFrequency = 5;
    public float shootingFrequency = 7;

    private const int exclusionNum = 8;

    private int exclBuffer = 50;

    private float[,] exclusionZones;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instantiateExclusionZones();
        StartCoroutine(spawn(asteroidFrequency, asteroidPrefab));
        StartCoroutine(spawn(kamikazeFrequency, kamikazePrefab));
        StartCoroutine(spawn(shootingFrequency, shootingPrefab));
    }

    /*// Update is called once per frame
    void Update()
    {
        
    }*/

    private IEnumerator spawn(float timer, GameObject toSpawn)
    {
        yield return new WaitForSeconds(timer * (UnityEngine.Random.Range(1, 300) / 100));
        //Set xVal and yVal to randomRange outside of camera bounds
        int section = UnityEngine.Random.Range(0, 8);
        float xVal = 0;
        float yVal = 0;
        switch(section)
        {
            case 0:
                xVal = mainCamera.transform.position.x + exclBuffer + UnityEngine.Random.Range(0, 50);
                yVal = mainCamera.transform.position.y + exclBuffer + UnityEngine.Random.Range(0, 50);
                break;
            case 1:
                xVal = mainCamera.transform.position.x - exclBuffer - UnityEngine.Random.Range(0, 50);
                yVal = mainCamera.transform.position.y - exclBuffer - UnityEngine.Random.Range(0, 50);
                break;
            case 2:
                xVal = mainCamera.transform.position.x - exclBuffer - UnityEngine.Random.Range(0, 50);
                yVal = mainCamera.transform.position.y + exclBuffer + UnityEngine.Random.Range(0, 50);
                break;
            case 3:
                xVal = mainCamera.transform.position.x + exclBuffer + UnityEngine.Random.Range(0, 50);
                yVal = mainCamera.transform.position.y - exclBuffer - UnityEngine.Random.Range(0, 50);
                break;
            case 4:
                xVal = mainCamera.transform.position.x + UnityEngine.Random.Range(-exclBuffer, exclBuffer);
                yVal = mainCamera.transform.position.y - exclBuffer - UnityEngine.Random.Range(0, 50);
                break;
            case 5:
                xVal = mainCamera.transform.position.x + UnityEngine.Random.Range(-exclBuffer, exclBuffer);
                yVal = mainCamera.transform.position.y + exclBuffer + UnityEngine.Random.Range(0, 50);
                break;
            case 6:
                xVal = mainCamera.transform.position.x + exclBuffer + UnityEngine.Random.Range(0, 50);
                yVal = mainCamera.transform.position.y + UnityEngine.Random.Range(-exclBuffer, exclBuffer);
                break;
            case 7:
                xVal = mainCamera.transform.position.x - exclBuffer - UnityEngine.Random.Range(0, 50);
                yVal = mainCamera.transform.position.y + UnityEngine.Random.Range(-exclBuffer, exclBuffer);
                break;
        }
        //Search exclusion zones to see if both xval and yval are not within the bounds of each part of the set
        if (checkExclusionZones(xVal, yVal))
        {
            GameObject spawned = Instantiate(toSpawn, new Vector3(xVal, yVal, 0), Quaternion.identity);
        }
        StartCoroutine(spawn(timer, toSpawn));

    }

    void instantiateExclusionZones()
    {
        //Xmin, Xmax, Ymin, Ymax
        exclusionZones = new float[exclusionNum, 4] { { -78, -3, -25, 70 }, { -265, -234, 36, 67 }, { -475, -383, 274, 315 }, { -494, -422, -257, -195 }, { -170, -43, -285, -214 }, { -140, 239, -80, -47 }, { 97, 109, 123, 134 }, { 170, 247, 180, 257 } };
        /*float[] spawn = {-78, -3, -25, 70};
        float[] triangleDemo = {-265, -234, 36, 67};
        float[] errZone = {-475, -383, 274, 315};
        float[] snowZone = {-494, -422, -257, -195};
        float[] bossZone = {-170, -43, -285, -214};
        float[] desertZone = {-140, 239, -80, -47};
        float[] house = {97, 109, 123, 134};
        float[] triangleZone = {170, 247, 180, 257};*/

    }
    
    public bool checkExclusionZones(float x, float y)
    {
        int i;
        for(i = 0; i < exclusionNum; i++)
        {
            if((x >= exclusionZones[i, 0] && x <= exclusionZones[i, 1]) && (y >= exclusionZones[i, 2] && y <= exclusionZones[i, 3]))
            {
                return false;
            }
        }
        return true;
    }
}
