using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	public float maxDamage = 500f;
	public float impactRadius = 5f;
	public float explosionForce = 10f;
	public GameObject impactEffect;
	public LayerMask enemyMask;
	//public AudioSource explosionSound;

	private float timer;

	void OnEnable()
	{
		timer = 0;

		gameObject.GetComponent<Rigidbody>().velocity = 100f * transform.forward;
	}

	private void Update()
	{
		timer += Time.deltaTime;

		if (timer >= 4)
		{
			SetInactive();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, impactRadius, enemyMask);

		for (int i = 0; i < colliders.Length; i++)
		{
			Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

			if (!targetRigidbody)
			{
				continue;
			}

			targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionForce);

			EnemyHealth targetHealth = targetRigidbody.GetComponent<EnemyHealth>();

			if (!targetHealth)
			{
				continue;
			}

			float damage = CalculateDamage(targetRigidbody.transform.position);

			targetHealth.TakeDamage((int)damage);
		}

		
		//explosionSound.Play();

		Instantiate(impactEffect, transform.position, transform.rotation);
		SetInactive();
	}


	private float CalculateDamage(Vector3 targetPosition)
	{
		// Calculate the amount of damage a target should take based on it's position.
		Vector3 explosionToTarget = targetPosition - transform.position;

		float explsionDistance = explosionToTarget.magnitude;
		float relativeDistance = (impactRadius - explsionDistance) / impactRadius;
		float damage = relativeDistance * maxDamage;

		damage = Mathf.Max(0f, damage);
		return damage;
	}


	private void SetInactive()
	{
		gameObject.SetActive(false);
	}
}
