using System.Collections;
using UnityEngine;

public class elevatorCaller : Abstr_Interactable
{
    /*
     * Class Explanation:
     * Calls the assigned eleavtor to the location A. after a while returns it to location B
     * Added this because I want to see what happens if player is crushed between floor and elevator.
     */


    public GameObject Elevator;
    public Vector3 locationA;
    public Vector3 locationB;
    protected Vector3 offset; //locationB -locationA

    public float callDuration = 5;
    public float timer = 0;

    public SpriteRenderer myRenderer;
    public Color defaultColor;
    public Color calledColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = callDuration * 3;
        offset = locationA - locationB;
        if (myRenderer == null)
        {
            myRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        if (defaultColor == null)
        {
            defaultColor = myRenderer.color;
        }
        if (calledColor == null)
        {
            calledColor = Color.yellow;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < callDuration * 3)
        {
            timer += Time.deltaTime;
        }
    }

    public override void Interact()
    {
        if (timer >= callDuration * 3)
        {
            Debug.Log("ElevatorCalled");
            midAction = true;
            myRenderer.color = calledColor;
            StartCoroutine(CallElevator());
        }
    }

    protected virtual IEnumerator CallElevator()
    {
        
        timer = 0;
        Vector3 start = Elevator.transform.position;
        Vector3 startOffset = locationA - start;
        if (Elevator.transform.position != locationA) //quick skip if already here
        {
            while (timer < callDuration)
            {
                Vector3 pos = start + startOffset * (timer / callDuration);
                Elevator.transform.position = pos;
                yield return null;
            }
            Elevator.transform.position = locationA;

            yield return new WaitForSeconds(callDuration);
        }
        myRenderer.color = defaultColor;
        timer = callDuration * 2;
        while(timer < callDuration * 3)
        {
            Vector3 pos = locationA - offset * ((timer - callDuration * 2) / callDuration);
            Elevator.transform.position = pos;
            yield return null;
        }
        Elevator.transform.position = locationB;

        midAction = false;
        yield return null;
    }

}
