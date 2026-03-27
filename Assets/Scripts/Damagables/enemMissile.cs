using UnityEngine;

public class enemMissle : Abstr_Damagable
{
    /*
     * missles fired by enemies
     */
    public float speed = -15f;
    public Rigidbody2D myBody;
    private float timer = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }
        myBody.SetRotation(-90);
        myBody.linearVelocity = transform.up * speed;
        //timer /= 6;
    }


     void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Damage();
        }
    }

    public override void Damage()
    {
        Destroy(gameObject);
    }

    

    
}
