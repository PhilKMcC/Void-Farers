using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class kamikazeSpawn : abstrGolem
{
    public GameObject kamikazePrefab;

    private float timer;
    private bool spawned;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 1;
        spawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 3:
                Spawn();
                break;
            case 7:
                timer = 1;
                spawned = false;
                break;
            default:
                break;
        }
    }
    void Spawn()
    {
        if (!wait)
        {
            Debug.Log(wait);
            if (!spawned)
            {
                StartCoroutine(Spawn(1 / 100, 7.5f, 0));
                StartCoroutine(Spawn(1 / 100, -7.5f, 0));
                StartCoroutine(Spawn(1 / 100, 0, 7.5f));
                StartCoroutine(Spawn(1 / 100, 0, -7.5f));
                spawned = true;
            }
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Debug.Log("Set to State 7");
                state = 7;
                wait = true;
            }
        }
    }
    private IEnumerator Spawn(float timer, float xmod, float ymod)
    {
        yield return new WaitForSeconds(timer);
        GameObject spawned = Instantiate(kamikazePrefab, new Vector3(transform.position.x + xmod, transform.position.y + ymod, 0), Quaternion.identity);
    }
}
