using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    /*
     * Class Explanation:
     * To be used with some other scripts that inherits from I_Interactable. 
     * Basically, notes the interaction. also controls the distance.
     */

    public InputAction InteractAction;

    public float interactDistance = 3;

    public static GameObject player;

    public I_Interactable[] interactables;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InteractAction = InputSystem.actions.FindAction("Player/Interact");

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        interactables = gameObject.GetComponents<I_Interactable>();

    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (InteractAction.ReadValue<float>() >0 && distance < interactDistance)
        {
            Interact();
        }
    }



    public void Interact()
    {
        Debug.Log("Interacted!");
        foreach (I_Interactable interactable in interactables)
        {
            interactable.Interact();
        }

    }
}
