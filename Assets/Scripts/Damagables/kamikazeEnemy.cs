using UnityEngine;

public class kamikazeEnemy : Abstr_Damagable
{
    public GameObject Rocket;
    public float Speed = 5f;
    private float distanceVector;
    public float detectionVal = 20f;

    public Rigidbody2D myBody;

    public float rotSpeed = 30f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myTag = gameObject.tag;
        initializeSets();
        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }
        if (Rocket == null) { Rocket = GameObject.FindGameObjectWithTag("Ship"); }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vel = myBody.linearVelocity;

        distanceVector = Vector2.Distance(transform.position, Rocket.transform.position);
        Vector2 direction = transform.position - Rocket.transform.position;
        if(distanceVector < detectionVal) {
            transform.position = Vector2.MoveTowards(this.transform.position, Rocket.transform.position, Speed * Time.deltaTime);

            float rotation = Vector2.SignedAngle(gameObject.transform.up, direction);
            Mathf.Clamp(rotation, -rotSpeed, rotSpeed);
            myBody.angularVelocity = rotation;
        }
        

    }

    public override void Damage()
    {
        Destroy(gameObject);
        Debug.Log("kaplow!");

    }
}
