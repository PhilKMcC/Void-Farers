using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InteractAction = InputSystem.actions.FindAction("Player/Interact");
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        interactables = null;

        vars = new Dictionary<string, int>();
        //loadVars();
        foreach (Interaction interact in interactions)
        {
            if (!vars.ContainsKey(interact.variable)){
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
                interaction.behaviour.Interact();
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

    /*
    public static void loadVars(){

    }
    public static void saveVars(){

    }
    */

    public static string displayVars()
    {
        string s = "";
        foreach (string var in vars.Keys)
        {
            s += "["+var + "," + vars[var] + "] ";
        }
        return s;
    }
}
