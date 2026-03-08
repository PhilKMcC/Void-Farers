using UnityEngine;

public interface I_Interactable
{
    /*
     * Class (interface) Explanation:
     * things inherited from this can be interacted with. 
     * they must also have a copy of the interactable class on them.
     * althogh, i suppose I could just make them add one if they are missing...
     */



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Interact();
}
