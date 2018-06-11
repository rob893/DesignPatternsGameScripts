using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerFlame : MonoBehaviour {

	public int damage = 5;

	private float timer;
	private AudioSource flameSound;
	private List<EnemyHealth> enemiesInFlame;

	private void Awake()
	{
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

		if(timer >= 0.2)
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

	public void SetInactive()
	{
		if(enemiesInFlame != null)
		{
			enemiesInFlame.Clear();
		}
		gameObject.SetActive(false);
	}
}
