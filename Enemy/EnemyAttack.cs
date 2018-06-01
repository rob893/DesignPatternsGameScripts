using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;
	public AudioClip attackSound;


	Animator anim;
	GameObject player;
	private AudioSource audioSource;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	bool playerInRange;
	float timer;


	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
		enemyHealth = GetComponent<EnemyHealth>();
		anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}


	private void OnEnable()
	{
		anim.SetBool("Attack", false);
		playerInRange = false;
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
		{
			playerInRange = true;
		}
	}


	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player)
		{
			playerInRange = false;
			anim.SetBool("Attack", false);
		}
	}


	void Update()
	{
		timer += Time.deltaTime;

		if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
		{
			Attack();
		}

		if (playerHealth.currentHealth <= 0)
		{
			anim.SetBool("Attack", false);
			anim.SetTrigger("PlayerDead");
		}
	}


	void Attack()
	{
		timer = 0f;
		
		if (playerHealth.currentHealth > 0)
		{
			anim.SetBool("Attack", true);
			audioSource.clip = attackSound;
			audioSource.Play();
			playerHealth.TakeDamage(attackDamage);
		}
	}
}
