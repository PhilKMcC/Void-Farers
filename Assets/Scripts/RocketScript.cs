using UnityEngine;
using UnityEngine.InputSystem;

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

    public Vector2 cameraOffset = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }

        MoveAction = InputSystem.actions.FindAction("Ship/Move");
        LeaveAction = InputSystem.actions.FindAction("Ship/Leave");
        AttackAction = InputSystem.actions.FindAction("Ship/Attack");

        I_Initializable.initials.Add(this);

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


        }
    }

    public void enterRocket()
    {
        Debug.Log("entered rocket");
        InputSystem.actions.FindActionMap("Player").Disable();
        InputSystem.actions.FindActionMap("Ship").Enable();
        player.SetActive(false);
        CameraControl.changeTarget(gameObject, cameraOffset);
    }

    public void leaveRocket()
    {
        //actually, 
        //fire a raycast downwards to look for ground. 
        // if ground found, go into langing mode, and then land, then let player out.
        // otherwise don't let player out.


        Debug.Log("left rocket");
        InputSystem.actions.FindActionMap("Player").Enable();
        InputSystem.actions.FindActionMap("Ship").Disable();
        player.SetActive(true);
        player.transform.position = gameObject.transform.position;
        CameraControl.changeTarget(player, player.GetComponent<PlayerScript>().cameraOffset);

    }

    public void Interact()
    {
        enterRocket();
    }


    public void init()
    {
        InputSystem.actions.FindActionMap("Ship").Disable();
    }

}
