using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;
	public AudioClip attackSound;

	private Animator anim;
	private GameObject player;
	private AudioSource audioSource;
	private PlayerHealth playerHealth;
	private EnemyHealth enemyHealth;
	private bool playerInRange;
	private float timer;


	private void Awake()
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
			anim.SetBool("Attack", false);
		}
	}


	private void Update()
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


	private void Attack()
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
