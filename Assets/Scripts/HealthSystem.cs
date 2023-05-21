using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;

public class HealthSystem : MonoBehaviour
{
    [Header("Shared VARS")]
    public int MaxHealth;
    public int Health;

    [Header("Player VARS")]
    CinemachineImpulseSource sourse;
    [SerializeField] Animator anim;
    [SerializeField] playerController cc;

    [Header("Enemy VARS")]
    public bool isEnemy;

    [Header("EMP VARS")]
    public bool isEMP;

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

		}

    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (IsPlayer)
        {
            sourse.GenerateImpulse();
        }

        if (Health <= 0)
        {
			if (IsPlayer)
			{
                cc.moveSpeed = 0;
                anim.SetTrigger("death");
                dead = true;
            }
			else
			{
                dead = true;
            }
        }
        
    }
    public void getHeal(int heal)
	{
        Health += heal;

		if (IsPlayer)
		{

        }
    }
}