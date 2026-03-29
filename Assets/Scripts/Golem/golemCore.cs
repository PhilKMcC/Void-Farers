using UnityEngine;

public class golemCore : abstrGolem
{
    public Rigidbody2D myBody;
    public Sprite closed;
    public Sprite open;
    public SpriteRenderer mySprite;
    public Collider2D fullCollider;
    public float timer = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }
        if (mySprite == null) { mySprite = gameObject.GetComponent<SpriteRenderer>(); }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 3:
                setOpen();
                break;
            case 5:
                setOpen();
                break;
            case 6:
                die();
                break;
            case 7:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    setClosed();
                    timer = 1;
                }
                break;
            default:
                break;

        }
    }
    
    void setClosed()
    {
        fullCollider.enabled = true;
        mySprite.sprite = closed;
        mySprite.sortingOrder = -3;
    }
    void setOpen()
    {
        fullCollider.enabled = false;
        mySprite.sprite = open;
        mySprite.sortingOrder = -1;
    }
    void die()
    {
        myBody.bodyType = RigidbodyType2D.Dynamic;
        myBody.gravityScale = 1;
    }
}
