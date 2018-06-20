using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttack : MonoBehaviour
{

	private Animator anim;
	private GameObject player;
	//private AudioSource audioSource;
	private PlayerHealth playerHealth;
	private EnemyHealth enemyHealth;
	private bool playerInRange;
	private bool attacking = false;
	private float timer;
	private float animationTimer;
	private float timeBetweenAttacks;
	private AbstractEnemyAttack selectedAttack;
	//public AnimationClip attackAnimation;
	private List<AbstractEnemyAttack> availableAttacks;
	private AnimatorOverrideController animatorOverrideController;


	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
		enemyHealth = GetComponentInParent<EnemyHealth>();
		anim = GetComponentInParent<Animator>();
		animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
		anim.runtimeAnimatorController = animatorOverrideController;
		//audioSource = GetComponentInParent<AudioSource>();
		availableAttacks = new List<AbstractEnemyAttack>();

		foreach(AbstractEnemyAttack attack in gameObject.GetComponents<AbstractEnemyAttack>())
		{
			availableAttacks.Add(attack);
		}

		SelectAttack();
	}


	private void OnEnable()
	{
		//anim.SetBool("Attack", false);
		anim.SetInteger("AttackNumber", 0);
		playerInRange = false;
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
		{
			playerInRange = true;
		}
	}


	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player)
		{
			playerInRange = false;
			//anim.SetBool("Attack", false);
			anim.SetInteger("AttackNumber", 0);
		}
	}


	private void Update()
	{
		timer += Time.deltaTime;

		if (playerInRange && !attacking && enemyHealth.currentHealth > 0) //timer >= timeBetweenAttacks
		{
			
			selectedAttack.Attack();
			AnimationWait(timeBetweenAttacks);
			attacking = true;
			timer = 0;
			//SelectAttack();
		}

		if (playerHealth.GetCurrentHealth() <= 0)
		{
			anim.SetInteger("AttackNumber", 0);
			//anim.SetBool("Attack", false);
			anim.SetTrigger("PlayerDead");
		}
	}



	public void SelectAttack()
	{
		selectedAttack = availableAttacks[Random.Range(0, availableAttacks.Count)];
		timeBetweenAttacks = selectedAttack.GetAttackSpeed();
		//attackAnimation = selectedAttack.GetAttackAnimation();
		//animatorOverrideController[attackAnimation] = selectedAttack.GetAttackAnimation();
		attacking = false;
	}

	private IEnumerator AnimationWaitCoroutine(float time)
	{
		yield return new WaitForSeconds(time);
		SelectAttack();
	}

	public void AnimationWait(float time)
	{
		StartCoroutine(AnimationWaitCoroutine(time));
		
	}
}
