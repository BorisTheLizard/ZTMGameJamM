using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [Header("Shared VARS")]
    public int MaxHealth;
    public int Health;
    public bool dead = false;

    [Header("Player VARS")]
    CinemachineImpulseSource sourse;
    [SerializeField] Animator anim;
    [SerializeField] playerController cc;
    public bool IsPlayer;

    [Header("Enemy VARS")]
    public bool isEnemy;


    [Header("EMP VARS")]
    public bool isEMP;
    [SerializeField] Image empHealthBar;
    [SerializeField] float timeToReloadLvl = 3f;


    private void Start()
    {
        Health = MaxHealth;

        if (IsPlayer)
        {
            sourse = GetComponent<CinemachineImpulseSource>();
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

	private void Update()
	{
		if (isEMP)
		{
            float currentHealth = Health;
            float maxHealth = MaxHealth;
            empHealthBar.fillAmount = currentHealth / maxHealth;

			if (dead == true)
			{
                StartCoroutine(ReloadLevel());
			}
        }
	}
    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(timeToReloadLvl);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}