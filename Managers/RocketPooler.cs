using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPooler : MonoBehaviour
{
	//singleton pattern
	public static RocketPooler current;

	public GameObject pooledObject;
	public int pooledAmount = 30;
	public bool willGrow = true;

	List<GameObject> pooledObjects;

	private void Awake()
	{
		current = this;
	}

	private void Start()
	{
		pooledObjects = new List<GameObject>();
		for (int i = 0; i < pooledAmount; i++)
		{
			GameObject obj = (GameObject)Instantiate(pooledObject, transform.position, transform.rotation);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	public GameObject GetPooledObject()
	{
		for (int i = 0; i < pooledObjects.Count; i++)
		{
			if (!pooledObjects[i].activeInHierarchy)
			{
				return pooledObjects[i];
			}
		}

		if (willGrow)
		{
			GameObject obj = (GameObject)Instantiate(pooledObject, transform.position, transform.rotation);
			pooledObjects.Add(obj);
			return obj;
		}

		return null;
	}
}
