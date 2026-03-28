using UnityEngine;

public class shootingStar : MonoBehaviour
{
    public Rigidbody2D myBody;
    public float Speed = 5f;

    private float distanceVector;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool movingDown = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }
        startPos = gameObject.transform.position;
        endPos = startPos;
        endPos.y = endPos.y - 15;
    }

    // Update is called once per frame
    void Update()
    {
        moveDown();
    }

    void moveDown()
    {
        distanceVector = Vector2.Distance(transform.position, endPos);

        if (!distanceVector.Equals(0) && movingDown)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, endPos, Speed * Time.deltaTime);
        }
        else
        {
            movingDown = false;
            moveBack();
        }
    }

    void moveBack()
    {
        distanceVector = Vector2.Distance(transform.position, startPos);

        if (!distanceVector.Equals(0))
        {
            transform.position = Vector2.MoveTowards(this.transform.position, startPos, Speed * Time.deltaTime);
        }
        else
        {
            movingDown = true;
            moveDown();
        }

    }
}
