using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level CurrentLevel;
    public static Vector3 StartPoint = new Vector3(-3,0,-3);
    void Start()
    {
        Utils.MakeLevel(1, Path.Combine(Application.persistentDataPath, "level2.json"));
        //CurrentLevel = Level.Read(Path.Combine(Application.persistentDataPath, "level1json"));
    }
    
}
