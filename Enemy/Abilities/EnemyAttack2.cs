using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack2 : AbstractEnemyAttack {

	public float attackSpeed = 2;
	public float damageDelay = 0;
	public int attackDamage = 20;
	//public AnimationClip attackAnimation;
	public AudioClip attackSound;
	public float animationClipLength;


	public override void Attack()
	{
		if (playerHealth.GetCurrentHealth() > 0 && damageDelay > 0)
		{
			//anim.SetBool("Attack", true);
			anim.SetInteger("AttackNumber", 2);
			audioSource.clip = attackSound;
			DamageDelay(damageDelay);
		}
		else if (playerHealth.GetCurrentHealth() > 0 && damageDelay == 0)
		{
			//anim.SetBool("Attack", true);
			anim.SetInteger("AttackNumber", 2);
			audioSource.Play();
			playerHealth.TakeDamage(attackDamage);
		}
	}

	//public override AnimationClip GetAttackAnimation()
	//{
	//	return attackAnimation;
	//}

	public override float GetAttackSpeed()
	{
		return attackSpeed;
	}

	private IEnumerator DamageDelayCoroutine(float time)
	{
		yield return new WaitForSeconds(time);
		if (!enemyHealth.getIsDead())
		{
			audioSource.Play();
			playerHealth.TakeDamage(attackDamage);
		}
	}

	public void DamageDelay(float time)
	{
		StartCoroutine(DamageDelayCoroutine(time));

	}
}
