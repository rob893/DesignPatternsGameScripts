using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerFlame : MonoBehaviour {

	public int damage = 5;
	public Transform gunBarrel;

	private float timer;
	private AudioSource flameSound;
	private List<EnemyHealth> enemiesInFlame;

	private void Awake()
	{
		gunBarrel = GameObject.Find("FlamethrowerBarrel").transform;
		enemiesInFlame = new List<EnemyHealth>();
		flameSound = GetComponent<AudioSource>();
	}

	private void OnEnable()
	{
		timer = 0;
		if(flameSound != null)
		{
			flameSound.Play();
		}
	}

	private void Update()
	{
		timer += Time.deltaTime;

		transform.position = gunBarrel.position;
		transform.rotation = gunBarrel.rotation;

		if(timer >= 0.25)
		{
			foreach (EnemyHealth enemy in enemiesInFlame.ToArray())
			{
				if (enemy.getIsDead())
				{
					enemiesInFlame.Remove(enemy);
				}
				else
				{
					enemy.TakeDamage(damage);
				}
			}

			timer = 0;
		}

		if (Input.GetButtonUp("Fire1"))
		{
			SetInactive();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Enemy")
		{
			enemiesInFlame.Add(other.GetComponent<EnemyHealth>());
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Enemy")
		{
			EnemyHealth enemy = other.GetComponent<EnemyHealth>();
			if (enemiesInFlame.Contains(enemy))
			{
				enemiesInFlame.Remove(enemy);
			}
		}
	}


	private void SetInactive()
	{
		enemiesInFlame.Clear();
		gameObject.SetActive(false);
	}
}
