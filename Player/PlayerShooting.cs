using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	public float timeBetweenBullets = 0.1f;
	public int startingAmmo = 1000;
	public Transform gunBarrel;
	public GameObject projectile;
	public bool singleShot;
	public bool hasAmmo;

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

	private void Update()
	{
		timer += Time.deltaTime;

		if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
		{
			if(!singleShot)
			{
				Shoot();
			}
			else
			{
				if (!fired)
				{
					Shoot();
				}
			}
		}

		if (Input.GetButtonUp("Fire1"))
		{
			fired = false;
			transform.localRotation = isAiming ? Quaternion.Euler(-1, 0, 0) : Quaternion.Euler(0, 0, 0);
		}

		if (Input.GetButton("Fire2"))
		{
			Aim(true);
		}

		if (Input.GetButtonUp("Fire2"))
		{
			Aim(false);
		}

		if (Input.GetKeyDown("1") && hasAmmo)
		{
			AddAmmo(10);
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

	private void Shoot()
	{
		if(hasAmmo && currentAmmo <= 0)
		{
			GameManager.Instance.ShowMessage("Out of ammo!", 2f);
			return;
		}

		timer = 0f;

		if (hasAmmo)
		{
			currentAmmo -= 1;
			weaponManager.SetAmmoText("Ammo: " + currentAmmo);
		}

		gunAudio.Play();

		
		gunLight.enabled = true;

		GameObject projectileInstance = ObjectPooler.Instance.GetPooledObject(projectile);
		projectileInstance.transform.position = gunBarrel.transform.position;
		projectileInstance.transform.rotation = gunBarrel.transform.rotation;
		projectileInstance.SetActive(true);
		//projectileInstance.GetComponent<Rigidbody>().velocity = projectileVelocity * gunBarrel.forward;

		if (!singleShot)
		{
			if (!isAiming && fired)
			{
				Quaternion randomRotation = Quaternion.Euler(Random.Range(-1.75f, 1.75f), Random.Range(-1.75f, 1.75f), 0);
				transform.localRotation = randomRotation;
			}
			else if (fired)
			{
				Quaternion randomRotation = Quaternion.Euler(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
				transform.localRotation = randomRotation;
			}
			else
			{
				transform.localRotation = Quaternion.Euler(-1f, 0, 0);
			}
		}
		else
		{
			gunShot.Play("GunShot");
		}


		fired = true;
	}

	public void Aim(bool aiming)
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

	public string GetCurrentAmmo()
	{
		if (hasAmmo)
		{
			return "Ammo: " + currentAmmo;
		}
		else
		{
			return "Ammo: Unlimited";
		}
	}

	public void AddAmmo(int amount)
	{
		currentAmmo += amount;
		weaponManager.SetAmmoText("Ammo: " + currentAmmo);
	}
}
