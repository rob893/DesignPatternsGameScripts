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
		if(playerHealth.currentHealth > 0)
		{
			int spawnPointIndex = Random.Range(0, spawnPoints.Length);

			GameObject obj = ObjectPooler.Instance.GetPooledObject(enemy);
			obj.transform.position = spawnPoints[spawnPointIndex].position;
			obj.transform.rotation = spawnPoints[spawnPointIndex].rotation;
			obj.SetActive(true);
		}
	}
}
