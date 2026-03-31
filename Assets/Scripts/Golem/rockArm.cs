using NUnit.Framework.Internal;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class rockArm : abstrGolem
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

    public AudioSource punchSound;

    private bool firstLaunch;

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
        if (punchSound == null) { punchSound = gameObject.GetComponent<AudioSource>(); }
        if (Rocket == null) { Rocket = GameObject.FindGameObjectWithTag("Ship"); }
        startPos = gameObject.transform.position;
        Debug.Log("Start Arm");
        moving = false;
        launchable = true;
        firstLaunch = true;
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
        //Debug.Log(state); --> 1 and 7
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
                moveOut(tossEndPos);
                break;
            case 5:
                moveOut(beamEndPos);
                break;
            case 6:
                die();
                break;
            case 7:
                firstLaunch = true;
                launchable = false;
                Retract(Speed);
                break;
            default:
                break;

        }

    
    }

   void moveOut(Vector3 dest)
   {
        distanceVectorTwo = Vector2.Distance(transform.position, dest);

        if (!distanceVectorTwo.Equals(0))
        {
            transform.position = Vector2.MoveTowards(this.transform.position, dest, Speed * Time.deltaTime);
        }
        else
        {
            wait = false;
        }
   }

   void die()
    {
        if (transform.position.y > -222)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, startPos, Speed*5*Time.deltaTime);
        }
        else {
            myBody.bodyType = RigidbodyType2D.Dynamic;
            myBody.gravityScale = 1;
        }
        //Explosion Animation?
    }


    void Launch(Vector3 dest)
    {
        if(firstLaunch)
        {
            punchSound.Play();
            firstLaunch = false;
        }
        //Debug.Log("Launchable: " + launchable);
        if(launchable) {
            distanceVectorTwo = Vector2.Distance(transform.position, dest);
        }

        if(!distanceVectorTwo.Equals(0) && launchable) {
            if ((mirror && (startPos.x-10) <= dest.x) || (!mirror && (startPos.x+10) >= dest.x))
            {
                transform.position = Vector2.MoveTowards(this.transform.position, dest, Speed * Time.deltaTime);
            }
            else {
                moving = false;
            }
        }
        else
        {
            //Debug.Log("Reached");
            launchable = false;
            Retract(Speed/2);
            //state = 7;
        }
      
    }

    void Retract(float retSpeed)
    {
        distanceVectorThree = Vector2.Distance(transform.position, startPos);
        //Debug.Log(distanceVectorThree);
        if (!distanceVectorThree.Equals(0) && !launchable)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, startPos, retSpeed * Time.deltaTime);
        }
        else
        {
            moving = false;
            launchable = true;
            state = 1;
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
