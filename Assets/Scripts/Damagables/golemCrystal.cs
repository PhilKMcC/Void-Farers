using NUnit.Framework.Internal;
using System.Threading;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class golemCrystal : Abstr_Damagable
{

    public SpriteRenderer mySprite;
    public golemController control;
    public GameObject crystal;
    /* 
     * Class Explanation:
     * the crystal in the body of the golem
     */

    public float healthTotal = 3f;
    private float health;
    private bool damagable;
    private float timer = 3;


    void Start()
    {
        if (mySprite == null) { mySprite = gameObject.GetComponent<SpriteRenderer>(); }
        health = 3f;
        myTag = gameObject.tag;
        gameObject.SetActive(true);
        //initializeSets();
    }

    void Update()
    {
        if(!damagable)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                damagable = true;
                timer = 3;
                mySprite.color = Color.white;
            }
        }

    }
    public override void Damage()
    {
        if (damagable && golemController.state != 0)
        {
            health--;
            mySprite.color = Color.red;
            if (health == 0)
            {
                Die();
            }
            damagable = false;
        }
    }

    public void Die()
    {
        //Death animation
        //Set state to 6
        golemController.state = 6;
        crystal.SetActive(true);
        Debug.Log("Boss Dead");
        gameObject.SetActive(false);
        //Spawn Crystal Collectable
        //Upon that crystal's collection, teleport to demo zone

    }

    //Activate upon player death
    public void Heal()
    {
        health = healthTotal;
    }

}
