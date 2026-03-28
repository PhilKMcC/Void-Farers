using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public GameObject kamikazePrefab;
    public GameObject shootingPrefab;

    public float asteroidFrequency = 3;
    public float kamikazeFrequency = 5;
    public float shootingFrequency = 7;

    private HashSet<float[]> exclusionZones;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instantiateExclusionZones();
        StartCoroutine(spawn(asteroidFrequency, asteroidPrefab));
        StartCoroutine(spawn(kamikazeFrequency, kamikazePrefab));
        StartCoroutine(spawn(shootingFrequency, shootingPrefab));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawn(float timer, GameObject toSpawn)
    {
        //Set xVal and yVal to randomRange outside of camera bounds
        float xVal = 0;
        float yVal = 0;
        //Search exclusion zones to see if both xval and yval are not within the bounds of each part of the set
        yield return new WaitForSeconds(timer);
        GameObject spawned = Instantiate(toSpawn, new Vector3(xVal, yVal, 0), Quaternion.identity);
        StartCoroutine(spawn(timer, toSpawn));

    }

    void instantiateExclusionZones()
    {
        //Xmin, Xmax, Ymin, Ymax
        exclusionZones = new HashSet<float[]>();
        exclusionZones.Add([-78, -25, 70, -3]);
        exclusionZones.Add([-265, -234, 36, 67]);
        exclusionZones.Add([-475, -383, 274, 315]);
        exclusionZones.Add([-494, -422, -257, -195]);
        exclusionZones.Add([-170, -43, -285, -214]);
        exclusionZones.Add([140, 239, -80, -47]);
        exclusionZones.Add([97, 109, 123, 134]);
        exclusionZones.Add([170, 247, 180, 257]);
    }
}
