using System;
using UnityEngine;

public class kamikazeEnemy : Abstr_Damagable
{
    public GameObject Rocket;
    public float Speed = 10f;
    public float detectionVal = 20f;
    private float distanceVector;
    private bool undetected;

    public Rigidbody2D myBody;

    public float rotSpeed = 60f;

    public Animator myAnimator;

    public bool boss = false;

    private Vector3 initialDetect;

    public GameObject explosion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myTag = gameObject.tag;
        initializeSets();
        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }
        if (Rocket == null) { Rocket = GameObject.FindGameObjectWithTag("Ship"); }
        if (myAnimator == null) { myAnimator = gameObject.GetComponent<Animator>(); }
        undetected = true; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vel = myBody.linearVelocity;

        distanceVector = Vector2.Distance(transform.position, Rocket.transform.position);
        Vector2 direction = transform.position - Rocket.transform.position;
        if(distanceVector < detectionVal) {
            if(undetected)
            {
                detectionVal += 15;
                undetected = false;
                myAnimator.SetBool("Attacking", true);
                initialDetect = Rocket.transform.position;
            }
            //if (!boss)
            //{
                transform.position = Vector2.MoveTowards(this.transform.position, Rocket.transform.position, Speed * Time.deltaTime);
            //}
            //else
            //{
            //    transform.position = Vector2.MoveTowards(this.transform.position, initialDetect, Speed * Time.deltaTime);
            //}
            float rotation = Vector2.SignedAngle(gameObject.transform.up, direction);
            Mathf.Clamp(rotation, -rotSpeed, rotSpeed);
            myBody.angularVelocity = rotation;
        }
        else
        {
            myAnimator.SetBool("Attacking",false);

        }


    }

    public override void Damage()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);    
        Destroy(gameObject);
        Debug.Log("kaplow!");

    }
}
