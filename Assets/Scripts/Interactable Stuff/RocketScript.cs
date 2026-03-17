using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

public class RocketScript : MonoBehaviour, I_Interactable, I_Initializable
{
    /*
     * Class Explanation:
     * This is used by the rocket ship. 
     * It handles rocket movement, entering and leaving the rocket
     * it does not handle updating the style of the rocket. Something else will do that.
     */

    public GameObject player;
    public Rigidbody2D myBody;

    public InputAction MoveAction;
    public InputAction LeaveAction;
    public InputAction AttackAction;

    public float acceleration;
    public float friction = 1f;
    public float maxSpeed;
    public float rotSpeed = 30f;
    public float landingDist = 2;

    public float camDist = 15;

    private bool isLanding = false;
    private ContactFilter2D TilesFilter;

    public Vector2 cameraOffset = Vector2.zero;

    public Animator myAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }

        MoveAction = InputSystem.actions.FindAction("Ship/Move");
        LeaveAction = InputSystem.actions.FindAction("Ship/Leave");
        AttackAction = InputSystem.actions.FindAction("Ship/Attack");

        I_Initializable.initials.Add(this);

        TilesFilter = new ContactFilter2D();
        TilesFilter.SetLayerMask(0b001000000000);


    }

    // Update is called once per frame
    void Update()
    {
        if (LeaveAction.WasPressedThisFrame())
        {
            leaveRocket();
        }
        else
        {

            //apply accelleration
            Vector2 vel = myBody.linearVelocity;
            vel += (MoveAction.ReadValue<Vector2>() * Time.deltaTime * acceleration);
            if (vel.magnitude > maxSpeed)
            {
                vel = vel.normalized * maxSpeed;
            }

            //apply friction
            vel -= vel.normalized * Mathf.Clamp(Time.deltaTime * friction, 0, vel.magnitude);
            //Mathf.Max(vel.magnitude, 1)

            myBody.linearVelocity = vel;

            //apply rotation
            float rotation = Vector2.SignedAngle(gameObject.transform.up, MoveAction.ReadValue<Vector2>());
            Mathf.Clamp(rotation, -rotSpeed, rotSpeed);
            myBody.angularVelocity = rotation;

            if (MoveAction.ReadValue<Vector2>().magnitude > 0.1f)
            {
                myAnimator.SetBool("firing", true);
            }
            else
            {
                myAnimator.SetBool("firing", false);
            }
        }
    }

    public void enterRocket()
    {
        Debug.Log("entered rocket");
        InputSystem.actions.FindActionMap("Player").Disable();
        InputSystem.actions.FindActionMap("Ship").Enable();
        CameraControl.changeTarget(gameObject, cameraOffset);
        CameraControl.changeScale(camDist);
        player.transform.position = new Vector3(99999, 99999, 0);//send player very far away
        player.SetActive(false);
        Debug.Log("diabled player");
    }

    public void leaveRocket()
    {
        //actually, 
        //fire a raycast downwards to look for ground. 
        // if ground found, go into langing mode, and then land, then let player out.
        // otherwise don't let player out.
        
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        Physics2D.Raycast(transform.position, Vector2.down, TilesFilter, hits, landingDist);
        if (hits.Count > 0 && !isLanding)
        {

            StartCoroutine(Landing());
        }
        

    }

   public IEnumerator Landing()
    {
        InputSystem.actions.FindActionMap("Ship").Disable();

        isLanding = true;
        //first rotate to be vertical
        myBody.angularVelocity = 0;
        myBody.linearVelocity = Vector2.zero;
        float closeEnough = 1;
        while (Vector2.Angle(gameObject.transform.up, Vector2.up) > closeEnough)
        {
            float rotation = Vector2.SignedAngle(gameObject.transform.up, Vector2.up);
            rotation = (rotation > 0)?(rotSpeed):(-rotSpeed);
            //Mathf.Clamp(rotation, -rotSpeed, rotSpeed);
            myBody.angularVelocity = rotation;
            yield return null;
        }
        myBody.angularVelocity = 0;
        myBody.rotation = 0;

        //then ground yourself.
        List<Collider2D> colliders = new List<Collider2D>();
        while (myBody.GetContacts(TilesFilter, colliders) == 0)
        {
            myBody.linearVelocity = Vector2.down * (maxSpeed / 4);
            yield return null;
        }
        myBody.linearVelocity = Vector2.zero;

        //exit the rocket
        Debug.Log("left rocket");
        InputSystem.actions.FindActionMap("Player").Enable();
        player.SetActive(true);
        player.transform.position = gameObject.transform.position;
        CameraControl.changeTarget(player, player.GetComponent<PlayerScript>().cameraOffset);
        CameraControl.resetScale();
        isLanding = false;
        yield return null;
    }

    public void Interact()
    {
        enterRocket();
    }


    public void init()
    {
        InputSystem.actions.FindActionMap("Ship").Disable();
        transform.rotation = Quaternion.identity;
    }

}
