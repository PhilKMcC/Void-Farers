using UnityEngine;

public class CameraControl : MonoBehaviour
{
    /*
     * Class Explanation:
     * This is attatched to the main camera.
     * it makes the camera follow the target, at the given offset.
     * target will usually be player or ship.
     */

    private GameObject target;
    private Vector3 offset;
    private float size;
    public static CameraControl camControl;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (camControl == null)
        {
            camControl = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + offset;
    }

    private void changeTarget(GameObject target, Vector3 offset)
    {
        this.target = target;
        this.offset = offset;
    }

    public static void changeTarget(GameObject target, Vector2 offset)
    {
        Vector3 offsetNew = new Vector3(offset.x, offset.y, -10);
        camControl.changeTarget(target, offsetNew);
    }
}
