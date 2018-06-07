using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 100;
	public int currentHealth;
	public int scoreValue = 10;
	public AudioClip deathClip;
	public AudioClip hurtClip;

	private Animator anim;
	private AudioSource enemyAudio;
	private SphereCollider sphereCollider;
	private CapsuleCollider capsuleCollider;
	private bool isDead;


	private void Awake()
	{
		anim = GetComponent<Animator>();
		enemyAudio = GetComponent<AudioSource>();
		sphereCollider = GetComponent<SphereCollider>();
		capsuleCollider = GetComponent<CapsuleCollider>();
	}

	private void OnEnable()
	{
		currentHealth = startingHealth;
		GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
		isDead = false;
		sphereCollider.enabled = true;
		capsuleCollider.enabled = true;
	}

	public void TakeDamage(int amount)
	{
		if (isDead)
			return;

		enemyAudio.clip = hurtClip;
		enemyAudio.Play();

		currentHealth -= amount;

		if (currentHealth <= 0)
		{
			Death();
		}
	}

	private void Death()
	{
		isDead = true;
		GameManager.Instance.IncreaseScore(scoreValue);
		sphereCollider.enabled = false;
		capsuleCollider.enabled = false;
		GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
		anim.SetTrigger("Dead");
		Invoke("SetInactive", 2f);
		enemyAudio.clip = deathClip;
		enemyAudio.Play();
	}

	public void SetInactive()
	{
		gameObject.SetActive(false);
	}

	public bool getIsDead()
	{
		return isDead;
	}
}
