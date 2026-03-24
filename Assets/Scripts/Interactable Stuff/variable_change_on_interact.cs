using UnityEngine;

public class variable_change_on_interact : Abstr_Interactable
{
    /*
     * Class Explanation:
     * a means of getting the variables for the conditional interactor to work.
     * is itself an interactable, so at simplest counts interactions.
     * but could count anything.
     * likely will not be the only means of updating those values.
     */
    public string variable_to_change;
    public enum Mode
    {
        SET,
        INC
    }
    public Mode mode;
    public int value;

    public override void Interact()
    {
        if (mode == Mode.SET)
        {
            ConditionalInteractor.setVar(variable_to_change, value);
        }
        if (mode == Mode.INC)
        {
            ConditionalInteractor.incVar(variable_to_change, value);
        }
        Debug.Log("variable updated: " + variable_to_change + " is now " + ConditionalInteractor.vars[variable_to_change]);
    }
}
