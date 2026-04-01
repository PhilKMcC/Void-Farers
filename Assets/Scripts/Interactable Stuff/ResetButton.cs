using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class resetButton : Abstr_Interactable
{
    /*
     * Class Explanation:
     * Calls the assigned eleavtor to the location A. after a while returns it to location B
     * Added this because I want to see what happens if player is crushed between floor and elevator.
     */

    public SpriteRenderer myRenderer;
    public Color defaultColor;
    public Color calledColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (myRenderer == null)
        {
            myRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        if (defaultColor == null)
        {
            defaultColor = myRenderer.color;
        }
        if (calledColor == null)
        {
            calledColor = Color.yellow;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact()
    {
            Debug.Log("RESET");
            midAction = true;
            myRenderer.color = calledColor;
            Collectable.deleteCollection();
            ConditionalInteractor.deleteConditionalData();
            PedastalCrystal.deleteCrystalsData();
            SavePointScript.deletePositionData();




        SavePointScript.respawnLocation = Vector3.zero;
        SavePointScript.loaded = false;
        SavePointScript sps = SavePointScript.FindFirstObjectByType<SavePointScript>();
        sps.saveSavedata();


        Collectable.Collectables = null;
        Collectable.InvCollectables = null;
        PedastalCrystal.placedIDs = null;
        ConditionalInteractor.vars.Clear();
        ConditionalInteractor.vars["deathCount"] = 0;
        ConditionalInteractor.saveVars();
        ConditionalInteractor.vars = null;


        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        InputSystem.actions.FindActionMap("Global").Enable();
        Debug.Log("RESET DONE");

    }

}
