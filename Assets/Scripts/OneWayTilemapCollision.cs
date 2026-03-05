using UnityEngine;

public class OneWayTilemapCollision : MonoBehaviour
{
    /*
     * Class Explanation:
     * This is attached to the tilemap colliders that want to be able to let the character out after they enter
     */


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("collision stay");
    }
}
