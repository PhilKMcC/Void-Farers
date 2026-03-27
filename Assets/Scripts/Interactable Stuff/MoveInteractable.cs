using System.Collections;
using UnityEngine;

public class MoveInteractable : Abstr_Interactable
{
    /*
     * used for moving NPCs around. works relative to current position
     * they move at a rate of movespeed an amount of move away.
     */
    public Vector3 move;
    public float moveSpeed = 3;
    public GameObject objectToMove; //defaults to self. if put on something else, such as a child would need to specify


    public override void Interact()
    {
        midAction = true;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        if (objectToMove == null)
        {
            objectToMove = gameObject;
        }

        Vector3 start = objectToMove.transform.position;
        float dist = move.magnitude;

        while (dist > (objectToMove.transform.position - start).magnitude){
            objectToMove.transform.position += move.normalized * moveSpeed * Time.deltaTime;
            yield return null;
        }



        midAction= false;
    }
}
