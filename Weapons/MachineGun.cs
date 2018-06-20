using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : AbstractWeapon {

	//Strategy Pattern

	public int damage = 25;
	public float timeBetweenBullets = 0.1f;
	public int startingAmmo = 1000;
	public Transform gunBarrel;
	public GameObject projectile;

	private float timer = 0;
	private int currentAmmo;
	private Vector3 aimTransform = new Vector3(0f, -0.172f, 0.11f);
	private Vector3 hipAim = new Vector3(0.1f, -0.25f, 0.15f);
	private AudioSource gunAudio;
	private Light gunLight;
	private float effectsDisplayTime = 0.2f;
	private bool fired = false;
	private bool isAiming = false;
	private WeaponManager weaponManager;
	private GameObject crosshair;
	private float damageMod = 1;

	private void Awake()
	{
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

	private void Update () {
		timer += Time.deltaTime;

		if (timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects();
		}
	}

	public override void PrimaryFunction(bool b)
	{
		Shoot(b);
	}

	public override void SecondaryFunction(bool b)
	{
		Aim(b);
	}

	public override string GetWeaponInfo()
	{
		return "Ammo: " + currentAmmo;
	}

	public override bool AddAmmo(int sizeOfAmmoPickup)
	{
		int amount;
		if(sizeOfAmmoPickup == 1)
		{
			amount = 100;
		} 
		else
		{
			amount = 200;
		}
		currentAmmo += amount;
		weaponManager.SetAmmoText("Ammo: " + currentAmmo);
		return true;
	}

	public override void ResetWeapon()
	{
		fired = false;
		Aim(false);
		timer = 0;
	}

	public void Shoot(bool shoot)
	{
		if(shoot && timer >= timeBetweenBullets)
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
			projectileInstance.GetComponent<Projectile>().Damage = (int)(damage * damageMod);
			projectileInstance.SetActive(true);

			if (!isAiming && fired)
			{
				Quaternion randomRotation =  Quaternion.Euler(Random.Range(-1.75f, 1.75f), Random.Range(-1.75f, 1.75f), 0);
				transform.localRotation = randomRotation;
			}
			else if (fired)
			{
				Quaternion randomRotation = Quaternion.Euler(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
				transform.localRotation = randomRotation;
			}
			else
			{
				transform.localRotation = Quaternion.Euler(-1.5f, 0, 0);
			}

			fired = true;
		}
		else if(!shoot)
		{
			transform.localRotation = isAiming ? Quaternion.Euler(-1, 0, 0) : Quaternion.Euler(0, 0, 0);
			fired = false;
		}
	}

	public void Aim(bool aiming)
	{
		if (aiming)
		{
			crosshair.SetActive(false);
			isAiming = true;
			damageMod = 1.25f;
			transform.localPosition = Vector3.Slerp(transform.localPosition, aimTransform, 10 * Time.deltaTime);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(-1, 0, 0), 10 * Time.deltaTime);
		}
		else
		{
			crosshair.SetActive(true);
			isAiming = false;
			damageMod = 1;
			transform.localPosition = hipAim;
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
	}

	public void DisableEffects()
	{
		gunLight.enabled = false;
	}
}
