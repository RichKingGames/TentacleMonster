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
    private List<Vector3> _fingerPositions;

    private Plane _plane;
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
            if (Vector3.Distance(tempFingerPos,_fingerPositions[_fingerPositions.Count -1]) > .1f)
            {
                UpdateLine(tempFingerPos);
            }
            else if(Vector3.Distance(_fingerPositions[_fingerPositions.Count - 1], _fingerPositions[_fingerPositions.Count - 2]) < .1f)
            {
                
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _tentacle.TentacleMove(_fingerPositions);
        }

    }

    /// <summary>
    /// Method that creates the starting point of the line.
    /// </summary>
    void CreateLine()
    {
        Vector3 pos = RayPosition(new Vector3());
        _currentLine = Instantiate(_linePrefab, pos, Quaternion.identity);
        _lineRenderer = _currentLine.GetComponent<LineRenderer>();
        _fingerPositions.Clear();
        _fingerPositions.Add(pos);
        _fingerPositions.Add(pos);
        _lineRenderer.SetPosition(0, _fingerPositions[0]);
        _lineRenderer.SetPosition(1, _fingerPositions[1]);
        
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
    /// Method that draws a line and calls the TentacleController.TentacleMove() method.
    /// </summary>
    void UpdateLine(Vector3 newFingerPos)
    {
        _fingerPositions.Add(newFingerPos);
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount-1, newFingerPos);

        //_tentacle.TentacleMove(_fingerPositions);

        //_tentacle.TentacleMove(newFingerPos,_lineRenderer.positionCount-1);
    }
}
