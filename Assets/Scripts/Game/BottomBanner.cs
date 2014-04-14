using UnityEngine;
using System.Collections;

public class BottomBanner : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Box")
		{
			Destroy (col.gameObject);
		}
	}
}
