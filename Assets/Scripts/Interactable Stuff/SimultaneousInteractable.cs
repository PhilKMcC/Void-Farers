using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class SimultaneousInteractable : Abstr_Interactable
{
    /*
      * Class Explanation:
      * this is used when you want a sequnce of interactions to happen at the same time.
      * technically, this can already be achieved with an interactor calling several, but there are use cases.
      * for example, it also works with the sequential interactor, so you can have multiple at the same time somewhere in the middle
      * note of caution, dialogue only allows one dialogue at a time, so if more than one is included the rest will be lost.
      * priority is for when it should return control to the sequence it belongs to - when all of its things have finished, when the first (index0) has, when any have, or don't wait for any.
      */
    public Abstr_Interactable[] interactables;

    public enum Priority
    {
        WAITFORALL,
        WAITFORFIRST,
        WAITFORANY,
        WAITFORNONE,
    }
    public Priority mode = Priority.WAITFORALL;
    public override void Interact()
    {
        //in order, call each interaction after the previous finishes.
        StartCoroutine(callInteractions());

    }

    IEnumerator callInteractions()
    {
        midAction = true;
        foreach (Abstr_Interactable interactable in interactables)
        {
           interactable.Interact();
            
        }
        yield return null;
        if (mode == Priority.WAITFORNONE)
        {
            //finish
            midAction = false;
        }
        else if (mode == Priority.WAITFORANY)
        {
            while (midAction)
            {
                //wait for any to be done
                foreach (Abstr_Interactable interactable in interactables)
                {
                    if (interactable.midAction == false)
                    {
                        midAction = false;
                    }
                }
                yield return null;
            }
        }
        else if (mode == Priority.WAITFORFIRST)
        {
            while (midAction)
            {
                if (interactables[0].midAction == false)
                {
                    midAction = false;
                }
                yield return null;
            }
        }
        else if (mode == Priority.WAITFORALL)
        {
            while (midAction)
            {
                bool done = true;
                foreach (Abstr_Interactable interactable in interactables)
                {
                    if (interactable.midAction == false)
                    {
                        done = false;
                    }
                }
                midAction = done;
                yield return null;
            }
        }
        yield return null;
    }
}
