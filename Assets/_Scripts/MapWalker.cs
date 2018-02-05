using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapWalker : MonoBehaviour {

	Vector3 targetPosition;

	void MovementControls () {
		if (Input.GetAxisRaw ("Horizontal") > 0.5f || Input.GetAxisRaw ("Horizontal") < -0.5f) {
			transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime, 0f, 0f));
		}

		if (Input.GetAxisRaw ("Vertical") > 0.5f || Input.GetAxisRaw ("Vertical") < -0.5f) {
			transform.Translate (new Vector3 (0f, Input.GetAxisRaw ("Vertical") * Time.deltaTime, 0f));
		}
	}

	void MouseControls () {
		if (Input.GetKey (KeyCode.Mouse1)) {
			Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint (mousePosition);

			targetPosition = new Vector3 (mouseInWorld.x, mouseInWorld.y, 0f);
		}

		if (Vector3.Distance(transform.position, targetPosition) > 0.1f) {
			transform.position = Vector3.MoveTowards (transform.position, targetPosition, GlobalData.mapMovementSpeed * Time.deltaTime);
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			Debug.Log ("Is on top of map marker: " + GlobalData.ontopOfMapMarker);
		}
	}

	// Use this for initialization
	void Start () {
		targetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//MovementControls ();
		MouseControls ();
	}
}
