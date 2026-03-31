using TMPro;
using UnityEngine;

public class deathCount : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextMeshProUGUI text;


    void Start()
    {
        if (text == null) { text = gameObject.GetComponent<TextMeshProUGUI>(); }
        text.text = "Death Count: " + ConditionalInteractor.vars["deathCount"];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
