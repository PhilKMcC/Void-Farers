using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceInteractor : Interactor
{
    /*
     * Class Explanation:
     * This is to be used on the rocket
     * space as in space bar not as in outer space
     * basically, it makes it interact on space, rather than E.
     */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        InteractAction = InputSystem.actions.FindAction("Player/Jump"); //spacekey

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        interactables = gameObject.GetComponents<I_Interactable>();
    }

    
}
