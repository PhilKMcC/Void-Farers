using NUnit.Framework.Internal;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class golemCrystal : Abstr_Damagable
{

    public golemController control;
    /* 
     * Class Explanation:
     * the crystal in the body of the golem
     */

    public float healthTotal = 3f;
    private float health;
    private bool damagable;
    private float timer = 3;
    private Vector3 startPos;
    private Vector3 rightPos;
    private Vector3 leftPos;
    private int animState = 0;
    private float distanceVector;

    void Start()
    {
        health = 3f;
        myTag = gameObject.tag;
        startPos = transform.position;
        rightPos = startPos;
        leftPos = startPos;
        rightPos.x += 2;
        leftPos.x -= 2;
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
                animState = 0;
            }
        }
    }
    public override void Damage()
    {
        if (damagable && golemController.state != 0)
        {
            health--;
            hurtAnim();
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
        Destroy(gameObject);
        //Spawn Crystal Collectable
        //Upon that crystal's collection, teleport to demo zone
        Debug.Log("Boss dead!");
    }

    //Activate upon player death
    public void Heal()
    {
        health = healthTotal;
    }
    
    void hurtAnim()
    {
        while (animState != 5)
        {
            switch (animState)
            {
                case 0:
                    distanceVector = Vector2.Distance(transform.position, rightPos);
                    if (distanceVector != 0)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, rightPos, 10 * Time.deltaTime);
                    }
                    else
                    {
                        animState = 1;
                    }
                    break;
                case 1:
                    distanceVector = Vector2.Distance(transform.position, leftPos);
                    if (distanceVector != 0)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, leftPos, 10 * Time.deltaTime);
                    }
                    else
                    {
                        animState = 2;
                    }
                    break;
                case 2:
                    distanceVector = Vector2.Distance(transform.position, rightPos);
                    if (distanceVector != 0)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, rightPos, 10 * Time.deltaTime);
                    }
                    else
                    {
                        animState = 3;
                    }
                    break;
                case 3:
                    distanceVector = Vector2.Distance(transform.position, leftPos);
                    if (distanceVector != 0)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, leftPos, 10 * Time.deltaTime);
                    }
                    else
                    {
                        animState = 4;
                    }
                    break;
                case 4:
                    distanceVector = Vector2.Distance(transform.position, startPos);
                    if (distanceVector != 0)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, startPos, 10 * Time.deltaTime);
                    }
                    else
                    {
                        animState = 5;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
