using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public GameObject impactEffect;

	private int damage = 25;
	private Vector3 prevPos;
	private float timer;


	private void OnEnable()
	{
		prevPos = transform.position;
		timer = 0;

		gameObject.GetComponent<Rigidbody>().velocity = 500f * transform.forward;
	}

	private void FixedUpdate()
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
				hits[i].collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(Damage);
				break;
			}

		}
		if(hits.Length > 0)
		{
			GameObject impactInstance = ObjectPooler.Instance.GetPooledObject(impactEffect);
			impactInstance.transform.position = transform.position;
			impactInstance.transform.rotation = transform.rotation;
			impactInstance.SetActive(true);
			SetInactive();
		}

		prevPos = transform.position;
	}

	private void SetInactive()
	{
		gameObject.SetActive(false);
	}

	public int Damage
	{
		get
		{
			return damage;
		}

		set
		{
			damage = value;
		}
	}
}
