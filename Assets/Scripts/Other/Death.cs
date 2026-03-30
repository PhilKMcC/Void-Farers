using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour 
{
    public static GameObject deathCanvas;
    public static void Die()
    {

        deathCanvas.SetActive(true);
        Death d = Camera.main.gameObject.AddComponent<Death>();
        d.StartCoroutine(waitForInput()); 

    }

    public static IEnumerator waitForInput()
    {
        int deathcount = 0;
        if (ConditionalInteractor.vars.ContainsKey("deathCount"))
        {
             deathcount = ConditionalInteractor.vars["deathCount"];
        }
        ConditionalInteractor.vars["deathCount"] = deathcount + 1;
        Debug.Log("Death Count:" + deathcount);


        SavePointScript sps = SavePointScript.FindFirstObjectByType<SavePointScript>();
        sps.saveSavedata();
        SavePointScript.loaded = false;


        InputSystem.actions.FindActionMap("Dialogue").Enable();

        while (!Dialouguer.nextAction.WasPressedThisDynamicUpdate())
        {
            yield return null;
        }

        Collectable.Collectables = null;
        PedastalCrystal.placedIDs = null;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        InputSystem.actions.FindActionMap("Global").Enable();
        

    }
}
