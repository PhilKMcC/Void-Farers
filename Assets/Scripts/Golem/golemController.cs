using UnityEngine;

public class golemController : abstrGolem
{

    private int frameCounter;
    public Beam beamSpin;
    private float timer = 1;
    private bool spinning;
    private float mostRecent = 0;
    private float secondMostRecent = 0;

    public AudioSource deathSound;
    private bool notPlayed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log("Start state 1");
        state = 0;
        wait = true;
        spinning = false;
        notPlayed = true;
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
                        beamSpin.StartBeamAttack(7.5f);
                    }
                }
                break;
            case 6:
                if (notPlayed)
                {
                    deathSound.Play();
                    notPlayed = false;
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
