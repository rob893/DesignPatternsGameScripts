using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{

	public int ammoPickupSize = 1;
	public AudioClip pickUpSound;

	private AudioSource audioSource;

	private void Start()
	{
		audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
			if (playerHealth.GetCurrentHealth() < playerHealth.startingHealth)
			{
				audioSource.clip = pickUpSound;
				audioSource.Play();
				playerHealth.AddHealth(25);
				Destroy(gameObject);
			}
			else
			{
				GameManager.Instance.ShowMessage("You are already at full health!", 3);
			}
		}
	}
}
