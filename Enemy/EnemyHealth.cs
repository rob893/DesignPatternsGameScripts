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
	private EnemyMovement enemyMovement;
	private SphereCollider sphereCollider;
	private CapsuleCollider capsuleCollider;
	private bool isDead;
	private bool isReusable = false;
	private float damageReductionMod = 1;

	public bool IsReusable
	{
		get
		{
			return isReusable;
		}

		set
		{
			isReusable = value;
		}
	}

	public float DamageReductionMod
	{
		get
		{
			return damageReductionMod;
		}

		set
		{
			damageReductionMod = value;
		}
	}

	private void Awake()
	{
		anim = GetComponent<Animator>();
		enemyAudio = GetComponent<AudioSource>();
		sphereCollider = GetComponentInChildren<EnemyAttack>().GetComponent<SphereCollider>();
		capsuleCollider = GetComponent<CapsuleCollider>();
		enemyMovement = GetComponent<EnemyMovement>();
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
		{
			return;
		}

		enemyAudio.clip = hurtClip;
		enemyAudio.Play();

		enemyMovement.Aggroed = true;
		currentHealth -= (int)(amount * damageReductionMod);

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
		Invoke("SetInactive", 3f);
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
