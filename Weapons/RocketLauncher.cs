using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : AbstractWeapon {

	//Strategy Pattern

	public float timeBetweenBullets = 1f;
	public int startingAmmo = 5;
	public Transform gunBarrel;
	public GameObject projectile;

	private float timer;
	private int currentAmmo;
	private Vector3 aimTransform = new Vector3(0f, -0.172f, 0.11f);
	private Vector3 hipAim = new Vector3(0.1f, -0.25f, 0.15f);
	private AudioSource gunAudio;
	private Animation gunShot;
	private Light gunLight;
	private float effectsDisplayTime = 0.2f;
	private bool fired = false;
	private bool isAiming = false;
	private WeaponManager weaponManager;
	private GameObject crosshair;


	private void Awake()
	{
		gunShot = GetComponent<Animation>();
		gunAudio = GetComponent<AudioSource>();
		gunLight = GetComponentInChildren<Light>();
		crosshair = GameObject.Find("Crosshair");
		weaponManager = GameObject.Find("WeaponHolder").GetComponent<WeaponManager>();
		currentAmmo = startingAmmo;
		if (!GetComponentInParent<WeaponManager>())
		{
			this.enabled = false;
		}
	}

	private void OnDisable()
	{
		fired = false;
	}

	private void Update ()
	{
		timer += Time.deltaTime;

		if (timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects();
		}
	}

	public override void Shoot()
	{
		if (timer >= timeBetweenBullets && !fired)
		{
			
			if (currentAmmo <= 0)
			{
				GameManager.Instance.ShowMessage("Out of ammo!", 2f);
				return;
			}

			timer = 0f;

			currentAmmo -= 1;
			weaponManager.SetAmmoText("Ammo: " + currentAmmo);

			gunAudio.Play();

			gunLight.enabled = true;

			GameObject projectileInstance = ObjectPooler.Instance.GetPooledObject(projectile);
			projectileInstance.transform.position = gunBarrel.transform.position;
			projectileInstance.transform.rotation = gunBarrel.transform.rotation;
			projectileInstance.SetActive(true);
			//projectileInstance.GetComponent<Rigidbody>().velocity = projectileVelocity * gunBarrel.forward;

			gunShot.Play("GunShot");
			
			fired = true;
		}
	}

	public override void Aim(bool aiming)
	{
		if (aiming)
		{
			crosshair.SetActive(false);
			isAiming = true;
			transform.localPosition = Vector3.Slerp(transform.localPosition, aimTransform, 10 * Time.deltaTime);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(-1, 0, 0), 10 * Time.deltaTime);
		}
		else
		{
			crosshair.SetActive(true);
			isAiming = false;
			transform.localPosition = hipAim;
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
	}

	public override string GetCurrentAmmo()
	{
		return "Ammo: " + currentAmmo;
	}

	public override void AddAmmo(int amount)
	{
		currentAmmo += amount;
		weaponManager.SetAmmoText("Ammo: " + currentAmmo);
	}

	public override void SetFired(bool hasFired)
	{
		if (hasFired == false)
		{
			transform.localRotation = isAiming ? Quaternion.Euler(-1, 0, 0) : Quaternion.Euler(0, 0, 0);
		}
		fired = hasFired;
	}

	public void DisableEffects()
	{
		gunLight.enabled = false;
	}
}
