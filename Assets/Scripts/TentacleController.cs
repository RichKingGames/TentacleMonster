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
    [SerializeField] private LayerMask _humanMask;
    [SerializeField] [Range(0,0.2f)]private float _tentacleSpeedBack;
    private Coroutine _coroutine = null;

    Animation anim;
    Animator animator;

   

    public GameObject human;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == _humanMask)
        {
            TentacleMoveBack(collision.gameObject);
        }

    }
    public void TentacleMoveBack(GameObject human)
    {
        StartCoroutine(TentacleCoroutineDecrease(human));
    }
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
    IEnumerator TentacleCoroutineDecrease(GameObject human)

    {

        while(human.transform.position != LevelManager.StartPoint)
        {
            SplinePoint[] points = _splineComputer.GetPoints();
            SplinePoint[] newPoints = new SplinePoint[_splineComputer.pointCount-2];
            for(int i =0; i<newPoints.Length; i++)
            {
                newPoints[i] = points[i];
            }

            human.transform.position = _splineComputer.GetPointPosition(_splineComputer.pointCount-1);
            _splineComputer.SetPoints(newPoints);
            
            if(Vector3.Distance(human.transform.position, LevelManager.StartPoint) < 0.5f)
            {
                Destroy(human);
                StopAllCoroutines();
            }
            yield return new WaitForSeconds(_tentacleSpeedBack);
        }
        
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
            if(i==movePositions.Count-1)
            {
                if (Vector3.Distance(movePositions[i], human.transform.position) < 1f)
                {
                    StopAllCoroutines();
                    TentacleMoveBack(human);
                }
            }
            
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
