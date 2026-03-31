using UnityEngine;
using UnityEngine.Audio;

public class BGMmanager : MonoBehaviour
{

    //swaps to next track after a while

    public AudioSource player; //all play on start, we swap out the loudness
    public AudioClip[] sources;
    public static int current = 0;

    public float MaxVolume = 0.75f;

    private float timer;
    public float timeBetween = 360;

    private void Start()
    {
        
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
            current = (current+1) % sources.Length;
            player.clip = sources[current];
            player.Play();
        }
    }

    public void updateVolume(float MaxVol)
    {
        MaxVolume = MaxVol;
        player.volume = MaxVolume;

    }
}
