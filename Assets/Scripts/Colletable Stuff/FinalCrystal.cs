using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class FinalCrystal : Collectable
{
    //collectable index: crystals are 200-299 (probably only need 200-205, for 6 crystals)

   // public GameObject player;
    public GameObject ship;
    public golemController control;
    //private bool uninit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        if (ship == null) { ship = GameObject.FindGameObjectWithTag("Ship"); }
        //uninit = true;
        gameObject.SetActive(false);
        //if (player == null) { player = GameObject.FindGameObjectWithTag("Player"); }
    }

    // Update is called once per frame
    void Update()
    {
        /*if(golemController.state == 6 && uninit)
        {
            Debug.Log("Activate");
            gameObject.SetActive(true);
            uninit = false;
        }*/
    }

    public override void Collect()
    {
        collected = true;
        saveCollection();
        gameObject.SetActive(false);
        //player.transform.position = new Vector2(-537, -530);
        ship.transform.position = new Vector2(-540, -530);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ship"))
        {
            Collect();
        }
    }
}

