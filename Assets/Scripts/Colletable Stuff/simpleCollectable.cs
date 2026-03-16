using UnityEngine;

public class simpleCollectable : Collectable
{
    /*
     * Class Explanation:
     * a simple example of the collection system.
     * if you walk into it, it becomes collected and disappears
     * and it is saved, via the collectables script
     * You probably shouldn't use this specifically, but make something like it.
     * Note for testing purposes, this does save even in the editor, so if you need to reset the collectables so you can get them again, go to the editor and go to myMenu/deleteCollection
     */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        if (collected)
        {
            gameObject.SetActive(false);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Collect()
    {
        collected = true;
        saveCollection();
        gameObject.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ship"))
        {
            Collect();
        }
    }
}
