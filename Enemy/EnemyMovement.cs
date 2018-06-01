using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	Transform player;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	private float speed;
	UnityEngine.AI.NavMeshAgent nav;


	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		playerHealth = player.GetComponent<PlayerHealth>();
		enemyHealth = GetComponent<EnemyHealth>();
		nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
		nav.speed = GameManager.Instance.zombieMovementSpeed;
	}

	private void OnEnable()
	{
		if(nav != null)
		{
			nav.speed = GameManager.Instance.zombieMovementSpeed;
		}
	}

	void Update()
	{
		if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
		{
			nav.SetDestination(player.position);
		}
		else
		{
			nav.enabled = false;
		}
	}
}
