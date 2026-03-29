using UnityEngine;

public class golemController : abstrGolem
{

    private int frameCounter;
    public Beam beamSpin;
    private float timer = 1;
    private bool spinning;
    private float mostRecent = 0;
    private float secondMostRecent = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log("Start state 1");
        state = 1;
        wait = true;
        spinning = false;
    }

    // Update is called once per frame
    void Update()
    {
        //If awoken, set state to 1
        //Debug.Log(state);
        switch(state)
        {
            case 1:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    state = Random.Range(1, 6);
                    if(state == mostRecent || state == secondMostRecent)
                    {
                        state = Random.Range(1, 6);
                    }
                    if(state == mostRecent)
                    {
                        state = Random.Range(1, 6);
                    }
                    secondMostRecent = mostRecent;
                    mostRecent = state;
                    timer = 1;
                }
                break;
            case 5:
                if(!wait)
                {
                    if (!spinning)
                    {
                        spinning = true;
                        beamSpin.StartBeamAttack(5);
                    }
                }
                break;
            case 7:
                spinning = false;
                break;
            default:
                break;
        }
    }

    public static void SpawnGolem()
    {
        state = 1;
    }
}
