using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {

	public int selectedWeapon = 0;
	public Text ammoText;

	private PlayerShooting currentWeapon;


	void Start () {
		SelectWeapon();
	}
	

	void Update () {


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
				PlayerShooting playerShooting = weapon.GetComponent<PlayerShooting>();
				playerShooting.Aim(false);
				ammoText.text = playerShooting.GetCurrentAmmo();
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
