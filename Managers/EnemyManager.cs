using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public GameObject enemy;
	public Transform[] spawnPoints;

	private GameObject player;
	private PlayerHealth playerHealth;
	private float spawnTime;


	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
		spawnTime = GameManager.Instance.spawnTime;
		InvokeRepeating("Spawn", spawnTime, spawnTime);
	}


	private void Spawn()
	{
		Transform closestSpawnPoint = spawnPoints[0];
		Vector3 playerPosition = player.transform.position;
		foreach (Transform spawnPoint in spawnPoints)
		{
			float distanceToPlayer = Vector3.Distance(spawnPoint.position, playerPosition);
			if(distanceToPlayer < Vector3.Distance(closestSpawnPoint.position, playerPosition))
			{
				closestSpawnPoint = spawnPoint;
			}
		}
		if(playerHealth.GetCurrentHealth() > 0)
		{
			//int spawnPointIndex = Random.Range(0, spawnPoints.Length);

			GameObject obj = ObjectPooler.Instance.GetPooledObject(enemy);
			obj.transform.position = closestSpawnPoint.position;
			obj.transform.rotation = closestSpawnPoint.rotation;
			obj.SetActive(true);

			obj.GetComponent<EnemyMovement>().Aggroed = true;
		}
	}
}
