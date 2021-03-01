using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Enum that describes what human is doing at the moment.
/// </summary>
public enum HumanState
{
    Idle,
    RunningOut,
    Caught
}

/// <summary>
/// Main Class of Bot Humans
/// </summary>
public class Human : MonoBehaviour
{
    [SerializeField] private FieldOfView _fieldOfView;

    [SerializeField] private Vector3 _startPosition;

    [SerializeField] private NavMeshAgent _agent;

    [HideInInspector] public string PrefabPath { get; }

    protected HumanState State;

    public LevelManager LvlManager;

    /// <summary>
    /// The method which initializes human.
    /// </summary>
    public void Init(LevelManager levelManager)
    {
        LvlManager = levelManager;
        _agent = GetComponent<NavMeshAgent>();
        this.SetState(HumanState.Idle);
        transform.position = _startPosition;
    }

    public void SetState(HumanState state)
    {
        State = state;
    }

    public HumanState GetState()
    {
        return State;
    }

    public void RunToExit()
    {
        _agent.SetDestination(LevelManager.ExitPoint);

        if (Vector3.Distance(transform.position,LevelManager.ExitPoint) < 1f) //check whether the human has reached the exit
        {
            LvlManager.GameOver();
            Destroy(this.gameObject);
        }
    }
}

