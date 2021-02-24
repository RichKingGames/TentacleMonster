using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    private string _prefabPath { get; }
    public GameObject StartPoint;
    public Environment(string prefabPath)
    {
        _prefabPath = prefabPath;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
