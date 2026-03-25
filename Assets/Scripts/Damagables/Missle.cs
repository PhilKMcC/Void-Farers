using UnityEngine;

public class Missle : Abstr_Damagable
{
    /*
     * missles fired by rocket
     */
    public float speed = 30f;
    public Rigidbody2D myBody;
    private float timer = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }
        myBody.linearVelocity = transform.up * speed;
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
