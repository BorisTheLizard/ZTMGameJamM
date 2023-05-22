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
	private Vector3 prevPosition;

	//death logic
	HealthSystem health;

	//attack
	public bool isMele = false;
	float attackTime;
	public float MaxAttackTime;
	[SerializeField] GameObject projectile;
	[SerializeField] Transform shootingPoint;
	[SerializeField] float forcePower = 100f;
	public bool canShoot = true;
	[SerializeField] GameObject hitCollider;

	//animator
	[SerializeField] Animator anim;
	public float CCSpeed;
	[SerializeField] Vector3 lastPosition;

	//targeting
	private Vector3 empTransform;
	private Transform empTolookAT;
	public float distanceDebug;


	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		prevPosition = transform.position;
		fov = GetComponent<fieldOfView>();
		health = GetComponent<HealthSystem>();
		empTolookAT = GameObject.FindGameObjectWithTag("EMP").transform;
	}
	private void OnDisable()
	{
		currentState = "IDLE";
	}
	private void OnEnable()
	{
		currentState = "IDLE";
	}

	private void Update()
	{
		speedCounter();
		if (health.dead)
		{
			//explosionEffect
			this.gameObject.SetActive(false);
			health.Health = health.MaxHealth;
			health.dead = false;
		}

		if (currentState == "IDLE")
		{
			rotationTowardsDirection();
			float distance = Vector3.Distance(agent.transform.position, empTransform);
			distanceDebug = distance;

			if (distance < 3)
			{
				currentState = "AttackEMP";
			}
		}
		else if (currentState == "Attack")
		{

		}
		else if (currentState == "AttackEMP")
		{
			transform.LookAt(empTolookAT);
			attack();
		}
	}
	void attack()
	{
		if (Time.time > attackTime)
		{
			attackTime = Time.time + MaxAttackTime;
			if (isMele)
			{
				//PlayAnim to On collider
				hitCollider.SetActive(true);
			}
			else
			{
				//shooting anim and methode
			}
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
	public void GoToPoint(Vector3 position)
	{
		empTransform = position;
		agent.SetDestination(position);
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
