using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck;
using Dreamteck.Splines;

public class TentacleController : MonoBehaviour
{
    private const float START_SIZE = 0.8f;

    [SerializeField] private SplineComputer _splineComputer;
    [SerializeField] private Spline _spline;

    private Coroutine _coroutine = null;

    Animation anim;
    Animator animator;
    
    

    /// <summary>
    /// Method that Move Tentacle.
    /// </summary>
    public void TentacleMove(List<Vector3> movePositions)
    {
        //anim.transform.position
        // animator.

        //for (int i = 2; i < movePositions.Count; i++)
        //{
        //    SplinePoint point = new SplinePoint();
        //    movePositions[i] = new Vector3(movePositions[i].x, 0.2f, movePositions[i].z);

        //    point.position = movePositions[i];
        //    point.size = ((float)(movePositions.Count - i) / movePositions.Count) * START_SIZE;
        //    _splineComputer.SetPoint(i, point);
        //}
        //if (_coroutine == null)
        //{
            _coroutine = StartCoroutine(TentacleCoroutineGrowing(movePositions)); ;
        //}
        
    }
    IEnumerator TentacleCoroutineGrowing(List<Vector3> movePositions)
    {
        int i = 2;
        while(i!= movePositions.Count)
        {
            SplinePoint point = new SplinePoint();
            movePositions[i] = new Vector3(movePositions[i].x, 0.2f, movePositions[i].z);

            point.position = movePositions[i];
            point.size = ((float)(movePositions.Count - i) / movePositions.Count) * START_SIZE;
            _splineComputer.SetPoint(i, point);
            i++;
            yield return new WaitForFixedUpdate();
        }
        //_coroutine = null;

    }
    public void StopCoroutine()
    {
        if (_coroutine == null) return;
        StopCoroutine(_coroutine);
        _coroutine = null;
    }

}
