using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : AbstractWeapon {

	//Strategy Pattern

	public float timeBetweenSwings = 0.5f;
	public int damage = 50;
	public LayerMask enemyMask;

	private Vector3 attackingPosition = new Vector3(0.1f, -0.25f, 0.65f);
	private Vector3 attackingRotation = new Vector3(-15, 270, 0);
	private Vector3 startingPosition = new Vector3(0.2f, -0.25f, 0.45f);
	private Vector3 startingRotation = new Vector3(-5f, 270, 90f);
	private Vector3 blockPosition = new Vector3(0, -0.2f, 0.45f);
	private Vector3 blockRotation = new Vector3(-45f, 270, 90f); 
	private AudioSource swingAudio;
	private bool attacking = false;
	private bool isInStartingPosition = false;
	private float timer = 0;
	private Transform player;
	private bool isBlocking = false;
	private PlayerHealth playerHealth;


	private void Awake()
	{
		swingAudio = GetComponent<AudioSource>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		playerHealth = player.GetComponent<PlayerHealth>();
		if (!GetComponentInParent<WeaponManager>())
		{
			this.enabled = false;
		}
	}

	private void Update()
	{
		timer += Time.deltaTime;
		if (attacking)
		{
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(attackingRotation), 25 * Time.deltaTime);
			transform.localPosition = Vector3.Slerp(transform.localPosition, attackingPosition, 25 * Time.deltaTime);
		}
		else if (!isInStartingPosition && !isBlocking)
		{
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(startingRotation), 10 * Time.deltaTime);
			transform.localPosition = Vector3.Slerp(transform.localPosition, startingPosition, 10 * Time.deltaTime);
			if (transform.localPosition == startingPosition)
			{
				isInStartingPosition = true;
			}
		}
	}

	public override bool AddAmmo(int SizeOfAmmoPickup)
	{
		return false;
	}

	public override string GetWeaponInfo()
	{
		return "Ammo: Unlimited";
	}

	public override void PrimaryFunction(bool b)
	{
		Attack(b);
	}

	public override void SecondaryFunction(bool b)
	{
		Block(b);
	}

	public override void ResetWeapon()
	{
		timer = 0;
		transform.localPosition = attackingPosition;
		transform.localRotation = Quaternion.Euler(attackingRotation);
		attacking = false;
		isInStartingPosition = false;
		isBlocking = false;
	}

	public void Attack(bool attack)
	{
		if(attack && timer >= timeBetweenSwings && !attacking)
		{

			Collider[] colliders = Physics.OverlapSphere(transform.position, 2.5f, enemyMask);

			for (int i = 0; i < colliders.Length; i++)
			{
				CapsuleCollider collider = colliders[i] as CapsuleCollider;
				if (collider != null)
				{
					var angle = Vector3.Angle(player.transform.forward, (colliders[i].gameObject.transform.position - player.transform.position));
					if(angle < 70)
					{
						collider.GetComponent<EnemyHealth>().TakeDamage(damage);
						swingAudio.Play();
					}
				}
			}
			attacking = true;
			isInStartingPosition = false;
			timer = 0;
		}
		else if(!attack)
		{
			attacking = false;
		}
	}

	public void Block(bool blocking)
	{
		if (blocking && timer >= timeBetweenSwings && !attacking)
		{
			isBlocking = true;
			playerHealth.DamageReductionMod = 0;
			transform.localPosition = Vector3.Slerp(transform.localPosition, blockPosition, 10 * Time.deltaTime);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(blockRotation), 10 * Time.deltaTime);
		}
		else
		{
			isBlocking = false;
			playerHealth.DamageReductionMod = 1;
			isInStartingPosition = false;
		}
	}
}
