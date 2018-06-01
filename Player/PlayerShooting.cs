using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	public float timeBetweenBullets = 0.1f;
	public float range = 100f;
	public Transform gunBarrel;
	public GameObject projectile;


	float timer;
	AudioSource gunAudio;
	Animation gunShot;
	Light gunLight;
	float effectsDisplayTime = 0.2f;


	void Awake()
	{
		gunShot = GetComponent<Animation>();
		gunAudio = GetComponent<AudioSource>();
		gunLight = GetComponentInChildren<Light>();
	}


	void Update()
	{
		timer += Time.deltaTime;

		if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
		{
			Shoot();
		}

		if (timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects();
		}
	}


	public void DisableEffects()
	{
		gunLight.enabled = false;
	}

	void Shoot()
	{
		timer = 0f;

		gunAudio.Play();

		gunShot.Play("GunShot");
		gunLight.enabled = true;

		if(projectile.name == "Projectile")
		{
			GameObject projectileInstance = BulletPooler.current.GetPooledObject();
			projectileInstance.transform.position = gunBarrel.transform.position;
			projectileInstance.transform.rotation = gunBarrel.transform.rotation;
			projectileInstance.SetActive(true);
		} else if (projectile.name == "Rocket")
		{
			GameObject projectileInstance = RocketPooler.current.GetPooledObject();
			projectileInstance.transform.position = gunBarrel.transform.position;
			projectileInstance.transform.rotation = gunBarrel.transform.rotation;
			projectileInstance.SetActive(true);
		}
		
		//projectileInstance.GetComponent<Rigidbody>().velocity = 300f * gunBarrel.forward;
	}
}
