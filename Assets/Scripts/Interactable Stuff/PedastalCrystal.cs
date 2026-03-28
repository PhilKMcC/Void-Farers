using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class PedastalCrystal : InvertCollectable, I_Interactable
{
    public static int CrystalsPlaced = 0;
    public static int MaxCrystals;

    public static string savefileLocationVars = "/saveCrystals.csv";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        MaxCrystals = 4;
        base.Start();
        if (myRenderer == null)
        {
            myRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        myRenderer.enabled = false;
        Debug.Log("disabled rederer");
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
        myRenderer.enabled = true;
        //check if all placed
    }
    /*
    public static string displayVars()
    {
        string s = "";
        foreach (string var in vars.Keys)
        {
            s += "[" + var + "," + vars[var] + "] ";
        }
        return s;
    }

    public static void resetVars()
    {
        if (vars == null) { vars = new Dictionary<string, int>(); }
        string path = Application.persistentDataPath + savefileLocationVars;
        //Write some text to the test.txt file*/
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
        *//*
        saveVars();

        //Print the text from the file for verification
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
    public static void loadVars()
    {
        if (vars == null) { vars = new Dictionary<string, int>(); }

        string path = Application.persistentDataPath + savefileLocationVars;

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
                    string[] toks = token.Split(",");
                    Debug.Log("token count: " + toks.Length + " var: " + toks[0] + ", value:" + toks[1] + "<-");
                    vars[toks[0]] = int.Parse(toks[1]);
                    Debug.Log("test collected: " + toks[0] + vars[toks[0]]);
                }
            }

        }
        catch (FileNotFoundException)
        {
            resetVars();
        }
    }
    public static void saveVars()
    {
        string path = Application.persistentDataPath + savefileLocationVars;
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        //writer.WriteLine("Test");
        foreach (string s in vars.Keys)
        {
            //write var and value
            writer.WriteLine(s + "," + (vars[s]));
        }
        writer.Close();

        //Print the text from the file for verification
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

    [MenuItem("myMenu/deleteConditionalData")]
    public static void deleteConditionalData()
    {
        string path = Application.persistentDataPath + savefileLocationVars;
        File.Delete(path);
        /*StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine("");
        writer.Close();*//*
    }

    */
}
