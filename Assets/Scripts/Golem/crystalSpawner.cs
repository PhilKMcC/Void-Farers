using System;
using System.Collections;
using UnityEngine;

public class crystalSpawner : abstrGolem
{
    public GameObject projectilePrefab;
    public GameObject projectilePrefabMirr;
    //public float state;
    public int spawnAmount = 25;
    private int counter;

    public AudioSource ding;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (ding == null) { ding = gameObject.GetComponent<AudioSource>(); }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(state); -> always 1
        switch(state)
        {
            case 4:
                Spawn();
                break;
            default:
                break;
        }
    }

    void Spawn()
    {
        if(!wait)
        {
            for(counter = 0; counter < spawnAmount; counter++)
            {
                StartCoroutine(Spawn(1/100));
                StartCoroutine(SpawnMirr(1 / 100));
            }
            if (counter >= spawnAmount)
            {
                Debug.Log("State 7 crystals");
                state = 7;
                wait = true;
            }
        }
    }
    private IEnumerator Spawn(float timer)
    {
        yield return new WaitForSeconds(timer);
        ding.Play();
        GameObject spawned = Instantiate(projectilePrefab, new Vector3(transform.position.x + UnityEngine.Random.Range(-5, 5), transform.position.y + UnityEngine.Random.Range(-5, 5), 0), Quaternion.identity);
    }

    private IEnumerator SpawnMirr(float timer)
    {
        yield return new WaitForSeconds(timer);
        GameObject spawned = Instantiate(projectilePrefabMirr, new Vector3(transform.position.x + 42 + UnityEngine.Random.Range(-5, 5), transform.position.y + UnityEngine.Random.Range(-5, 5), 0), Quaternion.identity);
    }

}
