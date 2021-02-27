using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHuman : Human
{
    [SerializeField] private float[] _rotateAngles;
    [SerializeField] private float _speed;
    [SerializeField] private float _waitingTime;
    [SerializeField] private float _nextAngle;
    private float _timer;
    void Start()
    {
        base.Start();
        _timer = _waitingTime;
        _rotateAngles = GetRandomAngles();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.rotation.y != _nextAngle)
        {
            transform.localEulerAngles += new Vector3(0, 1, 0);
        }

        //else if (transform.rotation.y - _nextAngle <=5f)
        //{
        //    if(_timer<=0)
        //    {
        //        _nextAngle = _rotateAngles[Random.Range(0, _rotateAngles.Length)];
        //        _timer = _waitingTime;
        //    }
        //    else
        //    {
        //        _timer -= Time.deltaTime;
        //    }
        //}
        //(transform.position, _nextPoint, _speed * Time.deltaTime);
        //if (transform.position == _nextPoint)
        //{
        //    if (_timer <= 0)
        //    {
        //        _nextPoint = _movePoints[Random.Range(0, _movePoints.Length)];
        //        _timer = _waitingTime;
        //    }
        //    else
        //    {
        //        _timer -= Time.deltaTime;
        //    }

        //}
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
