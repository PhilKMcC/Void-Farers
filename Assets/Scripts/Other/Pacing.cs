using UnityEngine;

public class Pacing : MonoBehaviour
{
    /*
     * Class Explanation:
     * This class is to be attached to an npc who should pace around a bit.
     * Ideally, should have some idle animation when stationary
     * And some walking animation when moving. (mirror for the other direction)
     * That will be handled by the animator object
     * the pacing npc waits about the time between then switches between walking and idling, and sets the timeTil next to about time between plus a small random.
     */
    public Rigidbody2D myBody;
    public Animator myAnimator; //currently not in use. once added, remove commenting
    public bool isWalking;
    public bool facing; //false = right
    public float timeTilNext;
    public float aboutTimeBetween = 3;
    public float moveSpeed = 1;
    private Vector3 startPos;
    public float maxDist = 3;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeTilNext = 0;
        if (aboutTimeBetween < 1)
        {
            Debug.Log("caution: time between actions is very low = " + aboutTimeBetween + " (on gameonject " + gameObject.name + ")");
        }
        startPos = transform.position;
        if (myBody == null)
        {
            myBody = gameObject.GetComponent<Rigidbody2D>();
            if (myBody == null)
            {
                gameObject.AddComponent<Rigidbody2D>();
            }
        }
        if (myBody.attachedColliderCount == 0)
        {
            gameObject.AddComponent<CircleCollider2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            //walking
            //check too far
            if (transform.position.x > startPos.x + maxDist)
            {
                facing = true;
                //myAnimator.SetBool("facing", facing);
            }
            if (transform.position.x < startPos.x - maxDist)
            {
                facing = false;
                //myAnimator.SetBool("facing", facing);

            }

            //walk
            myBody.linearVelocity = (facing ? -1 : 1) * Vector2.right * moveSpeed;

        }
        else
        {
            //idling

        }

        timeTilNext -= Time.deltaTime;

        if (timeTilNext <= 0)
        {
            timeTilNext += aboutTimeBetween + Random.Range(-1f, 1f);
            isWalking = !isWalking;
            //myAnimator.SetBool("isWalking", isWalking);
            if (!isWalking)
            {
                myBody.linearVelocity = Vector3.zero;
            }
        }




    }
}
