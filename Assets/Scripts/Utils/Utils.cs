using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Test Class
/// </summary>
public class Utils
{
    //public static bool MakeLevel(int level, string fileName)
    //{
    //    Level l = null;
    //    if (level == 1)
    //    {
    //        l = MakeLevel1();
    //    }
    //    if (l == null)
    //    {
    //        return false;
    //    }
    //    l.Write(fileName);
    //    return true;
    //}
 
    private static Level MakeLevel1()
    {
        return new Level();

        //ActiveHuman[] activeHumans = new ActiveHuman[2]
        //{
        //    new ActiveHuman("Prefab/Humans/CharacterActive", new Vector3(-3, 0, 6),
        //    new Vector3[]{ new Vector3(-3,0,6), new Vector3(-2, 0, 2.5f) },
        //    2f, 5f, 2f, 90, 3f),
        //    new ActiveHuman("Prefab/Humans/CharacterActive", new Vector3(1.5f, 0, 1.5f),
        //    new Vector3[]{ new Vector3(1.5f, 0, 1.5f), new Vector3(3, 0, 5) },
        //    3f, 2f, 2.5f, 120f, 2.5f)
        //};
        //StaticHuman[] staticHumans = new StaticHuman[2]
        //{
        //    new StaticHuman("Prefab/Humans/CharacterStatic", new Vector3(-1, 0, -3.5f),
        //    2.5f, 70f, 4f),
        //    new StaticHuman("Prefab/Humans/CharacterStatic", new Vector3(2, 0, -3f),
        //    2f, 100f, 3.5f)
        //};
        //return new Level(activeHumans, staticHumans, new Environment("Room1"));
    }
    public static int GetAngleFromVector(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        int angle = Mathf.RoundToInt(n);

        return angle;
    }
    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
    public static Vector3 GetVectorFromAngle(float angle)
    {
        // angle = 0 -> 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), 0,Mathf.Sin(angleRad));
    }
}