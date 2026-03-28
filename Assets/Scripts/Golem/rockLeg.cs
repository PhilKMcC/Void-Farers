using NUnit.Framework.Internal;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class rockLeg : MonoBehaviour, abstrGolem
{

    public Rigidbody2D myBody;
    public GameObject Rocket;

    public float Speed = 5f;
    public float detectionVal = 12f;
    public float bufferLaunch = 3f;

    private float distanceVector;
    private float distanceVectorTwo;
    private float distanceVectorThree;
    private Vector3 endPos;
    private Vector3 startPos;
    private bool moving = false;
    private bool launchable;

    //Need to be accessed by body script
    //public float state;
    //0 = Asleep
    //1 = Waiting
    //2 = Launch Arm
    //3 = Release Kamikazes
    //4 = Amethyst Toss
    //5 = Amethyst Beam
    //6 = Dying
    //7 = Resetting


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }
        if (Rocket == null) { Rocket = GameObject.FindGameObjectWithTag("Ship"); }
        startPos = gameObject.transform.position;
        Debug.Log("Start Leg");
        moving = false;
        launchable = true;
       
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("Moving: " + moving);
        if (!moving) {
            //Debug.Log("Checking");
            distanceVector = Vector2.Distance(gameObject.transform.position, Rocket.transform.position);
            endPos = Rocket.transform.position;
        }
        switch (state) {
            case 0:
                break;
            case 6:
                die();
                break;
            default:
                if (distanceVector < detectionVal)
                {
                    moving = true;
                    Launch(endPos);
                }
                break;
        }


    }

    void die()
    {
        myBody.bodyType = RigidbodyType2D.Dynamic;
        myBody.gravityScale = 1;
    }

    void Launch(Vector3 dest)
    {

        //Debug.Log("Launchable: " + launchable);
        if(launchable) {
            distanceVectorTwo = Vector2.Distance(transform.position, dest);
        }

        //Debug.Log(distanceVectorTwo);

        if(!distanceVectorTwo.Equals(0) && launchable) {
            transform.position = Vector2.MoveTowards(this.transform.position, dest, Speed * Time.deltaTime);
        }
        else
        {
            //Debug.Log("Reached");
            launchable = false;
            Retract();
        }
      
    }

    void Retract()
    {
        distanceVectorThree = Vector2.Distance(transform.position, startPos);
        if (!distanceVectorThree.Equals(0) && !launchable)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, startPos, Speed / 2 * Time.deltaTime);
        }
        else
        {
            moving = false;
            launchable = true;
        }

    }
}
