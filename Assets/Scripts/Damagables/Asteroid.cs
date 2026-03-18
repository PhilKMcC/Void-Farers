using UnityEngine;

public class Asteroid : Abstr_Damagable
{
    /* 
     * Class Explanation:
     * basic asteroid. explodes when hit
     * need at least one in the scene for the collisions to work properly, since we need something to initialize them
     */

    void Start()
    {
        myTag = gameObject.tag;
        initializeSets();
    }
    public override void Damage()
    {
        gameObject.SetActive(false);
        Debug.Log("kaboom!");
    }

}
