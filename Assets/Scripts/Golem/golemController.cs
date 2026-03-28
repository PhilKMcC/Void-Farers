using UnityEngine;

public class golemController : abstrGolem
{

    private int frameCounter;
    public Beam beamSpin;
    private float timer = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start state 1");
        state = 1;
        wait = true;
    }

    // Update is called once per frame
    void Update()
    {
        //If awoken, set state to 1
        Debug.Log(state);
        switch(state)
        {
            case 1:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    state = Random.Range(1, 6);
                    timer = 1;
                }
                break;
            case 5:
                if(!wait)
                {
                    beamSpin.StartBeamAttack(5);
                }
                break;
            default:
                break;
        }
    }

    public static void SpawnGolem()
    {
        Debug.Log("HOW THE F");
        state = 1;
    }
}
