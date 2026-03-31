using UnityEngine;

public class crystalCollectable : Collectable
{
    //collectable index: crystals are 200-299 (probably only need 200-205, for 6 crystals)
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Dialouguer crystalDialogue;
    private AudioSource crystalAudio;
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
        crystalDialogue.Interact();
        if (crystalAudio == null) { crystalDialogue.gameObject.GetComponent<AudioSource>().Play(); }
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
