using UnityEngine;

public class golemProjBlocker : abstrGolem
{
    public Collider2D myCollider;
    public Rigidbody2D myBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }
        if (myCollider == null) { myCollider = gameObject.GetComponent<Collider2D>(); }
        myBody.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 6:
                die();
                break;
            default:
                break;
        }
    }

    void die()
    {
        myCollider.enabled = false;
        myBody.simulated = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //check it is player or ship
        if (collision.gameObject.CompareTag("Friendly"))
        {
            myBody.simulated = true;
        }
        else
        {
            myBody.simulated = false;
        }
    }
}
