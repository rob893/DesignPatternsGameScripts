using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack1 : AbstractEnemyAttack {

	public float attackSpeed = 2;
	public int attackDamage = 10;
	public float damageDelay;
	public AudioClip attackSound;
	//public AnimationClip attackAnimation;
	public float animationClipLength;


	public override void Attack()
	{
		if (playerHealth.GetCurrentHealth() > 0 && damageDelay > 0)
		{
			//anim.SetBool("Attack", true);
			anim.SetInteger("AttackNumber", 1);
			audioSource.clip = attackSound;
			DamageDelay(damageDelay);
		}
		else if(playerHealth.GetCurrentHealth() > 0 && damageDelay == 0)
		{
			//anim.SetBool("Attack", true);
			anim.SetInteger("AttackNumber", 1);
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
