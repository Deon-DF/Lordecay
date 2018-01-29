using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGUI : MonoBehaviour {

	GameObject enterLocationButton;

	// Use this for initialization
	void Awake () {

		enterLocationButton = GameObject.Find ("EnterArea");
		enterLocationButton.SetActive (false);
		
	}

	void Update () {
		if (GlobalData.ontopOfMapMarker) {
			enterLocationButton.SetActive (true);
		} else {
			enterLocationButton.SetActive (false);
		}
	}
}
