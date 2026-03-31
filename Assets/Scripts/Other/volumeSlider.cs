using UnityEngine;
using UnityEngine.UI;

public class volumeSlider : MonoBehaviour
{

    public Slider volumeSlide;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            volumeSlide.value = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            PlayerPrefs.SetFloat("Volume", 1);
            volumeSlide.value = PlayerPrefs.GetFloat("Volume");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeVol()
    {
        AudioListener.volume = volumeSlide.value;
        PlayerPrefs.SetFloat("Volume", volumeSlide.value);
    }
}
