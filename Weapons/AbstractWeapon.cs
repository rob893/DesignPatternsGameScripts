using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour {

	//Strategy Pattern

	public abstract void PrimaryFunction(bool b);
	public abstract void SecondaryFunction(bool b);
	public abstract bool AddAmmo(int sizeOfAmmoPickUp);
	public abstract string GetWeaponInfo();
	public abstract void ResetWeapon();
}
