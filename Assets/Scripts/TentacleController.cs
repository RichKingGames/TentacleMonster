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


    private LevelManager _levelManager;
    private GameObject[] _humans;

    public void Init(LevelManager levelManager, List<ActiveHuman> activeHumans, List<StaticHuman> staticHumans, Vector3 startPosition)
    {
        transform.position = startPosition;
        SetLevelManager(levelManager);
        SetHumans(activeHumans, staticHumans);
    }

    public void SetLevelManager(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }
    public void SetHumans(List<ActiveHuman> activeHumans, List<StaticHuman> staticHumans)
    {
        _humans = new GameObject[activeHumans.Count + staticHumans.Count];
        for (int i = 0; i < activeHumans.Count; i++)
        {
            _humans[i] = activeHumans[i].gameObject;
        }
        for(int i=activeHumans.Count; i< (staticHumans.Count + activeHumans.Count); i++)
        {
            _humans[i] = staticHumans[i - activeHumans.Count].gameObject;
        }
       
    }
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
                _levelManager.DestroyHumans(human);
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
                for(int j = 0; j< _humans.Length; j++)
                {
                    if (Vector3.Distance(movePositions[i], _humans[j].transform.position) < 1f)
                    {
                        StopAllCoroutines();
                        _humans[j].gameObject.GetComponent<Human>().SetState(HumanState.Caught);
                        TentacleMoveBack(_humans[j]);
                    }
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
