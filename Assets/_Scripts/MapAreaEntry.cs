using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAreaEntry : MonoBehaviour {

	public string location = "";

	void OnTriggerEnter2D (Collider2D collider)
	{
		GlobalData.ontopOfMapMarker = true;
		GlobalData.nextEntry = location;
	}

	void OnTriggerExit2D (Collider2D collider) {
		GlobalData.ontopOfMapMarker = false;
	}
}
