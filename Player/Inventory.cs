using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public List<GameObject> inventory;

	private WeaponManager weaponManager;
	private Vector3 weaponVector3 = new Vector3(0.1f, -0.25f, 0.15f);
	private bool itemInRange = false;
	private GameObject item;


	private void Start()
	{
		weaponManager = GetComponentInChildren<WeaponManager>();
		foreach(GameObject weapon in inventory)
		{
			GameObject newWeapon = Instantiate(weapon, weaponManager.transform, false);
			newWeapon.transform.position = weaponVector3;
			newWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
			newWeapon.GetComponent<SphereCollider>().enabled = false;
		}
	}

	private void Update()
	{
		if(itemInRange && item != null)
		{
			if (Input.GetKeyDown("f"))
			{
				PickUpItem(item);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<AbstractWeapon>())
		{
			item = other.gameObject;
			itemInRange = true;
			GameManager.Instance.ShowMessage("Press F to pick up " + other.name, 0, false);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(itemInRange && item != null)
		{
			itemInRange = false;
			item = null;
			GameManager.Instance.ShowMessage("", 1);
		}
	}

	private void PickUpItem(GameObject item)
	{
		inventory.Add(item);
			
		item.transform.parent = weaponManager.transform;
		item.transform.localPosition = weaponVector3;
		item.transform.localRotation = Quaternion.Euler(0, 0, 0);
		item.transform.localScale = new Vector3(1, 1, 1);
		item.GetComponent<SphereCollider>().enabled = false;
		item.GetComponent<AbstractWeapon>().enabled = true;
		GameManager.Instance.ShowMessage("", 1);

		item.SetActive(false);
	}
}
