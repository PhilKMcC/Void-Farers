using UnityEngine;
using UnityEngine.InputSystem;

public class testPlayerScript : MonoBehaviour
{
    /*
     * Class Explanation:
     * used to test the new input system. not the actual player script. advised not to use this please.
     */

    public InputActionAsset actions;

    public InputAction MoveAction;
    public InputAction JumpAction;

    public Rigidbody2D myBody;

    public float MoveSpeed = 2;
    public float JumpIntensity = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction = InputSystem.actions.FindAction("Player/Move");
        JumpAction = InputSystem.actions.FindAction("Player/Jump");


    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();
        //Debug.Log(move);
        myBody.linearVelocityX = (move.x * MoveSpeed);
        if (JumpAction.WasPressedThisFrame())
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
