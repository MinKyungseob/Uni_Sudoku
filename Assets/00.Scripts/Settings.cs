using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    //Difficulty
    public enum Difficulties
    {
        DEBUG,
        EASY,
        MEDIUM,
        HARD,
        INSANE
    }

    public static Difficulties difficulty;
}
