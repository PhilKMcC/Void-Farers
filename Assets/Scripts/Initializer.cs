using System.Collections;
using UnityEngine;

public class Initializer : MonoBehaviour, I_Initializable
{
    /*
     * Class Explanaion:
     * This should only exist on exactly one gameobject somewhere in the scene
     * it calls the initial all method and then kills itself
     * someones gotta do it.
     */
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        I_Initializable.initials.Add(this);
        StartCoroutine(initialize());
    }

    /* Update is called once per frame
    void Update()
    {
        I_Initializable.initAll();
        //gameObject.SetActive(false);

    }*/

    IEnumerator initialize()
    {
        yield return null;

        I_Initializable.initAll();
        yield return null;
    }

    public void init()
    {
        Destroy(gameObject);

    }
}
