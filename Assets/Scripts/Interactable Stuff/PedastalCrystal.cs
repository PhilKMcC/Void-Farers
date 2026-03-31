using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class PedastalCrystal : InvertCollectable, I_Interactable
{
    public static int CrystalsPlaced = 0;
    public static int MaxCrystals;

    public static string savefileLocationCrys = "/saveCrystals.csv";
    public static List<int> placedIDs;
    public bool IAmPlaced;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        MaxCrystals = 6;
        base.Start();
        if (myRenderer == null)
        {
            myRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        if (placedIDs == null)
        {
            placedIDs = new List<int>();
            loadCrystals();
        }
        
        myRenderer.enabled = false;

        if (placedIDs.Contains(ID))
        {
            SoftPlaceCrystal();
        }

        //Debug.Log("disabled rederer");
    }

    

    public void Interact()
    {
        CheckInvCollected();
        if (Collectable.Collectables.ContainsKey(ID))
        { 
            if (Collectable.Collectables[ID].collected)
            {
                PlaceCrystal();
            }
        }
    }

    public void PlaceCrystal()
    {
        if ( !IAmPlaced)
        {
            IAmPlaced = true;
            myRenderer.enabled = true;
            //check if all placed
            if (CrystalsPlaced < MaxCrystals - 1)
            {
                placedIDs.Add(ID);
                CrystalsPlaced++;
                saveCrystals();
            }
            else //last crystal
            {
                StartCoroutine(waitAMomentThenBoss());
                CameraControl.spawnBoss(50);
            }
        }
       
    }

    public IEnumerator waitAMomentThenBoss()
    {
        yield return new WaitForSeconds(10);
        golemController.state = 1;
    }

    public void SoftPlaceCrystal()
    {
        myRenderer.enabled = true;
        if (CrystalsPlaced < MaxCrystals - 1)
        {
            CrystalsPlaced++;
            IAmPlaced = true;
        }
    }
    
    public static string displayCrystals()
    {
        string s = " ";
        foreach (int id in placedIDs)
        {
            s += id + " , " ;
        }
        return s;
    }



    public static void resetCrystals()
    {
        if (placedIDs == null) { placedIDs = new List<int>(); }
        string path = Application.persistentDataPath + savefileLocationCrys;
        //Write some text to the test.txt file
        /*
        StreamWriter writer = new StreamWriter(path, false);
        //writer.WriteLine("Test");
        foreach (string s in vars.Keys)
        {
            //reset
            vars[s] = 0;
            //write index and collectedstatus
            writer.WriteLine(s + "," + (vars[s]));
        }
        writer.Close();
        */
        saveCrystals();

        //Print the text from the file for verification
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
    public static void loadCrystals()
    {
        if (placedIDs == null) { placedIDs = new List<int>(); }

        string path = Application.persistentDataPath + savefileLocationCrys;

        try
        {
            StreamReader reader = new StreamReader(path);
            string str = reader.ReadToEnd();
            reader.Close();
            //Debug.Log(str);
            string[] tokens = str.Split("\n");
            foreach (string token in tokens)
            {
                if (token.Length > 0)
                {
                    Debug.Log("Crystals loading: token: " + token);
                    placedIDs.Add(int.Parse(token));
                }
            }

        }
        catch (FileNotFoundException)
        {
            resetCrystals();
        }
    }
    public static void saveCrystals()
    {
        string path = Application.persistentDataPath + savefileLocationCrys;
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        //writer.WriteLine("Test");
        foreach (int id in placedIDs)
        {
            //write var and value
            writer.WriteLine(id);
        }
        writer.Close();

        //Print the text from the file for verification
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
#if UNITY_EDITOR
    [MenuItem("myMenu/deleteCrystalsData")]
#endif
    public static void deleteCrystalsData()
    {
        string path = Application.persistentDataPath + savefileLocationCrys;
        File.Delete(path);
        /*StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine("");
        writer.Close();*/
    }

    
}
