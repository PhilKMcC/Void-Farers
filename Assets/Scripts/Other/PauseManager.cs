using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    public GameObject pauseCanvas;
    private float holdTimeScale;
    private bool isPaused = false;
    public static List<InvertCollectable> InvCollectables;
    public AudioSource pauseAudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvCollectables = new List<InvertCollectable>();
        ImageInvCollectable[] cols = GameObject.FindObjectsByType<ImageInvCollectable>(FindObjectsSortMode.None);

        foreach (ImageInvCollectable col in cols)
        {
            InvCollectables.Add(col);
        }
        pauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputSystem.actions.FindAction("Global/Pause").WasPressedThisFrame())
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }


    public void Pause()
    {
        pauseAudioSource.Play();
        holdTimeScale = Time.timeScale;
        Time.timeScale = 0;
        isPaused = true;
        pauseCanvas.SetActive(true);
        foreach (ImageInvCollectable col in InvCollectables)
        {
            col.CheckInvCollected();
        }

    }
    public void Resume()
    {
        Time.timeScale = holdTimeScale;
        isPaused = false;
        pauseCanvas.SetActive(false);
    }

    public void ToMainMenu()
    {
        SavePointScript.loaded = false;
        Time.timeScale = holdTimeScale;
        Debug.Log("toMain");
        SceneManager.LoadScene("MainMenu");

    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void SelfDestruct()
    {
        Resume();
        Death.Die();
    }
}
