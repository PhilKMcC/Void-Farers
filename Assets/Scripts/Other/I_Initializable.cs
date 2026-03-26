using UnityEngine;
using System.Collections.Generic;


public interface I_Initializable
{
    /*
     * Class (interface) Explanation:
     * for some reason, the input system doesn't like to have things start disabled
     * so, the stuff that needs to start disabled will be called by the init method on frame 2, not 1.
     * hopefully this makes them start disabled.
     * unfortunately, the way this works each initializable has to add itself to initials.
     */
    public static List<I_Initializable> initials = new List<I_Initializable>();
    public abstract void init();

    public static void initAll()
    {
        foreach (I_Initializable initial in initials)
        {
            initial.init();
        }

        initials.Clear();
    
    }
}
