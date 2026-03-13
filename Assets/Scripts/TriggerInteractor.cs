using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerInteractor : ConditionalInteractor
{
    /*
     * Class Explanation:
     * Conditional Interactor is fine, but sometimes we want things to trigger when a player enters a zone
     * for example, as the rocket first takes off, we'll want some dialogue.
     * This is used basically the same as the Conditional Interactor, in that you attatch various interactables to it.
     * The object this is attached to should have some attached Collider2D with "isTrigger" enabled
     * interact distance and interact object can be 0/null
     */

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        InteractAction = InputSystem.actions.FindAction("Player/Interact");
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        interactables = null;

        if (vars == null)
        {
            vars = new Dictionary<string, int>();
            //loadVars();

        }
        foreach (Interaction interact in interactions)
        {
            if (!vars.ContainsKey(interact.variable))
            {
                vars[interact.variable] = 0;
            }
        }
        Debug.Log("Conditional interaction vars: " + displayVars());
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //check it is player or ship
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ship"))
        {
            Interact();
        }
    }
}
