using UnityEngine;

public class BGMmanager : MonoBehaviour
{

    //swaps to next track after a while

    public AudioSource[] sources; //all play on start, we swap out the loudness

    public static int current = 0;

    public float MaxVolume = 0.75f;

    private float timer;
    public float timeBetween = 360;

    private void Start()
    {
        if (sources.Length == 0)
        {
            sources = gameObject.GetComponents<AudioSource>();
        }
        timer = -1;

        float vol;
        if (PlayerPrefs.HasKey("Volume"))
        {
            vol = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            PlayerPrefs.SetFloat("Volume", 1);
            vol = PlayerPrefs.GetFloat("Volume");
        }
        AudioListener.volume = vol;
        PlayerPrefs.SetFloat("Volume", vol);
    }


    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = timeBetween;
            sources[current].volume = 0;
            current = (current+1) % sources.Length;
            sources[current].volume = MaxVolume;

        }
    }

    public void updateVolume(float MaxVol)
    {
        MaxVolume = MaxVol;
        sources[current].volume = MaxVolume;

    }
}
