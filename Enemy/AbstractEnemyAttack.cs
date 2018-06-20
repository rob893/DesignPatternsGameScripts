using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemyAttack : MonoBehaviour {

	protected Animator anim;
	protected GameObject player;
	protected AudioSource audioSource;
	protected PlayerHealth playerHealth;
	protected EnemyHealth enemyHealth;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
		enemyHealth = GetComponentInParent<EnemyHealth>();
		anim = GetComponentInParent<Animator>();
		audioSource = GetComponentInParent<AudioSource>();
	}

	public abstract void Attack();
	public abstract float GetAttackSpeed();
	//public abstract AnimationClip GetAttackAnimation();
}
