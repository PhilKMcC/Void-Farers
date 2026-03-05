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
    public Vector3 offset;
    private float size;
    public static CameraControl camControl;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (camControl == null)
        {
            camControl = this;
        }
        if(target == null)
        {
            //initialization funky
            target = GameObject.FindGameObjectWithTag("Player");
            changeTarget(target, target.GetComponent<PlayerScript>().getCameraPlayerOffset());

        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = target.transform.position + offset;
    }

    private void changeTargetlocal(GameObject targetp, Vector3 offsetp)
    {
        this.target = targetp;
        this.offset = offsetp;
        Debug.Log("target updated: " + this.target + "; offset: " + offset);
    }

    public static void changeTarget(GameObject targetp, Vector2 offsetp)
    {
        Vector3 offsetNew = new Vector3(offsetp.x, offsetp.y, -10);
        camControl.changeTargetlocal(targetp, offsetNew);
    }

    public static void changeTarget(GameObject targetp, Vector3 offsetp)
    {
        Vector3 offsetNew = new Vector3(offsetp.x, offsetp.y, -10);
        camControl.changeTargetlocal(targetp, offsetNew);
    }
}
