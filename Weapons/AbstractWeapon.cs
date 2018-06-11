using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour {

	//Strategy Pattern

	public abstract void Shoot();
	public abstract void Aim(bool aiming);
	public abstract void SetFired(bool hasFired);
	public abstract void AddAmmo(int amount);
	public abstract string GetCurrentAmmo();
}
