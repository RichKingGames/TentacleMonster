using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HumanState
{
    Idle,
    RunningOut,
    Caught
}
public class Human : MonoBehaviour
{
    [SerializeField] private FieldOfView _fieldOfView;

    [SerializeField] private Vector3 _startPosition;
    [HideInInspector] public string PrefabPath { get; }
    public HumanState State { get; private set; }
    
    
    public void Start()
    {
        this.SetState(HumanState.Idle);
        transform.position = _startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetState(HumanState state)
    {
        State = state;
        Debug.Log(gameObject.name + " State is: " + State.ToString());
    }
}

