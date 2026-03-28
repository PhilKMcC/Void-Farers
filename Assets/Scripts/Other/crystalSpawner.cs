using System;
using System.Collections;
using UnityEngine;

public class crystalSpawner : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float state;
    public int spawnAmount = 25;
    public Boolean wait;
    private int counter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wait = false;
    }

    // Update is called once per frame
    void Update()
    {
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
            }
        }
    }
    private IEnumerator Spawn(float timer)
    {
        yield return new WaitForSeconds(timer);
        GameObject spawned = Instantiate(projectilePrefab, new Vector3(transform.position.x + UnityEngine.Random.Range(-5, 5), transform.position.y + UnityEngine.Random.Range(-5, 5), 0), Quaternion.identity);
    }

}
