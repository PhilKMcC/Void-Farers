using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PetScript : MonoBehaviour
{
    public float hOffset = 0.75f;
    public float wOffset = 0.5f;
    public SpriteRenderer myrenderer;
    public Sprite basic;
    public Sprite grabbed;
    public bool carried;
    public Vector3 offset;
    public Rigidbody2D myBody;
    public float gravity = 0.5f;
    public float bounce = 0.3f;
    public float friction = 0.3f;
    public Vector3 previousPos;
    public Vector3 currPos;
    public float updateTimer = 0;
    public float timePerUpdate = 0.01f;
    public float moveTolerance = 0.0005f;

    public PlayerScript playerscript;
    public float blastZone = 15;

    private InputAction mouseClick;
    private InputAction mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        mouseClick = InputSystem.actions.FindAction("Secrets/Click");
        mousePosition = InputSystem.actions.FindAction("Secrets/Touch");
        myBody = GetComponent<Rigidbody2D>();
        myrenderer = GetComponent<SpriteRenderer>();
        playerscript = GetComponent<PlayerScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(mousePosition.ReadValue<Vector2>());
        //check release
        if (mouseClick.ReadValue<float>()  < 0.1 && carried)
        {
            myrenderer.sprite = basic;
            carried = false;
            myBody.linearVelocity = (currPos - previousPos) / timePerUpdate;
        }

        if (!carried)
        {
            //check stop
            if (myBody.linearVelocity.magnitude < moveTolerance && transform.position.y == hOffset)
            {
                myBody.linearVelocity = Vector3.zero;
            }
            else
            {
                //apply friction
                myBody.linearVelocity -= myBody.linearVelocity * friction * Time.deltaTime;

                if (transform.position.y < 0 + hOffset) //below/hit ground
                {
                    transform.position = new Vector3(transform.position.x, hOffset, transform.position.z);
                    myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, -myBody.linearVelocity.y * bounce);
                    //adjust for landed
                    if (Mathf.Abs(myBody.linearVelocity.y) <= moveTolerance)
                    {
                        myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, 0);
                    }
                }

                float edges = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.scaledPixelWidth, 0, 0)).x - wOffset;
                if (transform.position.x < -edges) // bounce to the left
                {
                    transform.position = new Vector3(-edges, transform.position.y, transform.position.z);
                    myBody.linearVelocity = new Vector2(-myBody.linearVelocity.x * bounce, myBody.linearVelocity.y);
                }

                if (transform.position.x > edges) // bounce to the right
                {
                    transform.position = new Vector3(edges, transform.position.y, transform.position.z);
                    myBody.linearVelocity = new Vector2(-myBody.linearVelocity.x * bounce, myBody.linearVelocity.y);
                }

                if (transform.position.y > 0 + hOffset) // fall in air
                {
                    myBody.linearVelocity += new Vector2(0, -gravity) * Time.deltaTime;
                }
                //Debug.Log(myBody.linearVelocity.magnitude);
            }
        }
        if (carried && mouseClick.ReadValue<float>() > 0.1)
        {
            MyOnMouseDrag();
        }
        if (!carried && mouseClick.ReadValue<float>() > 0.1)
        {
            MyOnMouseDown();
        }
        if (playerscript.MoveAction.ReadValue<Vector2>().magnitude > 0.1)
        {
            playerscript.enabled = true;
        }

        if (gameObject.transform.position.y > blastZone)
        {
            //return to main scene
            Debug.Log("return to main scene");
        }
    }

    private void MyOnMouseDown()
    {
        if (myBody.OverlapPoint(Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>())))
        {
            Debug.Log("Mouse clicked");
            offset = transform.position - Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>());
            Debug.Log("hit");
            myrenderer.sprite = grabbed;
            carried = true;
            myBody.linearVelocity = Vector2.zero;
            /*
            currPos = transform.position;
            previousPos = transform.position;
            */
            playerscript.enabled = false;
        }
    }

    private void MyOnMouseDrag()
    {
        Debug.Log("Mouse dragged");
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>()) + offset;

        updateTime();

    }

    private void updateTime()
    {
        updateTimer += Time.deltaTime;
        if (updateTimer >= timePerUpdate)
        {
            updateTimer = 0;
            previousPos = currPos;
            currPos = transform.position;
        }
    }



}
