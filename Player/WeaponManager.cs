using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {

	public int selectedWeapon = 0;
	public Text ammoText;

	//Strategy Pattern
	private AbstractWeapon currentWeapon;


	void Start () {
		SelectWeapon();
	}
	

	void Update () {

		if (Input.GetButton("Fire1") && Time.timeScale != 0)
		{
			//Strategy Pattern
			currentWeapon.Shoot();
		}

		if (Input.GetButtonUp("Fire1"))
		{
			//Strategy Pattern
			currentWeapon.SetFired(false);
		}

		if (Input.GetButton("Fire2"))
		{
			//Strategy Pattern
			currentWeapon.Aim(true);
		}

		if (Input.GetButtonUp("Fire2"))
		{
			//Strategy Pattern
			currentWeapon.Aim(false);
		}

		if (Input.GetKeyDown("1"))
		{
			//Strategy Pattern
			currentWeapon.AddAmmo(10);
		}

		if (Input.GetKeyDown("q"))
		{
			if(selectedWeapon >= transform.childCount - 1)
			{
				selectedWeapon = 0;
			} else
			{
				selectedWeapon++;
			}

			SelectWeapon();
		}
	}

	private void SelectWeapon()
	{
		int i = 0;
		foreach(Transform weapon in transform)
		{
			if(i == selectedWeapon)
			{
				currentWeapon = weapon.GetComponent<AbstractWeapon>();
				currentWeapon.Aim(false);
				ammoText.text = currentWeapon.GetCurrentAmmo();
				weapon.gameObject.SetActive(true);
			}
			else
			{
				weapon.gameObject.SetActive(false);
			}
			i++;
		}
	}

	public void SetAmmoText(string str)
	{
		ammoText.text = str;
	}
}
