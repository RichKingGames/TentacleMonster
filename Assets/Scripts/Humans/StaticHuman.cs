using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class of human which standing and rotate
/// </summary>
public class StaticHuman : Human
{
    [SerializeField] private float[] _rotateAngles;
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _waitingTime;
    [SerializeField] private float _nextAngle;
    private float _timer;
    void Start()
    {
        _timer = _waitingTime;
        _rotateAngles = GetRandomAngles();
    }

    void FixedUpdate()
    {
        if (base.State == HumanState.Idle)
        {
            if (_timer <= 0)
            {
                _timer = _waitingTime;
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, (Mathf.PingPong(Time.time * _speed, _nextAngle) - 45), 0);
                _timer -= Time.deltaTime;
            }
        }
       
        else if (base.State == HumanState.RunningOut)
        {
            RunToExit();
        }
    }

    public float[] GetRandomAngles()
    {
        float[] angles = new float[Random.Range(2, 5)];
        for(int i = 0; i< angles.Length; i++)
        {
            angles[i] = Random.Range(-180, 180);
        }
        return angles;
    }
}
