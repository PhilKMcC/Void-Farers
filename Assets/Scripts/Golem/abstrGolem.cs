using UnityEngine;

public abstract class abstrGolem : MonoBehaviour
{
    public static float state;
    public static bool wait;
    //0 = Asleep
    //1 = Waiting
    //2 = Launch Arm
    //3 = Release Kamikazes
    //4 = Amethyst Toss
    //5 = Amethyst Beam
    //6 = Dying
    //7 = Resetting
}
