using UnityEngine;
using System.Collections;

public class NinjaStar : MonoBehaviour {

	public GameObject ninjaStar;

	void Destroy ()
	{
		ninjaStar.SetActive(false);
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "ground" || coll.gameObject.tag == "Platform" || coll.gameObject.tag == "Wall") {
			ninjaStar.SetActive(false);
		}
	}
}
