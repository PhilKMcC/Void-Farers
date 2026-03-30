using UnityEngine;
using UnityEngine.UI;

public class ImageInvCollectable : InvertCollectable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image myImage;

    public override void copySprite()
    {
        if (myImage == null)
        {
            myImage = gameObject.GetComponent<Image>();
        }

        if (myImage.sprite == null)
        {
            myImage.sprite = Collectable.Collectables[ID].gameObject.GetComponent<SpriteRenderer>().sprite;
        }
        if (myImage.color == Color.white)
        {
            myImage.color = Collectable.Collectables[ID].gameObject.GetComponent<SpriteRenderer>().color;
        }

    }

    public override void CheckInvCollected()
    {
        Debug.Log("ImageInvCol called!!!!!");
        if (Collectable.Collectables.ContainsKey(ID))
        {
            if (Collectable.Collectables[ID].collected)
            {
                gameObject.SetActive(true);
                copySprite();

            }
            else
            {
                gameObject.SetActive(false);
            }
        }

    }
}
