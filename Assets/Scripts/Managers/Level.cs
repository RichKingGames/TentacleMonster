using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class Level : MonoBehaviour
{
    public Human[] Humans { get; }
    public TentacleController Tentacle { get; }
    public Environment RoomEnvironment { get; }
    public Level(Human[] humans, TentacleController tentacle, Environment environment)
    {
        Humans = humans;
        Tentacle = tentacle;
        RoomEnvironment = environment;
    }


    private static JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.Objects,
        Formatting = Formatting.Indented
    };

    public void Write(string jsonFile)
    {
        string jsonString = JsonConvert.SerializeObject(this, Settings);
        File.WriteAllText(jsonFile, jsonString);
    }

    public static Level Read(string jsonFile)
    {
        string jsonStringOutput = File.ReadAllText(jsonFile);
        return JsonConvert.DeserializeObject<Level>(jsonStringOutput, Settings);
    }

}
