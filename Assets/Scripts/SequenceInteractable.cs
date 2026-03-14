using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SequenceInteractable : Abstr_Interactable
{
    /*
     * Class Explanation:
     * this is used when you want a sequnce of interactions to happen right after each other.
     * for example, a npc has dialogue, then walks somewhere, then an elevator is called, then it walks back, then more dialogue.
     * goes in array order
     */
    public Abstr_Interactable[] interactables;
    public bool FreezePlayer = true;

    public override void Interact()
    {
        //in order, call each interaction after the previous finishes.
        StartCoroutine(callInteractions());
        
    }

    IEnumerator callInteractions()
    {
        foreach (Abstr_Interactable interactable in interactables)
        {
            if (FreezePlayer)
            {
                InputSystem.actions.FindActionMap("Player").Disable();
            }
            interactable.Interact();
            //wait until action concluded.
            //unfortunately this does mean that for any multi-frame interaction, i need to set midaction to true, run the coroutine, then set it to false. uhg.
            while (interactable.midAction == true) 
            {
                yield return null;
            }

            InputSystem.actions.FindActionMap("Player").Enable();

            yield return null;
        }
    }
}
