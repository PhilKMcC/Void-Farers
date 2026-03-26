using UnityEngine;

public class golemCrystal : Abstr_Damagable
{
    /* 
     * Class Explanation:
     * the crystal in the body of the golem
     */

    public float healthTotal = 3f;
    private float health;

    void Start()
    {
        health = 3f;
        myTag = gameObject.tag;
        //initializeSets();
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

    public  void Die()
    {
        //Death animation
        //Set state to 6
        Destroy(gameObject);
        //Spawn Crystal Collectable
        //Upon that crystal's collection, teleport to demo zone
        Debug.Log("Boss dead!");
    }

    //Activate upon player death
    public  void Heal()
    {
        health = healthTotal;
    }

}
