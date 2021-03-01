using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class which Controlling progress.
/// </summary>
public class ProgressManager : MonoBehaviour
{
    public static JsonLibrary Json;
    void Awake()
    {
        Json = new JsonLibrary(Application.persistentDataPath);
        Json.Read();
    }

}
