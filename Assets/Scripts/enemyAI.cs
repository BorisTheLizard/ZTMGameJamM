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

	public bool ReachedPoint = false;
	[SerializeField] POI poi;

	public Vector3 currentDestination;
	private Vector3 prevPosition;


	//weapon
	[SerializeField] int maxBallsInclip = 5;
	[SerializeField] int ballsInClip = 0;
	//public bool hasBall;

	//FindBall logic
	public bool goingToGetOne=false;
	public bool grabbingBall = false;
	[SerializeField] float SearchRadius = 10f;
	[SerializeField] float grabBallTime = 1f;
	[SerializeField] LayerMask snowSourceLayer;
	private Transform nearestSnowSource;
	private float nearestDistance = float.MaxValue;

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
		goToNextPoint();
		attackTime = MaxAttackTime;
		fov = GetComponent<fieldOfView>();
		health = GetComponent<HealthSystem>();
	}

	private void Update()
	{
		//AnimatorController();
		speedCounter();

		if (currentState == "IDLE")
		{
			rotationTowardsDirection();
			float distance = Vector3.Distance(transform.position, currentDestination);

			if (distance < 2)
			{
				goToNextPoint();
			}

			/*			if (ballsInClip>0)
						{
							if (fov.seeEnemy)
							{
								currentState = "Attack";
							}

							if (distance < 2)
							{
								goToNextPoint();
							}
						}
						else
						{
							if (!goingToGetOne)
							{
								findBall();
							}
						}*/
		}
		else if(currentState== "Attack" && canShoot)
		{
			goToNextPoint();
			if (fov.seeEnemy)
			{
				if (fov.SeeTarget!=null)
				{
					transform.LookAt(fov.SeeTarget);
				}
				else
				{
					rotationTowardsDirection();
				}

				if (ballsInClip <= 0)
				{
					currentState = "IDLE";
				}
				else
				{
					Attack();
				}
			}
			else
			{
				currentState = "IDLE";
			}
		}
		else if(currentState == "death")
		{
			//canShoot = false;
			//agent.speed = deathSpeed;

/*			if (!deathAnimPlayed)
			{
				fov.enabled = false;
				fov.seeEnemy = false;
				anim.SetTrigger("death");
				deathAnimPlayed = true;
			}*/

			StartCoroutine(goRessurect());
		}
/*		else if (currentState == "ressurection")
		{
			if (!goingToRessurect)
			{
				goingToRessurect = true;
			}
			if (health.Health >= 4)
			{
				currentState = "IDLE";
				goingToRessurect = false;
				goToNextPoint();
			}
		}*/
	}

	public void goToNextPoint()
	{
		Vector3 randomPoint = poi.points[Random.Range(0, poi.points.Length)].transform.position;
		currentDestination = randomPoint;
		agent.SetDestination(currentDestination);
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
	private void findBall()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, SearchRadius, snowSourceLayer);

		foreach (var snow in hitColliders)
		{
			
			if (snow.gameObject.tag == "snowSource")
			{
				float distance = Vector3.Distance(transform.position, snow.transform.position);

				if (distance < nearestDistance)
				{
					nearestDistance = distance;
					nearestSnowSource = snow.transform;
				}

				if (nearestSnowSource != null)
				{
					goToSpecificPoint(nearestSnowSource.transform.position);
				}

				if (distance < 2)
				{
					if (!grabbingBall)
					{
						StartCoroutine(grabBall());
					}
				}
			}
		}
	}
	private void goToSpecificPoint(Vector3 PointPosition)
	{
		agent.destination = PointPosition;
	}
	IEnumerator grabBall()
	{
		grabbingBall = true;
		agent.speed = 0;
		anim.SetBool("grabSnow", true);
		yield return new WaitForSeconds(grabBallTime);
		agent.speed = agentSpeed;
		goToNextPoint();
		ballsInClip += maxBallsInclip;
		grabbingBall = false;
		anim.SetBool("grabSnow", !true);
	}
	private void Attack()
	{
		if (Time.time > attackTime)
		{
			attackTime = Time.time + MaxAttackTime;
			anim.SetTrigger("shoot");
			GameObject projectiles = Instantiate(projectile, shootingPoint.transform.position, shootingPoint.transform.rotation);
			//projectile.transform.SetParent(BallKillCounter);
			projectiles.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * forcePower);
			ballsInClip--;
		}
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

	IEnumerator goRessurect()
	{
		yield return new WaitForSeconds(4);
		agent.speed = agentSpeed;
		currentState = "ressurection";
		//deathAnimPlayed = false;
		yield return new WaitForSeconds(1);
	}
}
