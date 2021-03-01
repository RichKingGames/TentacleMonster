using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for drawing line by touch.
/// </summary>
public class LineController : MonoBehaviour
{

    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private GameObject _currentLine;

    [SerializeField] private TentacleController _tentacle;

    private LineRenderer _lineRenderer;
    private static List<Vector3> _fingerPositions;

    private Plane _plane;

    public GameObject human;
    void Start()
    {
         _fingerPositions = new List<Vector3>();
    }
    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        if(Input.GetMouseButton(0))
        {
            Vector3 tempFingerPos = RayPosition(_fingerPositions[_fingerPositions.Count-1]);
            if (Vector3.Distance(tempFingerPos,_fingerPositions[_fingerPositions.Count -1]) > .1f) // Checking if the finger has moved
            {
                UpdateLine(tempFingerPos);
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(_currentLine);
        }
    }

    bool CanCreateLine(Vector3 tentaclePos, Vector3 mouseClickPos)
    {
        return Vector3.Distance(tentaclePos, mouseClickPos) < 1f;
    }

    /// <summary>
    /// Method that creates the starting point of the line.
    /// </summary>
    void CreateLine()
    {
        Vector3 pos = RayPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if(CanCreateLine(LevelManager.StartPoint, pos)) // Checking if finger at start position
        {
            _currentLine = Instantiate(_linePrefab, pos, Quaternion.identity);
            _currentLine.transform.position = pos;
            _lineRenderer = _currentLine.GetComponent<LineRenderer>();
            _fingerPositions = new List<Vector3>();
            _fingerPositions.Add(LevelManager.StartPoint);
            _fingerPositions.Add(pos);
            _lineRenderer.SetPosition(0, _fingerPositions[0]);
            _lineRenderer.SetPosition(1, _fingerPositions[1]);
            _tentacle.NewAction(_fingerPositions[1]);
        }
        else if(CanCreateLine(_fingerPositions[_fingerPositions.Count-1],pos)) // Checking if finger at the end of tentacle
        {
            _currentLine = Instantiate(_linePrefab, pos, Quaternion.identity);
            _currentLine.transform.position = pos;
            _lineRenderer = _currentLine.GetComponent<LineRenderer>();
            _fingerPositions.Add(pos);

            _lineRenderer.SetPosition(0, _fingerPositions[_fingerPositions.Count-1]);
            _lineRenderer.SetPosition(1, _fingerPositions[_fingerPositions.Count - 1]);
        }
        else
        {
            //TODO Failed Sound/Animation
        }

    }

    /// <summary>
    /// Method that returns Vector3 from a touch of your finger/mouse by using Raycast.
    /// </summary>
    Vector3 RayPosition(Vector3 pos)
    {
        _plane = new Plane(Vector3.up, transform.position);
      
        Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance;
        if (_plane.Raycast(mRay, out rayDistance))
        {     
            pos = mRay.GetPoint(rayDistance);
            pos.y = 0.1f;
        }
        return pos;
    }

    /// <summary>
    /// Method invokes when finger moved.  Drawing line and calls the TentacleController.TentacleMove() method.
    /// </summary>
    void UpdateLine(Vector3 newFingerPos) 
    {
        _fingerPositions.Add(newFingerPos);
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount-1, newFingerPos);

        _tentacle.TentacleMove(_fingerPositions);

        //_tentacle.TentacleMove(newFingerPos,_lineRenderer.positionCount-1);
    }

    public static List<Vector3> GetPositions()
    {
        return _fingerPositions;
    }
        
}
