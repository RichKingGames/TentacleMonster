using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static bool MakeLevel(int level, string fileName)
    {
        Level l = null;
        if (level == 1)
        {
            l = MakeLevel1();
        }
        if (l == null)
        {
            return false;
        }
        l.Write(fileName);
        return true;
    }
    private static Level MakeLevel1()
    {
        return new Level(new Human[] { }, new TentacleController(), new Environment("temp"));
    }
}