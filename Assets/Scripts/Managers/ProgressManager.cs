using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static JsonLibrary Json;
    void Awake()
    {
        Json = new JsonLibrary(Application.persistentDataPath);
        Json.Read();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
