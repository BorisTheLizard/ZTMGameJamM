using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    public float agentSpeed = 4.5f;
    public string currentState = "IDLE";
    public float SearchRadius = 20f;

    private Vector3 prevPosition;

    //death logic
    HealthSystem health;

    //attack
    public bool isMele = false;
    float attackTime;
    public float MaxAttackTime;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shootingPoint;
    public bool canShoot = true;
    [SerializeField] GameObject hitCollider;
    [SerializeField] ParticleSystem muzzleflash;

    //animator
    [SerializeField] Animator anim;
    public float CCSpeed;
    [SerializeField] Vector3 lastPosition;

    //targeting
    private Vector3 empTransform;
    private Transform empTolookAT;
    public float distanceDebug;
    [SerializeField] Transform PlayerTransform;
    fieldOfView fov;

    //drop on death
    [SerializeField] int deathEffectIndex = 5;
    GameObjectPool pool;
    public int dropObject = 2;
    public bool haveLoot = false;

    //audio
    AudioSource audioSource;
    [SerializeField] AudioClip shootClip;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        prevPosition = transform.position;
        health = GetComponent<HealthSystem>();
        empTolookAT = GameObject.FindGameObjectWithTag("EMP").transform;
        pool = FindObjectOfType<GameObjectPool>();
        fov = GetComponent<fieldOfView>();
        audioSource = GetComponent<AudioSource>();
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
        float PlayerDistance = Vector3.Distance(agent.transform.position, PlayerTransform.position);
        float EmpDistance = Vector3.Distance(agent.transform.position, empTransform);

        if (health.dead)
        {
            //explosionEffect
            health.PlayDestroyedAudio();
            this.gameObject.SetActive(false);
            health.Health = health.MaxHealth;
            health.dead = false;
            if (!isMele)
            {
                fov.StopAllCoroutines();
            }

            if (haveLoot)
            {
                GameObject obj = pool.GetObject(dropObject);
                if (obj != null)
                {
                    obj.transform.position = transform.position;
                    obj.transform.rotation = transform.rotation;
                    obj.SetActive(true);
                }
            }
            GameObject deathEffect = pool.GetObject(deathEffectIndex);
            if (deathEffect != null)
            {
                deathEffect.transform.position = transform.position;
                deathEffect.transform.rotation = transform.rotation;
                deathEffect.SetActive(true);
            }
        }

        if (currentState == "IDLE")
        {
            rotationTowardsDirection();

            distanceDebug = EmpDistance;

            if (PlayerDistance < 8)
            {
                currentState = "Attack";
            }

            if (EmpDistance < 5)
            {
                currentState = "AttackEMP";
            }
        }

        else if (currentState == "Attack")
        {
            if (isMele)
            {
                if (PlayerDistance > 8)
                {
                    currentState = "IDLE";
                }
                else
                {
                    if (PlayerDistance <= 2)
                    {
                        attack();
                    }
                    else
                    {
                        agent.SetDestination(PlayerTransform.position);
                    }
                    if (PlayerDistance > 8)
                    {
                        currentState = "IDLE";
                    }
                }
            }
            else
            {
                if (fov.seeEnemy)
                {
                    //Vector3 pos = transform.position;
                    //pos.z = PlayerTransform.position.z;
                    transform.LookAt(new Vector3(PlayerTransform.position.x, transform.position.y, PlayerTransform.position.z));
                    shoot();
                }
                else
                {
                    agent.SetDestination(PlayerTransform.position);
                }
                if (PlayerDistance > 8)
                {
                    currentState = "IDLE";
                }
            }

        }

        else if (currentState == "AttackEMP")
        {
            transform.LookAt(empTolookAT);
            //isMele = true;

            if (EmpDistance > 2)
            {
                //anim?
            }
            else
            {
                attack();
            }
        }
    }
    void shoot()
    {
        if (Time.time > attackTime)
        {
            attackTime = Time.time + MaxAttackTime;
            muzzleflash.Play();
            audioSource.PlayOneShot(shootClip);
            GameObject obj = pool.GetObject(0);
            if (obj != null)
            {
                obj.transform.position = shootingPoint.transform.position;
                obj.transform.rotation = shootingPoint.transform.rotation;
                obj.SetActive(true);
            }
        }
    }
    void attack()
    {
        if (Time.time > attackTime)
        {
            attackTime = Time.time + MaxAttackTime;
            //if (isMele)
            anim.SetTrigger("attack");
            hitCollider.SetActive(true);
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
}
