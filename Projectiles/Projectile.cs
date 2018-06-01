using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public int damage = 25;
	public GameObject impactEffect;
	private Vector3 prevPos;
	private float timer;

	void OnEnable()
	{
		prevPos = transform.position;
		timer = 0;

		gameObject.GetComponent<Rigidbody>().velocity = 300f * transform.forward;
	}

	void FixedUpdate()
	{
		timer += Time.deltaTime;

		if(timer >= 4)
		{
			SetInactive();
		}

		RaycastHit[] hits = Physics.RaycastAll(new Ray(prevPos, (transform.position - prevPos).normalized), (transform.position - prevPos).magnitude);
		
		for(int i = 0; i < hits.Length; i++)
		{
			if (hits[i].collider.gameObject.CompareTag("Enemy"))
			{
				hits[i].collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
			}

		}
		if(hits.Length > 0)
		{
			Instantiate(impactEffect, transform.position, transform.rotation);
			SetInactive();
		}

		prevPos = transform.position;
	}

	private void SetInactive()
	{
		gameObject.SetActive(false);
	}
}
