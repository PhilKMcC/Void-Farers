using UnityEngine;

public class golemCrystal : Abstr_Damagable
{
    /* 
     * Class Explanation:
     * basic asteroid. explodes when hit
     * need at least one in the scene for the collisions to work properly, since we need something to initialize them
     */

    public float healthTotal = 3f;
    private float health;

    void Start()
    {
        health = 3f;
        myTag = gameObject.tag;
        initializeSets();
    }

    void Update()
    {
        //Deal with states here?
    }
    public override void Damage()
    {
        health--;
        //Hurt animation
        if(health == 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        //Death animation
        //Set state to 6
        Destroy(gameObject);
        //Spawn Crystal Collectable
        //Upon that crystal's collection, teleport to demo zone
        Debug.Log("Boss dead!");
    }

    //Activate upon player death
    public override void Heal()
    {
        health = heathTotal;
    }

}
