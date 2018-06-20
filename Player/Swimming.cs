using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swimming : MonoBehaviour {


	private void OnTriggerEnter(Collider Other)
	{
		GameObject DaveyJones = Other.gameObject;
		if (DaveyJones.tag == "Player")
		{
			DaveyJones.GetComponent<Rigidbody>().drag = 5;
			DaveyJones.GetComponent<Rigidbody>().useGravity = false;
			DaveyJones.GetComponent<Rigidbody>().velocity = new Vector3(0f, -.5f, 0f);
			//DaveyJones.GetComponent<Animator>().SetBool("IsSwimming", true);
			//DaveyJones.GetComponent<MouseControls>().isSwimming = true;

		}
	}

	private void OnTriggerExit(Collider Other)
	{
		GameObject DaveyJones = Other.gameObject;
		if (DaveyJones.tag == "Player")
		{
			DaveyJones.GetComponent<Rigidbody>().drag = 1;
			DaveyJones.GetComponent<Rigidbody>().useGravity = true;
			DaveyJones.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
			//DaveyJones.GetComponent<Animator>().SetBool("IsSwimming", false);
			//DaveyJones.GetComponent<MouseControls>().isSwimming = false;

		}
	}
}
