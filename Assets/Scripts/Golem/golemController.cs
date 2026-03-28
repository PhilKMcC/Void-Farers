using UnityEngine;

public class golemController : abstrGolem
{

    private int frameCounter;
    public Beam beamSpin;
    private float timer = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = 1;
        wait = true;
    }

    // Update is called once per frame
    void Update()
    {
        //If awoken, set state to 1
        if(state == 1)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                state = Random.Range(1, 6);
                Debug.Log(state);
            }
        }
        if(state == 5 && !wait)
        {
            beamSpin.StartBeamAttack(5);
        }
    }

    public static void SpawnGolem()
    {
        state = 1;
    }
}
