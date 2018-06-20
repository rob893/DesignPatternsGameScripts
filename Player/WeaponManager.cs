using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponManager : MonoBehaviour {

	public int selectedWeapon = 0;
	public TextMeshProUGUI ammoText;

	//Strategy Pattern
	private AbstractWeapon currentWeapon;


	void Update () {

		if (Input.GetButton("Fire1"))
		{
			//Strategy Pattern
			currentWeapon.PrimaryFunction(true);
		}

		if (Input.GetButtonUp("Fire1"))
		{
			//Strategy Pattern
			currentWeapon.PrimaryFunction(false);
		}

		if (Input.GetButton("Fire2"))
		{
			//Strategy Pattern
			currentWeapon.SecondaryFunction(true);
		}

		if (Input.GetButtonUp("Fire2"))
		{
			//Strategy Pattern
			currentWeapon.SecondaryFunction(false);
		}

		if (Input.GetKeyDown("q"))
		{
			selectedWeapon = (selectedWeapon + 1) % transform.childCount;

			SelectWeapon();
		}
	}


	public void SelectWeapon()
	{
		int i = 0;
		foreach(Transform weapon in transform)
		{
			if(i == selectedWeapon)
			{
				currentWeapon = weapon.GetComponent<AbstractWeapon>();
				ammoText.text = currentWeapon.GetWeaponInfo();
				currentWeapon.ResetWeapon();
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

	public AbstractWeapon GetCurrentWeapon()
	{
		return currentWeapon;
	}
}
