using System;
using UnityEngine;

public class Beam : abstrGolem
{


    public GameObject[] beams;
    public Sprite startingSprite;
    public Sprite damagingSprite;


    public float rotateSpeed = 20;

    public bool damaging;
    public float timer;
    public float startDamaging;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Rotate();
        if (!damaging && timer <= startDamaging)
        {
            MakeDamaging();
        }
        if (timer <= 0)
        {
            EndBeams();
        }
    }

    void Rotate()
    {
        foreach (GameObject beam in beams)
        {
            beam.transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
        }
    }

    void EndBeams()
    {
        foreach (GameObject beam in beams)
        {
            beam.GetComponent<Collider2D>().enabled = false;
            beam.SetActive(false);

        }
        damaging = false;
        wait = true;
        state = 7;
    }

    void StartBeams()
    {
        foreach (GameObject beam in beams)
        {
            beam.SetActive(true);
        }
    }

    void MakeDamaging()
    {
        foreach (GameObject beam in beams)
        {
            beam.GetComponent<Collider2D>().enabled = true;
        }
        damaging = true;

    }

    public void StartBeamAttack(float duration)
    {
        timer = duration;
        startDamaging = 2 * (duration / 3);
        StartBeams();
    }

}
