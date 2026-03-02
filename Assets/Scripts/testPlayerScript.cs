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

    public Rigidbody2D myBody;

    public float MoveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction = InputSystem.actions.FindAction("Player/Move");

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = MoveAction.ReadValue<Vector2>();
        //Debug.Log(move);
        myBody.linearVelocity = (move * MoveSpeed);
    }

    private void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
    }

    private void Awake()
    {
    }
    private void OnDisable()
    {
        actions.FindActionMap("Player").Disable();
    }
    
}
