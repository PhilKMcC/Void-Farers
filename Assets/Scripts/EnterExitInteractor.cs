using UnityEngine;

public class EnterExitInteractor : TriggerInteractor
{
    /*
     * Class Explanation:
     * same as the trigger interactor, except it works on the exit as well.
     * i made it for the sake of doors, but probably has other uses.
     */



    public void OnTriggerExit2D(Collider2D collision)
    {
        //check it is player or ship
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ship"))
        {
            Interact();
        }
    }
}
