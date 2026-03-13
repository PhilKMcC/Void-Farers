using UnityEngine;

public class Description : MonoBehaviour
{
    /*
     * Class Explanation:
     * this just exists so we can attach it to objects that need a little bit more explanation.
     * no functional purpose.
     */
    [TextArea(4,10)]
    public string words;
}
