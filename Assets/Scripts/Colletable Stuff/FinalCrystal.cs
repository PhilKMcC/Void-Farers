using Unity.VisualScripting;
using UnityEngine;

public class FinalCrystal : Collectable
{
    //collectable index: crystals are 200-299 (probably only need 200-205, for 6 crystals)

    public GameObject player;
    public GameObject ship;
    public golemController control;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        gameObject.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        if(golemController.state == 6)
        {
            gameObject.SetActive(true);
        }
    }

    public override void Collect()
    {
        collected = true;
        saveCollection();
        gameObject.SetActive(false);
        player.transform.position = new Vector2(-537, -530);
        ship.transform.position = new Vector2(-540, -530);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ship"))
        {
            Collect();
        }
    }
}

