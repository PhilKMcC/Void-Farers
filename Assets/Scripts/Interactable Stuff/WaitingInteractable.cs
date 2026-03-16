using System.Collections;
using UnityEngine;

public class WaitingInteractable : Abstr_Interactable
{
    /*
     * Class Explanation:
     * waits the set amount of time after it is called
     */
    public float waitTime;
    public override void Interact()
    {
        midAction = true;
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(waitTime);
        midAction = false;
    }
    
}
