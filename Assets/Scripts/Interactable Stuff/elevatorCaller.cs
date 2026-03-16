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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = callDuration * 3;
        offset = locationA - locationB;
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
            StartCoroutine(CallElevator());
        }
    }

    protected virtual IEnumerator CallElevator()
    {
        timer = 0;
        Vector3 start = Elevator.transform.position;
        while(timer < callDuration) {
            Vector3 pos = start + offset * (timer/callDuration);
            Elevator.transform.position = pos;
            yield return null;
        }
        Elevator.transform.position = locationA;
        
        yield return new WaitForSeconds(callDuration);
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
