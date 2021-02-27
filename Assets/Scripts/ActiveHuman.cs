using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveHuman : Human
{
    [SerializeField] private Vector3[] _movePoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _waitingTime;
    private Vector3 _nextPoint;
    private float _timer;
    //public ActiveHuman(string prefabPath, Vector3 startPosition, 
    //    Vector3[] movePoints, float speed, float waitingTime, float viewRadius, float viewAngle, float screamingRadius) 
    //    : base(prefabPath,startPosition,viewRadius,viewAngle,screamingRadius)
    //{
    //    _movePoints = movePoints;
    //    _speed = speed;
    //    _waitingTime = waitingTime;
    //}
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        _nextPoint = _movePoints[1];
        _timer = _waitingTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePosition();
    }
    void MovePosition()
    {
        transform.LookAt(_nextPoint) ;
        transform.position = Vector3.MoveTowards(transform.position, _nextPoint, _speed * Time.deltaTime);
        if (transform.position == _nextPoint)
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
