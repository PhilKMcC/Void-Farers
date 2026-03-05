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
    public float maxSpeed;

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

        Vector2 vel = myBody.linearVelocity;
        vel += (MoveAction.ReadValue<Vector2>() * Time.deltaTime * acceleration);

        myBody.linearVelocity = vel;
    }

    public void enterRocket()
    {
        Debug.Log("entered rocket");
        InputSystem.actions.FindActionMap("Player").Disable();
        InputSystem.actions.FindActionMap("Ship").Enable();
        player.SetActive(false);
    }

    public void leaveRocket()
    {
        Debug.Log("left rocket");
        InputSystem.actions.FindActionMap("Player").Enable();
        InputSystem.actions.FindActionMap("Ship").Disable();
        player.SetActive(true);
        player.transform.position = gameObject.transform.position;
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
