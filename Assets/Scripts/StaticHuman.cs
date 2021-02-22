using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHuman : Human
{
    
    void Start()
    {
        this.SetState(HumanState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
