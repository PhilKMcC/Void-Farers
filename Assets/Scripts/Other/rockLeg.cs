using NUnit.Framework.Internal;
using UnityEngine;

public class rockLeg : MonoBehaviour
{

    public Rigidbody2D myBody;
    public GameObject Rocket;
    public float Speed = 15f;
    public float detectionVal = 10f;
    private float distanceVector;
    private Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }
        if (Rocket == null) { Rocket = GameObject.FindGameObjectWithTag("Ship"); }
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distanceVector = Vector2.Distance(transform.position, Rocket.transform.position);

        if (distanceVector < detectionVal)
        {
            Launch(Rocket.transform.position);
        }


    }

    void Launch(Vector3 dest)
    {
        while (transform.position != dest) 
        {
            transform.position = Vector2.MoveTowards(this.transform.position, dest, Speed * Time.deltaTime);
        }
        Retract();
    }

    void Retract()
    {
        while (transform.position != startPos)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, startPos, Speed/2 * Time.deltaTime);
        }
    }
}
