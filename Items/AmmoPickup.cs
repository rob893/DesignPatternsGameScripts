using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour {

	public int ammoPickupSize = 1;
	public AudioClip pickUpSound;

	private AudioSource audioSource;

	private void Start()
	{
		audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			if (other.GetComponentInChildren<WeaponManager>().GetCurrentWeapon().AddAmmo(ammoPickupSize) == true)
			{
				audioSource.clip = pickUpSound;
				audioSource.Play();

				Destroy(gameObject);
			}
			else
			{
				GameManager.Instance.ShowMessage("This weapon does not use ammo!", 3);
			}
		}
	}
}
