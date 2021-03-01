using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class of human which walking at map
/// </summary>
public class ActiveHuman : Human
{
    [SerializeField] private Vector3[] _movePoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _waitingTime;

    private Vector3 _nextPoint;

    private float _timer;
    
    void Start()
    {
        _nextPoint = _movePoints[1];
        _timer = _waitingTime;
    }

    void FixedUpdate()
    {
        if(base.State == HumanState.Idle)
        {
            MovePosition();
        }
        else if(base.State == HumanState.RunningOut)
        {
            RunToExit();
        }
    }
    /// <summary>
    /// Method that Move ActiveHuman.
    /// </summary>
    void MovePosition()
    {
        transform.LookAt(_nextPoint);
        transform.position = Vector3.MoveTowards(transform.position, _nextPoint, _speed * Time.deltaTime);
        if (Vector3.Distance(transform.position,_nextPoint) < .05f)
        {
            if (_timer <= 0)
            {
                _nextPoint = _movePoints[Random.Range(0, _movePoints.Length)];
                _timer = _waitingTime;
            }
            else
            {
                _timer -= Time.deltaTime;
            }

        }
    }
}
