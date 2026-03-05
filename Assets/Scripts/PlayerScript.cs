using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    /*
     * Class Explanation:
     * The script to be attached to the player. handles movement, etc. 
     */

    public InputActionAsset actions;

    public InputAction MoveAction;
    public InputAction JumpAction;

    public Rigidbody2D myBody;
    public ContactFilter2D floorDetector;

    public float MoveSpeed = 2;
    public float JumpIntensity = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction = InputSystem.actions.FindAction("Player/Move");
        JumpAction = InputSystem.actions.FindAction("Player/Jump");

        floorDetector = new ContactFilter2D();
        floorDetector.SetNormalAngle(45, 135); //upwards normals
        //floorDetector.NoFilter();

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
    }

    private void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
    }

    
    private void OnDisable()
    {
        actions.FindActionMap("Player").Disable();
    }
}
