using UnityEngine;

public class golemProjBlocker : abstrGolem
{
    public PolygonCollider2D myCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (myCollider == null) { myCollider = gameObject.GetComponent<PolygonCollider2D>(); }
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
