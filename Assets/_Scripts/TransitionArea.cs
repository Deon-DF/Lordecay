using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionArea : MonoBehaviour {

	public string areaName;
	public string entryname;

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "Player") {

			GlobalData.nextEntry = entryname;
			if (areaName == "WorldMap") {
				GlobalData.worldmap = true;
			} else {
				GlobalData.worldmap = false;	
			}
			SceneManager.LoadScene (areaName);
		}
	}
}
