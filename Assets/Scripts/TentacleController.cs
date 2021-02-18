using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck;
using Dreamteck.Splines;

public class TentacleController : MonoBehaviour
{
    [SerializeField] private SplineComputer _splineComputer;
    [SerializeField] private Spline _spline;

    /// <summary>
    /// Method that Move Tentacle.
    /// </summary>
    public void TentacleMove(Vector3 movePositions,int index)
    {

        SplinePoint point = new SplinePoint();
        point.position = movePositions;
        point.size = 0.2f;
        _splineComputer.SetPoint(index, point);
        
    }

}
