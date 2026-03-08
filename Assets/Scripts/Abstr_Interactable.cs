using UnityEngine;

public abstract class Abstr_Interactable : MonoBehaviour, I_Interactable
{
    /*
     * Class Explanation:
     * allows interactables to be treated as class rather than interface
     */

    public abstract void Interact();
}
