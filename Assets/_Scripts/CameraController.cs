using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject target;
	private Vector3 targetPos;
	//private static bool cameraExists = false;

	// Use this for initialization
	void Awake () {
		/*
		if (!cameraExists) {
			cameraExists = true;
			DontDestroyOnLoad (transform.gameObject);
		} else {
			Destroy (gameObject);
		}*/
	}

	void Start () {
		if (!GlobalData.worldmap) {
			target = GameObject.FindGameObjectWithTag ("Player");
		} else {
			target = GameObject.Find ("MapWalker");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, transform.position.z);		
		}
	}
}
