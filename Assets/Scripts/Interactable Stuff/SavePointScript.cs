using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;


public class SavePointScript : Abstr_Interactable
{
    /*
     * Class Explanation:
     * This is used on save points
     * It saves the game overall
     * This includes player position, and the interactable variables
     * the collectables are handled elsewhere.
     * This way, collectables quicksave, other things need a full save. 
     * ^^^ this may mean that players can abuse this to pick branching paths simultaneaously ("take 1","nah, imma take both")
     * ^^^ alternatively, those can also force a hard save. lets agree on Vector3(99999,99999,99999) to mean "ignore me, stick with previous location"
     */

    public static string savefileLocationPosition = "/savePosition.txt";
    public Vector3 spawnLocation; // this should be this gameObject's location. if it is zero vector, it will default to this gO's pos. otherwise will go with what written.
    // so don't put one of these at 0,0,0 plz.
    public static Vector3 respawnLocation; //this is where the player respawns if dead. ie, most recent save.


    public static bool loaded = false;

    public static readonly Vector3 IgnoreMe = new Vector3(99999, 99999, 99999); 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (spawnLocation == Vector3.zero)
        {
            spawnLocation = gameObject.transform.position;
        }
        if (!loaded)
        {
            loadSavedata();
        }
    }

    public void resetSavedata()
    {
        //likely unused...
        resetPositionData();
        ConditionalInteractor.resetVars();
    }

    public void loadSavedata()
    {
        loadPositionData();
        ConditionalInteractor.loadVars();
    }
    public void saveSavedata()
    {
        savePositionData();
        ConditionalInteractor.saveVars();
    }

    public void resetPositionData()
    {
        string path = Application.persistentDataPath + savefileLocationPosition;
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        //writer.WriteLine("Test");
        writer.WriteLine("0,0,0");
        
        writer.Close();

        //Print the text from the file for verification
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

    public void loadPositionData()
    {
        string path = Application.persistentDataPath + savefileLocationPosition;

        try
        {
            StreamReader reader = new StreamReader(path);
            string str = reader.ReadToEnd();
            reader.Close();
            //Debug.Log(str);
            string[] tokens = str.Split("\n");
            foreach (string token in tokens)
            {
                //should only have 1 token
                if (token.Length > 0)
                {
                    string[] toks = token.Split(",");
                    Debug.Log("token count: " + toks.Length + " position: " + toks[0] + "," + toks[1] + "," + toks[2]);
                    Vector3 readPosition = new Vector3(int.Parse(toks[0]), int.Parse(toks[1]), int.Parse(toks[2]));
                    if (readPosition != Vector3.zero)
                    {
                        respawnLocation = readPosition;
                        GameObject.FindGameObjectWithTag("Player").transform.position = readPosition;
                    }
                }
            }

        }
        catch (FileNotFoundException)
        {
            resetPositionData();
        }
    }

    public void savePositionData()
    {
        string path = Application.persistentDataPath + savefileLocationPosition;
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        //writer.WriteLine("Test");
        //write postition as x,y,z
        writer.WriteLine(respawnLocation.x + "," + respawnLocation.y + "," + respawnLocation.z);
        writer.Close();

        //Print the text from the file for verification
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

#if UNITY_EDITOR
    [MenuItem("myMenu/deletePositionData")]
#endif
    public static void deletePositionData()
    {
        string path = Application.persistentDataPath + savefileLocationPosition;
        File.Delete(path);
        /*StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine("");
        writer.Close();*/
    }


    public override void Interact()
    {
        if (spawnLocation != IgnoreMe){
            respawnLocation = spawnLocation;
        }
        saveSavedata();
    }

    
    
}
