using UnityEngine;

public class crystalProjectile : MonoBehaviour
{
    public float speed = 15f;
    public Rigidbody2D myBody;
    private float timer = 3;
    private float randModifier;
    public float maxRotation = 91f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        randModifier = Random.Range(50, 200) / 100;
        if (myBody == null) { myBody = gameObject.GetComponent<Rigidbody2D>(); }
        transform.rotation = Quaternion.Euler(Vector3.forward * Random.Range(45, maxRotation));
        myBody.linearVelocity = transform.up * speed * randModifier;
    }

    // Update is called once per frame
    void Update()
    {
        myBody.gravityScale = 1;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
