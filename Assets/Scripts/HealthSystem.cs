using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;

public class HealthSystem : MonoBehaviour
{
    public int MaxHealth;
    public int Health;
    CinemachineImpulseSource sourse;
   // [SerializeField] minusHealthVisual minusHealth;
    [SerializeField] Animator anim;
    [SerializeField] playerController cc;
   // [SerializeField] shooting PlayerShoot;

    [SerializeField] GameObject player;
    //[SerializeField] LayerMask defaultMask;
    [SerializeField] bool animationPlayed = false;
    [SerializeField] GameObject pathAI;
    enemyAI ai;
    [SerializeField] GameObject punchText;

    public bool dead = false;

    public bool IsPlayer;

    private void Start()
    {

        Health = MaxHealth;

        if (IsPlayer)
        {
            sourse = GetComponent<CinemachineImpulseSource>();
        }
		else
		{
            ai = GetComponent<enemyAI>();
		}

    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (IsPlayer)
        {
            sourse.GenerateImpulse();
         //   minusHealth.DestroyLastCounted();
        }

        if (Health <= 0)
        {
			if (IsPlayer)
			{
                cc.moveSpeed = 0;
                anim.SetTrigger("death");
                dead = true;
                player.layer = 0;
          //      PlayerShoot.canShoot = false;
                punchText.SetActive(true);
                if (!animationPlayed)
				{
                    StartCoroutine(returnSpeed());
                }
            }
			else
			{
                dead = true;
                ai.currentState = "death";
                ai.gameObject.layer = 0;
            }
        }
        
    }
    public void getHeal(int heal)
	{
        Health += heal;

		if (IsPlayer)
		{
   //         minusHealth.PlusHealth();
        }
    }
    IEnumerator returnSpeed()
	{
        animationPlayed = true;
        pathAI.transform.position = cc.transform.position;
        yield return new WaitForSeconds(4f);
        cc.moveSpeed = 5;
        animationPlayed = false;
        pathAI.SetActive(true);
    }
}