using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	public float aggroRange = 50;

	private Transform player;
	private PlayerHealth playerHealth;
	private Animator anim;
	private EnemyHealth enemyHealth;
	private float baseSpeed = 4;
	private UnityEngine.AI.NavMeshAgent nav;
	private float moveTimer = 0;
	private float distanceTimer = 0;
	private float distanceToPlayer;
	private bool aggroed = false;


	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerHealth = player.GetComponent<PlayerHealth>();
		enemyHealth = GetComponent<EnemyHealth>();
		anim = GetComponent<Animator>();
		nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
		nav.speed = baseSpeed + (GameManager.Instance.Score / 100);
		distanceToPlayer = Vector3.Distance(this.transform.position, player.position);
	}

	private void OnDisable()
	{
		aggroed = false;
	}

	private void OnEnable()
	{
		if(nav != null)
		{
			nav.speed = baseSpeed + (GameManager.Instance.Score / 100);
		}
	}

	private void Update()
	{
		distanceTimer += Time.deltaTime;
		moveTimer += Time.deltaTime;

		if(distanceTimer >= 1f)
		{
			distanceToPlayer = Vector3.Distance(this.transform.position, player.position);
			if(distanceToPlayer <= aggroRange && !aggroed)
			{
				aggroed = true;
				nav.speed = baseSpeed + (GameManager.Instance.Score / 100);
			}
			distanceTimer = 0;
		}

		if (aggroed && distanceToPlayer <= aggroRange)
		{
			MoveToPlayer();
			moveTimer = 0;
		}
		else if (aggroed && moveTimer >= 1)
		{
			MoveToPlayer();
			moveTimer = 0;
		}
	}

	private void MoveToPlayer()
	{
		if (enemyHealth.currentHealth > 0 && playerHealth.GetCurrentHealth() > 0)
		{
			anim.SetTrigger("Aggroed");
			nav.SetDestination(player.position);
		}
		else
		{
			nav.enabled = false;
		}
	}

	public bool Aggroed
	{
		get
		{
			return aggroed;
		}

		set
		{
			aggroed = value;
		}
	}
}
