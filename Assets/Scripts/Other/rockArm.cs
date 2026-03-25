using NUnit.Framework.Internal;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class rockArm : MonoBehaviour
{

    public Rigidbody2D myBody;
    public GameObject Rocket;

    public float Speed = 15f;
    public float detectionVal = 50f;
    public float bufferLaunch = 3f;
    public bool mirror = false;

    private float distanceVector;
    private float distanceVectorTwo;
    private float distanceVectorThree;
    private Vector3 endPos;
    private Vector3 startPos;
    private bool moving = false;
    private bool launchable;

    private Vector3 relEndPos;
    private Vector3 tossEndPos;
    private Vector3 beamEndPos;

    //Need to be accessed by body script
    public float state;
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
        Debug.Log("Start Arm");
        moving = false;
        launchable = true;
        initializeEndPositions();
       
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

        switch(state)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                if (distanceVector < detectionVal)
                {
                    moving = true;
                    Launch(endPos);
                }
                break;
            case 3:
                moveOut(relEndPos);
                break;
            case 4:
                Launch(tossEndPos);
                break;
            case 5:
                moveOut(beamEndPos);
                break;
            case 6:
                die();
                break;
            case 7:
                Retract(Speed);
                break;
            default:
                break;

        }

    
    }

   void moveOut(Vector3 dest)
   {
        distanceVectorTwo = Vector2.Distance(transform.position, dest);

        if(!distanceVectorTwo.Equals(0))
        {
            transform.position = Vector2.MoveTowards(this.transform.position, dest, Speed * Time.deltaTime);
        }
   }

   void die()
    {
        myBody.bodyType = RigidbodyType2D.Dynamic;
        myBody.gravityScale = 1;
        //Explosion Animation?
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
            Retract(Speed/2);
        }
      
    }

    void Retract(float retSpeed)
    {
        distanceVectorThree = Vector2.Distance(transform.position, startPos);
        if (!distanceVectorThree.Equals(0) && !launchable)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, startPos, retSpeed * Time.deltaTime);
        }
        else
        {
            moving = false;
            launchable = true;
        }

    }

    void initializeEndPositions()
    {
        relEndPos = startPos;
        tossEndPos = startPos;
        beamEndPos = startPos;
        relEndPos.y += 30;
        tossEndPos.y += 15;
        beamEndPos.y += 50;
        if (mirror)
        {
            relEndPos.x -= 20;
            tossEndPos.x += 20;
        }
        else
        {
            relEndPos.x += 20;
            tossEndPos.x -= 20;
        }
    }
}
