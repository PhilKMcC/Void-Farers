using UnityEngine;
using System.Collections.Generic;
public abstract class Abstr_Damagable : MonoBehaviour, I_Damagable
{
    /*
     * Class Explanation:
     * Things that can be damaged, such as enemies, asteroids, inherit from this.
     * when they are hit by an opposing attack they do something, such as taking damage or dying, as defined by Damage().
     * opposing is defined by the tags. like tags don't hurt each other. other tags do.
     * Player, Ship, and friendly are like tags. damgaged by enemies and nuetrals
     * enemy and boss are like tags, includes basic enemies. damaged by friendlies and nuetrals
     * nuetral is a tag. includes stuff like asteroids, or traps. damaged by friendlies and enemies.
     */


    public static HashSet<string> friendliesTags;
    public static HashSet<string> enemiesTags;
    public static HashSet<string> neutralsTags;
    public bool DamagedByTiles;

    protected string myTag = null;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myTag = gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Damage();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (myTag == null) { myTag = gameObject.tag; }

        //if the other is one of the fighers, and isn't the same category as mine (friendly fire off), damage me
        // this does mean if you walk into an enemy, both damaged, which is fine.
        string otherTag = collision.gameObject.tag;
        Debug.Log("collision! me: " + myTag + "; other: " + otherTag);

        if (!friendliesTags.Contains(myTag) && friendliesTags.Contains(otherTag))
        {
            Damage();
        }
        if (!enemiesTags.Contains(myTag) && enemiesTags.Contains(otherTag))
        {
            Damage();
        }
        if (!neutralsTags.Contains(myTag) && neutralsTags.Contains(otherTag))
        {
            Damage();
        }
        if (DamagedByTiles && collision.gameObject.layer == 9)
        {
            Damage();
        }
    }

    public static void initializeSets()
    {
        friendliesTags = new HashSet<string>();
        friendliesTags.Add("Player");
        friendliesTags.Add("Ship");
        friendliesTags.Add("Friendly");

        enemiesTags = new HashSet<string>();
        enemiesTags.Add("Enemy");
        enemiesTags.Add("Boss");

        neutralsTags = new HashSet<string>();
        neutralsTags.Add("Nuetral");


    }
}
