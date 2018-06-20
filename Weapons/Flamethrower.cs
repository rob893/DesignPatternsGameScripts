using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : AbstractWeapon {

	//Strategy Pattern

	private Vector3 aimTransform = new Vector3(0f, -0.172f, 0.11f);
	private Vector3 hipAim = new Vector3(0.1f, -0.25f, 0.15f);
	private AudioSource gunAudio;
	private Animation gunShot;
	private bool fired = false;
	private bool isAiming = false;
	private GameObject crosshair;
	private GameObject flame;

	private void Awake()
	{
		gunShot = GetComponent<Animation>();
		gunAudio = GetComponent<AudioSource>();
		crosshair = GameObject.Find("Crosshair");
		flame = GetComponentInChildren<FlamethrowerFlame>().gameObject;
		flame.SetActive(false);
		if (!GetComponentInParent<WeaponManager>())
		{
			this.enabled = false;
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

	public override bool AddAmmo(int SizeOfAmmoPickup)
	{
		return false;
	}

	public override string GetWeaponInfo()
	{
		return "Ammo: Unlimited";
	}

	public override void ResetWeapon()
	{
		flame.GetComponent<FlamethrowerFlame>().SetInactive();
		fired = false;
		Aim(false);
	}

	public void Shoot(bool shoot)
	{
		if (shoot && !fired)
		{
			gunAudio.Play();

			flame.SetActive(true);

			gunShot.Play("GunShot");

			fired = true;
		}
		else if(!shoot)
		{
			transform.localRotation = isAiming ? Quaternion.Euler(-1, 0, 0) : Quaternion.Euler(0, 0, 0);
			flame.GetComponent<FlamethrowerFlame>().SetInactive();
			fired = false;
		}
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
}
