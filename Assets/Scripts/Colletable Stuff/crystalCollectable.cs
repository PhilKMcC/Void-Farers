using UnityEngine;

public class crystalCollectable : Collectable
{

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
        //Animate something here?
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ship"))
        {
            Collect();
        }
    }
}
