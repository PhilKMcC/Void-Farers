using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

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
    private float defaultDist = 10; //the distance away from the 2d region this is. negative to the z coord.
    public static CameraControl camControl;
    public TextMeshProUGUI coordsText;


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
        defaultDist = -transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(target.transform.position.x + offset.x, target.transform.position.y + offset.y,gameObject.transform.position.z);
        coordsText.text = "(" + transform.position.x.ToString("n3") + "," + transform.position.y.ToString("n3") + ")";
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


    
    public static void changeScale(float scale)
    {
        camControl.StopAllCoroutines(); //caution if have multiple diff coroutines
        camControl.StartCoroutine(camControl.gradualScale( scale));
    }
    public static void resetScale()
    {
        camControl.StopAllCoroutines(); //caution if have multiple diff coroutines
        camControl.StartCoroutine(camControl.gradualScale( camControl.defaultDist));
    }

    IEnumerator gradualScale(float become)
    {
        become = -become;
        float closeEnough = 0.1f;
        float speedOfChange = 2f;

        //go further back (zoom out)
        while (transform.position.z > become + closeEnough)
        {
            transform.position = transform.position + Vector3.back * (speedOfChange * Time.deltaTime);
            Debug.Log("zooming out");
            yield return null;
        }
        //go closer (zoom in)
        while (transform.position.z < become - closeEnough)
        {
            transform.position = transform.position + Vector3.forward * (speedOfChange * Time.deltaTime);
            Debug.Log("zooming in");
            yield return null;
        }

        //snap to
        transform.position = new Vector3(transform.position.x, transform.position.y, become);

        yield return null;
    }
}
