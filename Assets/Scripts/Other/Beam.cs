using System;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public float xdirection = 0f;
    public float ydirection = 0f;

    public BoxCollider2D myCollider;
    public Rigidbody2D myBody;
    public SpriteRenderer mySprite;

    private RaycastHit2D endPoint1;
    private RaycastHit2D endPoint2;

    private Boolean extended;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }
        if (myCollider == null) { myCollider = gameObject.GetComponent<BoxCollider2D>(); }
        if (mySprite == null) { mySprite = gameObject.GetComponent<SpriteRenderer>(); }
        endPoint1 = Physics2D.Raycast(transform.position, transform.TransformDirection(xdirection, ydirection, 0));
        endPoint2 = Physics2D.Raycast(transform.position, transform.TransformDirection(-xdirection, -ydirection, 0));
        extended = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!extended)
        {
            Extend();
        }
        else
        {
            Rotate();
        }
    }

    void Rotate()
    {

    }

    void Extend()
    {
        /*if()
        {

        }
        else
        {
            extended = true;
        }*/
    }
}
