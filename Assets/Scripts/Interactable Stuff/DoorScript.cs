using UnityEngine;

public class DoorScript : Abstr_Interactable
{
    /*
     * Class Explanation:
     * This is to be used on doors and other on/off things.
     * it toggles between an active (open) and passive (closed) state
     * it is to be used with any interactor
     * best with enter/exit interactor
     */
    public SpriteRenderer myRenderer;
    public Collider2D myCollider;
    public Sprite closedSprite;
    public Sprite openSprite;
    public bool open = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (myRenderer == null)
        {
            myRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        myRenderer.sprite = closedSprite;
        if (myCollider == null)
        {
            myCollider = gameObject.GetComponent<Collider2D>();
        }
    }

    


    public override void Interact()
    {
        open = !open;
        if (open)
        {
            myRenderer.sprite = openSprite;
            myCollider.enabled = false;
        }
        else
        {
            myRenderer.sprite = closedSprite;
            myCollider.enabled = true;
        }
    }
}
