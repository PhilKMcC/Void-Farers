using System.Collections.Generic;
using UnityEngine;

public class InvertCollectable : MonoBehaviour
{
    /*
     * This is not a collectable
     * It is something that only appears when its corresponding (same ID) collectable is collected.
     * mostly, only works for simple collectables. paint cans have two sprites so will need something better. 
     * for convenience, it takes the appearance of the thing it corresponds with.
     * This itself doesn't do anything, it just looks pretty.
     */

    public int ID;
    public SpriteRenderer myRenderer;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        //copySprite();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void copySprite()
    {
        if (myRenderer == null)
        {
            myRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        
            if (myRenderer.sprite == null)
            {
                myRenderer.sprite = Collectable.Collectables[ID].gameObject.GetComponent<SpriteRenderer>().sprite;
            }
            if (myRenderer.color == Color.white)
            {
                myRenderer.color = Collectable.Collectables[ID].gameObject.GetComponent<SpriteRenderer>().color;
            }
        
    }

    public void CheckInvCollected()
    {
        if (Collectable.Collectables.ContainsKey(ID))
        {
            if (Collectable.Collectables[ID].collected)
            {
                copySprite();
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

    }
}
