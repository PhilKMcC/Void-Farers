using System.Collections;
using UnityEngine;

public class HalfElevatorCaller : elevatorCaller
{
    /*
     * Class Explanation:
     * half of an elevator caller.
     * just calls it to a location, doesn't leave it.
     * nothing here.
     */

    protected override IEnumerator CallElevator()
    {
        timer = 0;
        Vector3 start = Elevator.transform.position;
        offset = locationA - start; 
        while (timer < callDuration)
        {
            Vector3 pos = start + offset * (timer / callDuration);
            Elevator.transform.position = pos;
            yield return null;
        }
        Elevator.transform.position = locationA;
        timer = callDuration * 3;
        midAction = false;
        myRenderer.color = defaultColor;
    }
}
