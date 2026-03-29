using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    /*
     * Class Explanation:
     * all collectables should inherit from this.
     * this includes paint cans, ships, emotes, boss parts anything that is unlocked and saved.
     * each collectible should have a different ID. for convenience, we could have, ex, paintcans have 0-99, ships have 100-199, etc.
     * there will be something else later for things that check if things are collected, ex customization room.
     * most collectables will connect as a result of OnTriggerEnter2D. 
     * Note for testing purposes, this does save even in the editor, so if you need to reset the collectables so you can get them again, go to the editor and go to myMenu/deleteCollection
     */

    //instance variables
    public int ID;
    public bool collected;

    //used to access by ID
    public static Dictionary<int, Collectable> Collectables;
    //subclasses might want their own Dicts, for example so that the customizer can search up paints somehow

    //used for saving
    public static string savefileLocationCollectables = "/saveCollectables.csv";

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        if (Collectables == null)
        {
            Collectables = new Dictionary<int, Collectable>();
            Collectable[] allcollected = GameObject.FindObjectsByType<Collectable>(FindObjectsSortMode.None);

            foreach (Collectable collectable in allcollected) 
            {
                Collectables[collectable.ID] = collectable;
            }
            loadCollection();
            saveCollection();


            

        }
        if (Collectables[ID] != this)
        {
            Debug.Log("you have two collectables with the same ID: " + gameObject.name + ", " +  Collectables[ID].name);
        }
    }

    public abstract void Collect();

    public static void resetCollection()
    {
        string path = Application.persistentDataPath + savefileLocationCollectables;
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        //writer.WriteLine("Test");
        foreach (int i in Collectables.Keys)
        {
            //reset
            Collectables[i].collected = false;
            //write index and collectedstatus
            writer.WriteLine(i + "," + (Collectables[i].collected ? 0 : 1));
        }
        writer.Close();

        //Print the text from the file for verification
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

    public static void loadCollection()
    {
        string path = Application.persistentDataPath + savefileLocationCollectables;

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
                    Debug.Log("token count: " + toks.Length + " ID: " + toks[0] + ", values:" + toks[1] + "<-");
                    Collectables[int.Parse(toks[0])].collected = (int.Parse(toks[1]) == 1);
                    Debug.Log("test collected: " + Collectables[int.Parse(toks[0])].collected);
                }
            }

        }
        catch (FileNotFoundException)
        {
            resetCollection();
        }
    }

    public static void saveCollection()
    {
        string path = Application.persistentDataPath + savefileLocationCollectables;
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        //writer.WriteLine("Test");
        foreach (int  i in Collectables.Keys)
        {
            //write index and collectedstatus
            writer.WriteLine(i + "," + (Collectables[i].collected?1:0));
        }
        writer.Close();

        //Print the text from the file for verification
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();


        InvertCollectable[] allInvCollected = GameObject.FindObjectsByType<InvertCollectable>(FindObjectsSortMode.None);
        foreach (InvertCollectable invertCollectable in allInvCollected)
        {
            invertCollectable.CheckInvCollected();
        }
    }
#if UNITY_EDITOR
    [MenuItem("myMenu/deleteCollectionData")]
#endif
    public static void deleteCollection()
    {
        string path = Application.persistentDataPath + savefileLocationCollectables;
        File.Delete(path);
        /*StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine("");
        writer.Close();*/
    }

}
