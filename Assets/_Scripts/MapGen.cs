using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGen : MonoBehaviour {


	GameObject PickRandomGameObject (List<GameObject> list) {
		int result = Random.Range(0, list.Count);
		return list[result];
	}

	void Awake () {
		if (SceneManager.GetActiveScene ().name == "Safehouse") {

			GameObject safehouse = Resources.Load ("Tiled2Unity/Prefabs/Safehouse") as GameObject;
			GameObject maptile = Instantiate (safehouse, new Vector3 (0, 15, 0), Quaternion.identity);
			maptile.name = "Safehouse";
		}

		if (SceneManager.GetActiveScene ().name == "Slums") {
			
			GameObject border = Resources.Load ("Tiled2Unity/Prefabs/Border") as GameObject;
			GameObject citytile1 = Resources.Load ("Tiled2Unity/Prefabs/TownHouse1") as GameObject;
			GameObject citytile2 = Resources.Load ("Tiled2Unity/Prefabs/TownHouse1") as GameObject;
			GameObject citytile3 = Resources.Load ("Tiled2Unity/Prefabs/TownHouse1") as GameObject;
			GameObject citytile4 = Resources.Load ("Tiled2Unity/Prefabs/TownHouse2") as GameObject;
			GameObject citytile5 = Resources.Load ("Tiled2Unity/Prefabs/TownHouse2") as GameObject;
			GameObject citytile6 = Resources.Load ("Tiled2Unity/Prefabs/TownHouse2") as GameObject;
			GameObject citytile7 = Resources.Load ("Tiled2Unity/Prefabs/Motel1") as GameObject;
			GameObject citytile8 = Resources.Load ("Tiled2Unity/Prefabs/Cafe1") as GameObject;

			List<GameObject> citytiles;

			citytiles = new List<GameObject> ();
			if (citytile1 != null) {citytiles.Add (citytile1);}
			if (citytile2 != null) {citytiles.Add (citytile2);}
			if (citytile3 != null) {citytiles.Add (citytile3);}
			if (citytile4 != null) {citytiles.Add (citytile4);}
			if (citytile5 != null) {citytiles.Add (citytile5);}
			if (citytile6 != null) {citytiles.Add (citytile6);}
			if (citytile7 != null) {citytiles.Add (citytile7);}
			if (citytile8 != null) {citytiles.Add (citytile8);}

			GameObject mapborder = Instantiate (border, new Vector3 (0, 100, 0), Quaternion.identity);

			for (int i = 0; i < 4; i++) {
				for (int j = 0; j < 4; j++) {
					GameObject tile1 = Instantiate (PickRandomGameObject (citytiles), new Vector3 (i * 25, (j + 1) * 25, 0), Quaternion.identity);
				}
			}

		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
