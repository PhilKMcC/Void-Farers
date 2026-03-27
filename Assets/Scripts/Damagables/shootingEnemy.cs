using UnityEngine;

public class shootingEnemy : Abstr_Damagable
{
    public GameObject Rocket;
    public float Speed = 5f;
    public float detectionVal = 30f;
    private float distanceVector;
    private bool undetected;

    public Rigidbody2D myBody;

    public float rotSpeed = 45f;

    public Animator myAnimator;

    public GameObject enemmisslePrefab;
    public float upOffset = -2;
    public float sideOffset = 0;
    private float frameCounter = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myTag = gameObject.tag;
        initializeSets();
        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }
        if (Rocket == null) { Rocket = GameObject.FindGameObjectWithTag("Ship"); }
        if (myAnimator == null) { myAnimator = gameObject.GetComponent<Animator>(); }
        undetected = true; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vel = myBody.linearVelocity;

        distanceVector = Vector2.Distance(transform.position, Rocket.transform.position);
        Vector2 direction = transform.position - Rocket.transform.position;
        if(distanceVector+5 < detectionVal) {
            if(undetected)
            {
                detectionVal += 15;
                undetected = false;
                myAnimator.SetBool("Attacking", true);
            }

            transform.position = Vector2.MoveTowards(this.transform.position, Rocket.transform.position, Speed * Time.deltaTime);
            Shoot();

            float rotation = Vector2.SignedAngle(gameObject.transform.up, direction);
            Mathf.Clamp(rotation, -rotSpeed, rotSpeed);
            myBody.angularVelocity = rotation;
        }
        else
        {
            myAnimator.SetBool("Attacking",false);

        }
        if(frameCounter < 60)
        {
            frameCounter++;
        }
        else
        {
            frameCounter = 0;
        }


    }

    public void Shoot()
    {
        if (frameCounter == 1)
        {
            Debug.Log("Shot");
            Vector3 Offset = transform.up * upOffset + transform.right * sideOffset;
            Instantiate(enemmisslePrefab, transform.position + Offset, transform.rotation);
        }
    }
    public override void Damage()
    {
        Destroy(gameObject);
        Debug.Log("kaplow!");

    }
}
