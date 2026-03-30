using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{

    public GameObject pauseCanvas;
    private float holdTimeScale;
    private bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        holdTimeScale = Time.timeScale;
        Time.timeScale = 0;
        isPaused = true;
        pauseCanvas.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = holdTimeScale;
        isPaused = false;
        pauseCanvas.SetActive(false);
    }

    public void ToMainMenu()
    {

    }

    public void Quit()
    {

    }

    public void SelfDestruct()
    {
        Resume();
        Death.Die();
    }
}
