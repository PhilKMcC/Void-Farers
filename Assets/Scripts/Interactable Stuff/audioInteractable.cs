using UnityEngine;

public class audioInteractable : Abstr_Interactable
{

    public AudioSource audSrc;


    public override void Interact()
    {
        audSrc.Play();
    }
}
