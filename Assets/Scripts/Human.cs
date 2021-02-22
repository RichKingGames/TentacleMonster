using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HumanState
{
    Idle,
    RunningOut
}
public class Human : MonoBehaviour
{
    public HumanState State { get; private set; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetState(HumanState state)
    {
        State = state;
    }
}

