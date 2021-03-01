using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

/// <summary>
/// Class which contain information about level.
/// </summary>
public class Level : MonoBehaviour
{
    public List<ActiveHuman> ActiveHumans;
    public List<StaticHuman> StaticHumans;
    public Environment RoomEnvironment;
    public static int HumansCount;
    
    void Start()
    {
        HumansCount = ActiveHumans.Count + StaticHumans.Count;
    }
    

}
