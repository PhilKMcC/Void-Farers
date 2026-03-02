using UnityEngine;

public class Dialouguer : MonoBehaviour, I_Interactable
{
    /*
     * Class Explanation:
     * Used by any NPC who opens a dialougue box. this could be any character, sign, etc.
     * Attatch it to the gameobject who opens the dialougue on interaction, and also attach a "Interactable" component so it can be interacted.
     * Currently not sure what would happen if interact with two different dialogues at the same time....
     */

    public Sprite LeftTalker; // The image of the talker on the left side of the dialogue box
    public Sprite RightTalker; // The image of the talker on the right side of the dialogue box

    public string[] text; //the full text of the dialogue. currently doesn't support interruptions, or context, but that may alter in the future if we need it. indices separate a new phrase of dialogue.
    public string[] textMetadata; // meta data for the  dialougue. each corresponds to the text of the same index. must be same length as text. consists of <L|R>. L for left active, R for right. may add more later.

    public static GameObject Canvas; //the canvas for the dialogue. will probably need to add more stuff, but basically this is thestuff this modifies. 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (LeftTalker == null || RightTalker == null)
        {
            Debug.Log("One or more talkers not assigned, using blanks! (gameObject " + gameObject.ToString() + ")");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public virtual void Interact()
    {
        Debug.Log("dialougue interacted!");
    }
}
