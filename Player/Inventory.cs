using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public List<GameObject> inventory;
	public AudioClip pickUpSound;

	private WeaponManager weaponManager;
	private Vector3 weaponVector3 = new Vector3(0.1f, -0.25f, 0.15f);
	private bool itemInRange = false;
	private GameObject item;
	private AudioSource audioSource;


	private void Start()
	{
		weaponManager = GetComponentInChildren<WeaponManager>();
		audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
		foreach(GameObject weapon in inventory)
		{
			GameObject newWeapon = Instantiate(weapon, weaponManager.transform, false);
			newWeapon.transform.position = weaponVector3;
			newWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
			newWeapon.GetComponent<SphereCollider>().enabled = false;
			SetLayer(newWeapon, 9);
		}
		weaponManager.SelectWeapon();
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

		audioSource.clip = pickUpSound;
		audioSource.Play();
		item.transform.parent = weaponManager.transform;
		item.transform.localPosition = weaponVector3;
		item.transform.localRotation = Quaternion.Euler(0, 0, 0);
		item.transform.localScale = new Vector3(1, 1, 1);
		SetLayer(item, 9);
		item.GetComponent<SphereCollider>().enabled = false;
		item.GetComponent<AbstractWeapon>().enabled = true;
		GameManager.Instance.ShowMessage("", 1);

		item.SetActive(false);
	}

	public static void SetLayer(GameObject obj, int layer)
	{
		obj.layer = layer;
		foreach (Transform child in obj.transform)
			SetLayer(child.gameObject, layer);
	}
}
