using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class Level : MonoBehaviour
{
    public List<ActiveHuman> ActiveHumans;
    public List<StaticHuman> StaticHumans;
    public Environment RoomEnvironment;
    //public Level(ActiveHuman[] activeHumans, StaticHuman[] staticHumans, Environment environment)
    //{
    //    ActiveHumans = activeHumans;
    //    StaticHumans = staticHumans;
    //    RoomEnvironment = environment;
    //}


    //private static JsonSerializerSettings Settings = new JsonSerializerSettings
    //{
    //    TypeNameHandling = TypeNameHandling.Objects,
    //    Formatting = Formatting.Indented
    //};

    //public void Write(string jsonFile)
    //{
    //    string jsonString = JsonConvert.SerializeObject(this, Settings);
    //    File.WriteAllText(jsonFile, jsonString);
    //}

    //public static Level Read(string jsonFile)
    //{
    //    string jsonStringOutput = File.ReadAllText(jsonFile);
    //    return JsonConvert.DeserializeObject<Level>(jsonStringOutput, Settings);
    //}

}
