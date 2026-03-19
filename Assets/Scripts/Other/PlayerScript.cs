using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerScript : Abstr_Damagable
{
    /*
     * Class Explanation:
     * The script to be attached to the player. handles movement, etc. 
     */

    public InputActionAsset actions;

    public InputAction MoveAction;
    public InputAction JumpAction;

    public Rigidbody2D myBody;
    public Collider2D triggerer;
    public ContactFilter2D floorDetector;

    public float MoveSpeed = 2;
    public float JumpIntensity = 2;
    public float gravity = 1.0f;

    public Vector3 cameraOffset = Vector3.zero;

    public Animator myAnimator;

    public bool alive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction = InputSystem.actions.FindAction("Player/Move");
        JumpAction = InputSystem.actions.FindAction("Player/Jump");

        floorDetector = new ContactFilter2D();
        floorDetector.SetNormalAngle(45, 135); //upwards normals
        //floorDetector.NoFilter();


        //CameraControl.changeTarget(gameObject, cameraOffset);




    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();
        //Debug.Log(move);
        myBody.linearVelocityX = (move.x * MoveSpeed);
        if (JumpAction.WasPressedThisFrame() && myBody.IsTouching(floorDetector))
        {
            myBody.linearVelocityY = JumpIntensity;
        }
        myBody.linearVelocityY -= gravity * Time.deltaTime;

        myAnimator.SetFloat("movement", myBody.linearVelocityX);
    }

    private void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
    }

    /*
    private void OnDisable()
    {
        actions.FindActionMap("Player").Disable();
    }
    */

    public Vector3 getCameraPlayerOffset()
    {
        return cameraOffset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            myBody.bodyType = RigidbodyType2D.Kinematic;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //exited all
        List<Collider2D> res = new List<Collider2D>();
        triggerer.Overlap(res);
        if ( res.Count == 0)
        {
            myBody.bodyType = RigidbodyType2D.Dynamic;

        }

    }

    public override void Damage()
    {
        alive = false;
        actions.Disable();
    }

}
