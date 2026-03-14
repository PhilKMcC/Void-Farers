using System;
using UnityEngine;

public abstract class Abstr_Interactable : MonoBehaviour, I_Interactable
{
    /*
     * Class Explanation:
     * allows interactables to be treated as class rather than interface
     */

    [NonSerialized]
    public bool midAction = false;
    public abstract void Interact();


}
