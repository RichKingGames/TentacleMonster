using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck;
using Dreamteck.Splines;


/// <summary>
/// The class that move the tentacle
/// </summary>
public class TentacleController : MonoBehaviour
{
    private const float START_SIZE = 0.8f;

    [SerializeField] private SplineComputer _splineComputer;
    [SerializeField] private Spline _spline;
    [SerializeField] private LayerMask _humanMask;

    [SerializeField] [Range(0,0.2f)]private float _tentacleSpeedBack;

    private Coroutine _coroutine = null;

    private LevelManager _levelManager;
    private GameObject[] _humans;

    private int _humansKilled;   

    public void Init(LevelManager levelManager, List<ActiveHuman> activeHumans, List<StaticHuman> staticHumans, Vector3 startPosition)
    {
        transform.position = startPosition;
        SetLevelManager(levelManager);
        SetHumans(activeHumans, staticHumans);
        SetStartPoint();
        _humansKilled = 0;
    }

    /// <summary>
    /// Method that refresh tentacle spline to start position
    /// </summary>
    void SetStartPoint()
    {
        SplinePoint[] newPoints = new SplinePoint[2];
        newPoints[0].SetPosition(LevelManager.StartPoint);
        newPoints[0].size = START_SIZE;
        newPoints[1].SetPosition( new Vector3(LevelManager.StartPoint.x,
            LevelManager.StartPoint.y, LevelManager.StartPoint.z + 0.1f));
        newPoints[1].size = START_SIZE-0.1f;

        _splineComputer.SetPoints(newPoints); // Erase all Point, and add new.
    }

    /// <summary>
    /// Method that invokes when Line Drawing
    /// </summary>
    public void NewAction(Vector3 secondPosition)
    {
        SplinePoint point = new SplinePoint();
        point.position = LevelManager.StartPoint;
        point.size = START_SIZE;
        _splineComputer.SetPoint(0, point);

        point = new SplinePoint();
        secondPosition = new Vector3(secondPosition.x, 0.2f, secondPosition.z);
        point.position = secondPosition;
        point.size = START_SIZE;
        _splineComputer.SetPoint(1, point);
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



    /// <summary>
    /// Method that Move Tentacle.
    /// </summary>
    public void TentacleMove(List<Vector3> movePositions)
    {
        for (int i = 2; i < movePositions.Count; i++)
        {
            SplinePoint point = new SplinePoint();
            movePositions[i] = new Vector3(movePositions[i].x, 0.2f, movePositions[i].z);

            point.position = movePositions[i];
            point.size = ((float)(movePositions.Count - i) / movePositions.Count) * START_SIZE;
            _splineComputer.SetPoint(i, point);
        }
        for (int j = 0; j < _humans.Length; j++)
        {
            if (Vector3.Distance(movePositions[movePositions.Count-1], _humans[j].transform.position) <0.5f)
            {
                _humans[j].gameObject.GetComponent<Human>().SetState(HumanState.Caught);
                TentacleMoveBack(_humans[j]);
            }
        }
    }

    /// <summary>
    /// Method invokes when tentacle catch human
    /// </summary>
    public void TentacleMoveBack(GameObject human)
    {
        StartCoroutine(TentacleCoroutineDecrease(human));
    }

    /// <summary>
    /// Coroutine invokes when tentacle catch human
    /// </summary>
    IEnumerator TentacleCoroutineDecrease(GameObject human)

    {

        while(human.transform.position != LevelManager.StartPoint)
        {
            SplinePoint[] points = _splineComputer.GetPoints();
            SplinePoint[] newPoints = new SplinePoint[_splineComputer.pointCount-1];
            Vector3 humanPoint =new Vector3();
            for(int i =0; i<newPoints.Length; i++)
            {
                newPoints[i] = points[i];
                if(i == newPoints.Length-1)
                {
                    humanPoint = (newPoints[i].position);
                }
                
            }

            human.transform.position = humanPoint;
            _splineComputer.SetPoints(newPoints);

            if (Vector3.Distance(human.transform.position, LevelManager.StartPoint) < 1f)
            {
                DeathVFX();
                _levelManager.DestroyHumans(human);
                Destroy(human);
                SetStartPoint();
                StopAllCoroutines();
                _humansKilled++;
                if(_humansKilled == Level.HumansCount)
                {
                    _levelManager.MissionComplete();
                }
            }
            yield return new WaitForSeconds(_tentacleSpeedBack);
        }
        
    }

    void DeathVFX()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Player/Death"), LevelManager.StartPoint, new Quaternion());
    }

    //IEnumerator TentacleCoroutineGrowing(List<Vector3> movePositions)
    //{
    //    int i = 2;
    //    while(i!= movePositions.Count)
    //    {
    //        SplinePoint point = new SplinePoint();
    //        movePositions[i] = new Vector3(movePositions[i].x, 0.2f, movePositions[i].z);

    //        point.position = movePositions[i];
    //        point.size = ((float)(movePositions.Count - i) / movePositions.Count) * START_SIZE;
    //        _splineComputer.SetPoint(i, point);
    //        i++;
    //        if(i==movePositions.Count-1)
    //        {
    //            for(int j = 0; j< _humans.Length; j++)
    //            {
    //                if (Vector3.Distance(movePositions[i], _humans[j].transform.position) < 1f)
    //                {
    //                    StopAllCoroutines();
    //                    _humans[j].gameObject.GetComponent<Human>().SetState(HumanState.Caught);
    //                    TentacleMoveBack(_humans[j]);
    //                }
    //            }

    //        }

    //        yield return new WaitForFixedUpdate();
    //    }

    //    //_coroutine = null;

    //}
    //public void StopCoroutine()
    //{
    //    if (_coroutine == null) return;
    //    StopCoroutine(_coroutine);
    //    _coroutine = null;
    //}

}
