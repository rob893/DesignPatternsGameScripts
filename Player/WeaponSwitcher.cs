using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour {

	public int selectedWeapon = 0;

	private bool weapon1Active;

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
				weapon.gameObject.SetActive(true);
			} else
			{
				weapon.gameObject.SetActive(false);
			}
			i++;
		}
	}
}
