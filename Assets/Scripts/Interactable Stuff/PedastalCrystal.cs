using UnityEngine;

public class PedastalCrystal : InvertCollectable, I_Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        if (myRenderer == null)
        {
            myRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        myRenderer.enabled = false;
        Debug.Log("disabled rederer");
    }

    

    public void Interact()
    {
        CheckInvCollected();
        if (Collectable.Collectables.ContainsKey(ID))
        { 
            if (Collectable.Collectables[ID].collected)
            {
                PlaceCrystal();
            }
        }
    }

    public void PlaceCrystal()
    {
        myRenderer.enabled = true;
        //check if all placed
    }

}
