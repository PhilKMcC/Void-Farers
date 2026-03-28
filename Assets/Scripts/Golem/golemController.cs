using UnityEngine;

public class golemController : abstrGolem
{

    private int frameCounter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = 0;
        wait = true;
    }

    // Update is called once per frame
    void Update()
    {
        //If awoken, set state to 1
        if(state == 1)
        {
            for(frameCounter = 0; frameCounter < 60; frameCounter++)
            {

            }
            state = Random.Range(1, 6);
        }
    }
}
