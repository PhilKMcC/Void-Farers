using NUnit.Framework.Internal;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class rockHead : MonoBehaviour
{

    public Rigidbody2D myBody;
    public GameObject Rocket;

    public float Speed = 5f;
    public float detectionVal = 12f;
    public float bufferLaunch = 3f;

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
        Debug.Log("Start Head");
       
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case 6:
                die();
                break;
            default:
                break;
        }


    }

    void die()
    {
        myBody.bodyType = RigidbodyType2D.Dynamic;
        myBody.gravityScale = 1;
    }

}
