using UnityEngine;
using System.Collections;

public class NinjaStarFireScript : MonoBehaviour {

	void Fire()
	{
		GameObject obj = ObjectPooler.current.GetPooledObject();

		if (obj == null) {
			return;
		}

		obj.transform.position = transform.position;
		obj.transform.rotation = transform.rotation;
		obj.SetActive(true);
	}
}