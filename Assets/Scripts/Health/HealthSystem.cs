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
    [SerializeField] Image PlayerHealthBar;
    public bool PlayerIsDeadTextCalled = false;
    [SerializeField] GameObject deathTecxt;
    [SerializeField] AttackSystem attacks;

    [Header("Enemy VARS")]
    public bool isEnemy;
    private int looseHealth;
    [SerializeField] GameObject EMPattackedText;


    [Header("EMP VARS")]
    public bool isEMP;
    [SerializeField] Image empHealthBar;
    [SerializeField] float timeToReloadLvl = 3f;
    public bool Defeated = false;
    [SerializeField] ParticleSystem flame;
    [SerializeField] GameObject lineRender;
    AudioSource audioSource;
    [SerializeField] AudioClip anderAttack;
    [SerializeField] GameObject PressImpulseTrig;

    //off music
    [SerializeField] CombatMusic music;
    [SerializeField] GameObject musicObject;
    [SerializeField] AudioClip explosionSound;


    private void Start()
    {
        Health = MaxHealth;
        looseHealth = Health;
        audioSource = GetComponent<AudioSource>();
        if (IsPlayer || isEMP)
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
                dead = true;
                cc.enabled = false;
                //anim.SetTrigger("death");
                if (!PlayerIsDeadTextCalled)
				{
                    attacks.enabled = false;
                    deathTecxt.SetActive(true);
                }
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
            if (Health > MaxHealth)
			{
                Health = MaxHealth;
			}
        }
    }

	private void Update()
	{
		if (isEMP)
		{
            float currentHealth = Health;
            float maxHealth = MaxHealth;
            empHealthBar.fillAmount = currentHealth / maxHealth;

			if (looseHealth != Health)
			{
                EMPattackedText.SetActive(true);
				if (!audioSource.isPlaying && !dead)
				{
                    audioSource.PlayOneShot(anderAttack);
                }
                looseHealth = Health;
            }

			if (dead == true)
			{
				if (!Defeated)
				{
                    Defeated = true;
                    anim.SetTrigger("dead");
                    deathTecxt.SetActive(true);
                    StartCoroutine(ReloadLevel());
                    lineRender.SetActive(false);
                    flame.Play();
                    PressImpulseTrig.SetActive(false);
                    audioSource.Stop();
                    music.enabled = false;
                    musicObject.SetActive(false);
                    sourse.GenerateImpulse();
                    audioSource.PlayOneShot(explosionSound);
                }
			}

        }
		if (IsPlayer)
		{
            float currentHealth = Health;
            float maxHealth = MaxHealth;
            PlayerHealthBar.fillAmount = currentHealth / maxHealth;

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