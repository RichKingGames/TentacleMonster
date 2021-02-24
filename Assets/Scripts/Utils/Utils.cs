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