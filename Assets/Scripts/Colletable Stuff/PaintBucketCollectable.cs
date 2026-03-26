using UnityEngine;
using UnityEditor;

public class PaintBucketCollectable : Collectable
{
    /*
     * Class Explanation:
     * the paint bucket collectables.
     * They have a color associated.
     * Hypothetically, we could later do patterns, but that would use a different script.
     * if you walk into it, it becomes collected and disappears
     * we should probably have some collect effect
     * and it is saved, via the collectables script
     * Note for testing purposes, this does save even in the editor, so if you need to reset the collectables so you can get them again, go to the editor and go to myMenu/deleteCollection
     */

    //collectable index 100-199.

    public Color paintColor = Color.white;
    public SpriteRenderer paintRenderer; //note, this is the renderer for the PAINT, not the BUCKET.
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        paintRenderer.color = paintColor;
        if (collected)
        {
            gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Collect()
    {
        collected = true;
        saveCollection();
        gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collect();
    }

    [MenuItem("myMenu/applyPaint")]
    public static void applyPaint()
    {
        PaintBucketCollectable[] buckets = GameObject.FindObjectsByType<PaintBucketCollectable>(FindObjectsSortMode.None);
        foreach (PaintBucketCollectable bucket in buckets)
        {
            bucket.paintRenderer.color = bucket.paintColor;
        }
    }
}
