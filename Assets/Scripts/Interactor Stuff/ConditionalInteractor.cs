using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

public class ConditionalInteractor : Interactor
{
    /*
     * Class Explanation:
     * The basic interactable class doesn't allow for conditional execution, so this is allowing that.
     * takes a bit more set up, but allows for conditions.
     * for each interaction, if it's variable's value in vars is between the min and max (both inclusive) it is interacted with.
     * eventually, will want to save/load this stuff
     */

    [Serializable]
    public struct Interaction
    {
        public Abstr_Interactable behaviour;
        public string variable;
        public int min;
        public int max;
    }

    public Interaction[] interactions;
    public static Dictionary<string, int> vars;
    public static string savefileLocationVars = "/saveVars.csv";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        InteractAction = InputSystem.actions.FindAction("Player/Interact");
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        interactables = null;

        if (vars == null)
        {
            vars = new Dictionary<string, int>();
            //loadVars();
            //loading of the variables is called by the savePoint script.


        }
        foreach (Interaction interact in interactions)
        {
            if (!vars.ContainsKey(interact.variable))
            {
                vars[interact.variable] = 0;
            }
        }
        Debug.Log("Conditional interaction vars: " + displayVars());

    }

    public override void Interact()
    {
        Debug.Log("Interacted conditionally!");
        foreach (Interaction interaction in interactions)
        {
            if (vars[interaction.variable] >= interaction.min && vars[interaction.variable] <= interaction.max)
            {
                interaction.behaviour.Interact();
            }
        }
    }

    public static void setVar(string varname, int value)
    {
        vars[varname] = value;
    }
    public static void incVar(string varname, int byWhat)
    {
        vars[varname] += byWhat;
    }

    public static string displayVars()
    {
        string s = "";
        foreach (string var in vars.Keys)
        {
            s += "["+var + "," + vars[var] + "] ";
        }
        return s;
    }

    public static void resetVars()
    {
        if (vars == null) { vars = new Dictionary<string, int>(); }
        string path = Application.persistentDataPath + savefileLocationVars;
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
        writer.Close();*/
    }
}
