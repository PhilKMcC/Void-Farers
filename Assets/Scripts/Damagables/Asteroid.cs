using UnityEngine;

public class Asteroid : Abstr_Damagable
{
    /* 
     * Class Explanation:
     * basic asteroid. explodes when hit
     * need at least one in the scene for the collisions to work properly, since we need something to initialize them
     */

    public GameObject explosion;

    void Start()
    {
        myTag = gameObject.tag;
        initializeSets();
    }
    public override void Damage()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        gameObject.SetActive(false);
        Debug.Log("kaboom!");
    }

}
