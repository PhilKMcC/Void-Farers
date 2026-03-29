using Unity.VisualScripting;
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
    }
}
