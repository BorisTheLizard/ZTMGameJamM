using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{
	NavMeshAgent agent;
	public float agentSpeed = 4.5f;
	public string currentState = "IDLE";
	fieldOfView fov;
	public float SearchRadius = 20f;

	public bool ReachedPoint = false;
	[SerializeField] POI poi;

	public Vector3 currentDestination;
	private Vector3 prevPosition;

	//weapon
	[SerializeField] int maxBallsInclip = 5;
	[SerializeField] int ballsInClip = 0;

	//death logic
	HealthSystem health;

	//attack
	float attackTime;
	public float MaxAttackTime;
	[SerializeField] GameObject projectile;
	[SerializeField] Transform shootingPoint;
	[SerializeField] float forcePower = 100f;
	public bool canShoot = true;

	//animator
	[SerializeField] Animator anim;
	public float CCSpeed;
	[SerializeField] Vector3 lastPosition;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		prevPosition = transform.position;
		attackTime = MaxAttackTime;
		fov = GetComponent<fieldOfView>();
		health = GetComponent<HealthSystem>();
		currentDestination = GameObject.FindWithTag("EMP").transform.position;
		agent.SetDestination(currentDestination);
	}

	private void Update()
	{
		speedCounter();

		if (currentState == "IDLE")
		{
			rotationTowardsDirection();
			float distance = Vector3.Distance(transform.position, currentDestination);
		}
		else if (currentState == "Attack")
		{

		}
	}

	void rotationTowardsDirection()
	{
		Vector3 deltaPosition = transform.position - prevPosition;
		if (deltaPosition != Vector3.zero)
		{
			transform.forward = deltaPosition;
		}
		prevPosition = transform.position;
	}


	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, SearchRadius);
	}
	public void speedCounter()
	{
		CCSpeed = Mathf.Lerp(CCSpeed, (transform.position - lastPosition).magnitude / Time.deltaTime, 0.75f);
		lastPosition = transform.position;
	}






/*	public void AnimatorController()
	{
		if (CCSpeed < 0.5f)
		{
			anim.SetBool("walk", false);
		}
		else
		{
			anim.SetBool("walk", !false);
		}
	}*/
}
