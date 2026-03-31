using TMPro;
using UnityEngine;

public class deathCount : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextMeshProUGUI text;


    void Start()
    {
        if (text == null) { text = gameObject.GetComponent<TextMeshProUGUI>(); }
        if (ConditionalInteractor.vars.ContainsKey("deathCount"))
        {
            text.text = "Death Count: " + ConditionalInteractor.vars["deathCount"];
        }
        else
        {
            text.text = "Death Count: " + 0;
        }
    }

    
}
