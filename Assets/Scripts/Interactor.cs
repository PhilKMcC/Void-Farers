using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
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

    public GameObject indicatorObject;

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
        if (indicatorObject != null) { indicatorObject.SetActive(false); }
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (InteractAction.WasPressedThisFrame() && distance < interactDistance)
        {
            Interact();
        }
        else if (distance < interactDistance)
        {
            if (indicatorObject != null) { indicatorObject.SetActive(true); }
        }
        //consider having an iteract icon above their head...
    }



    public virtual void Interact()
    {
        Debug.Log("Interacted!");
        foreach (I_Interactable interactable in interactables)
        {
            interactable.Interact();
        }

    }
}
