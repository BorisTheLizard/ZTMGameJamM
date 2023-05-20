using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fieldOfView : MonoBehaviour
{
	public float viewRadius;
	[Range(0,360)] public float viewAngle;
	public List<Transform> visibleTargets = new List<Transform>();
	public LayerMask obstacleLayer;
	public LayerMask enemyLayer;
	GameObject self;
	public bool seeEnemy = false;
	RaycastHit hit;
	public Transform SeeTarget;

	private void Awake()
	{
		self = this.gameObject;
	}

	private void Start()
	{
		StartCoroutine(findTargetsWithDelay(.2f));
	}

	IEnumerator findTargetsWithDelay(float delay)
	{
		while (true)
		{
			yield return new WaitForSeconds(delay);
			findVisibleTargets();
		}
	}
	void findVisibleTargets()
	{
		visibleTargets.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, enemyLayer);

		for(int i=0; i< targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;

			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				float distToTarget = Vector3.Distance(transform.position, target.position);

				if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleLayer) && target.gameObject!=self)
				{
					visibleTargets.Add(target);
					

					if (visibleTargets.Contains(target))
					{
						seeEnemy = true;
						SeeTarget = target;
					}
				}
			}
			if (!visibleTargets.Contains(target))
			{
				seeEnemy = false;
				SeeTarget = null;
			}
		}
	}

	public Vector3 dirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

}
