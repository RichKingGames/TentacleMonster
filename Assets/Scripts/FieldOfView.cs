using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldOfView : MonoBehaviour
{

	[SerializeField][Range(0, 5)] private float _viewRadius;
	[SerializeField][Range(0, 360)] private float _viewAngle;

	[SerializeField] private LayerMask _targetMask;
	[SerializeField] private LayerMask _obstacleMask;

	[SerializeField] private Human currentHuman;

	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

	public float meshResolution;
	public int edgeResolveIterations;
	public float edgeDstThreshold;

	public MeshFilter viewMeshFilter;
	Mesh viewMesh;

	void Start()
	{
		viewMesh = new Mesh();
		viewMesh.name = "View Mesh";
		viewMeshFilter.mesh = viewMesh;

		//StartCoroutine(FindTargetsWithDelay( .2f));
	}
	private void FixedUpdate()
	{
		if(currentHuman.State==HumanState.Idle)
		{
			FindVisibleTargets();
			DrawFieldOfView();
		}
		else if(currentHuman.State == HumanState.RunningOut)
		{

		}
	}

	IEnumerator FindTargetsWithDelay(float delay)
	{
		while (true)
		{
			yield return new WaitForSeconds(delay);
			FindVisibleTargets();
		}
	}

	//void LateUpdate()
	//{
	//	//DrawFieldOfView();
	//}

	void FindVisibleTargets()
	{
		visibleTargets.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius, _targetMask);
		List<Vector3> tentaclePositions = LineController.GetPositions();

		if(tentaclePositions!= null && targetsInViewRadius!=null)
		{
			for (int i = 0; i < tentaclePositions.Count-1; i++)
			{
				Vector3 target = tentaclePositions[i];//targetsInViewRadius[i].transform;
				Vector3 dirToTarget = (target - transform.position).normalized;
				if (Vector3.Angle(transform.forward, dirToTarget) < _viewAngle / 2)
				{
					float dstToTarget = Vector3.Distance(transform.position, target);
					if (Physics.Raycast(transform.position, dirToTarget, _viewRadius, _targetMask))
					{
						targetsInViewRadius[i].gameObject.GetComponent<TentacleController>().TentacleMoveBack(this.gameObject);
						Debug.Log("COLLISION");
						currentHuman.SetState(HumanState.RunningOut);
						//visibleTargets.Add(target);
					}
				}
			}
		}
		
	}

	Vector3 ClosestPosition()
	{
		
		return new Vector3();
	}
	void DrawFieldOfView()
	{
		int stepCount = Mathf.RoundToInt(_viewAngle * meshResolution);
		float stepAngleSize = _viewAngle / stepCount;
		List<Vector3> viewPoints = new List<Vector3>();
		ViewCastInfo oldViewCast = new ViewCastInfo();
		for (int i = 0; i <= stepCount; i++)
		{
			float angle = transform.eulerAngles.y - _viewAngle / 2 + stepAngleSize * i;
			ViewCastInfo newViewCast = ViewCast(angle);

			if (i > 0)
			{
				bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
				if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
				{
					EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
					if (edge.pointA != Vector3.zero)
					{
						viewPoints.Add(edge.pointA);
					}
					if (edge.pointB != Vector3.zero)
					{
						viewPoints.Add(edge.pointB);
					}
				}

			}


			viewPoints.Add(newViewCast.point);
			oldViewCast = newViewCast;
		}

		int vertexCount = viewPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount - 2) * 3];

		vertices[0] = Vector3.zero;
		for (int i = 0; i < vertexCount - 1; i++)
		{
			vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

			if (i < vertexCount - 2)
			{
				triangles[i * 3] = 0;
				triangles[i * 3 + 1] = i + 1;
				triangles[i * 3 + 2] = i + 2;
			}
		}

		viewMesh.Clear();

		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateNormals();
	}


	EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
	{
		float minAngle = minViewCast.angle;
		float maxAngle = maxViewCast.angle;
		Vector3 minPoint = Vector3.zero;
		Vector3 maxPoint = Vector3.zero;

		for (int i = 0; i < edgeResolveIterations; i++)
		{
			float angle = (minAngle + maxAngle) / 2;
			ViewCastInfo newViewCast = ViewCast(angle);

			bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
			if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
			{
				minAngle = angle;
				minPoint = newViewCast.point;
			}
			else
			{
				maxAngle = angle;
				maxPoint = newViewCast.point;
			}
		}

		return new EdgeInfo(minPoint, maxPoint);
	}


	ViewCastInfo ViewCast(float globalAngle)
	{
		Vector3 dir = DirFromAngle(globalAngle, true);
		RaycastHit hit;

		if (Physics.Raycast(transform.position, dir, out hit, _viewRadius, _obstacleMask))
		{
			return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
		}
		else
		{
			return new ViewCastInfo(false, transform.position + dir * _viewRadius, _viewRadius, globalAngle);
		}
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

	public struct ViewCastInfo
	{
		public bool hit;
		public Vector3 point;
		public float dst;
		public float angle;

		public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
		{
			hit = _hit;
			point = _point;
			dst = _dst;
			angle = _angle;
		}
	}

	public struct EdgeInfo
	{
		public Vector3 pointA;
		public Vector3 pointB;

		public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
		{
			pointA = _pointA;
			pointB = _pointB;
		}
	}

}
