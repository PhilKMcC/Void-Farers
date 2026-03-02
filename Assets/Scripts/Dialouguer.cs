using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialouguer : MonoBehaviour, I_Interactable
{
    /*
     * Class Explanation:
     * Used by any NPC who opens a dialougue box. this could be any character, sign, etc.
     * Attatch it to the gameobject who opens the dialougue on interaction, and also attach a "Interactable" component so it can be interacted.
     * Currently not sure what would happen if interact with two different dialogues at the same time....
     */

    public Sprite[] LeftSpeaker; // The image of the talker on the left side of the dialogue box, can have multiple if needed. defaults to index 0.
    public Sprite[] RightSpeaker; // The image of the talker on the right side of the dialogue box

    public string[] text; //the full text of the dialogue. currently doesn't support interruptions, or context, but that may alter in the future if we need it. indices separate a new phrase of dialogue.
    public string[] textMetadata; // meta data for the  dialougue. each corresponds to the text of the same index. must be same length as text. consists of "<L|R>,<index>". L for left active, R for right. index is index of that talker to update to.
    
    public static GameObject Canvas; //the canvas for the dialogue. will probably need to add more stuff, but basically this is thestuff this modifies. 
    public static Image LeftDialoguer;
    public static Image RightDialoguer;
    public static GameObject textBG;
    public static TextMeshProUGUI LeftText;
    public static TextMeshProUGUI RightText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (LeftSpeaker.Length == 0 || RightSpeaker.Length == 0)
        {
            Debug.Log("One or more talkers not assigned, using blanks! (gameObject " + gameObject.ToString() + ")");
        }
        if (Canvas == null)
        {
            initializeCanvas();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public virtual void Interact()
    {
        Debug.Log("dialougue interacted!");
        beginDialoguing();
    }

    public void initializeCanvas()
    {
        Canvas = GameObject.FindGameObjectWithTag("Dialogue");
        Image[] images = Canvas.GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            if (image.gameObject.name == "Right Dialoguer")
            {
                RightDialoguer = image;
            }
            if (image.gameObject.name == "Left Dialoguer")
            {
                LeftDialoguer = image;
            }
            if (image.gameObject.name == "Text BG")
            {
                textBG = image.gameObject;
            }
        }
        TextMeshProUGUI[] texts = textBG.GetComponentsInChildren<TextMeshProUGUI>();
        foreach(TextMeshProUGUI text in texts)
        {
            if (text.gameObject.name == "Leftern Text")
            {
                LeftText = text;
            }
            if (text.gameObject.name == "Rightern Text")
            {
                RightText = text;
            }
        }

        if (Canvas != null && RightDialoguer != null && LeftDialoguer != null && textBG != null && LeftText != null && RightText != null)
        {
            Debug.Log("successfully initialized dialogue");
        }
        else
        {
            Debug.Log("failed to initialize dialogue");
            Debug.Log("Canvas: " + Canvas.name);
            Debug.Log("LeftDialoguer: " + LeftDialoguer);
            Debug.Log("RightDialoguer: " + RightDialoguer);
            Debug.Log("TextBG: " + textBG);
            Debug.Log("Left Text: " + LeftText);
            Debug.Log("Right Text: " + RightText);
        }
        
        Canvas.SetActive(false);

    }


    public void beginDialoguing()
    {
        Canvas.SetActive(true);
        LeftDialoguer.sprite = LeftSpeaker[0];
        RightDialoguer.sprite = RightSpeaker[0];
        
        //do some string tokenizing, or something
        


    }

}
