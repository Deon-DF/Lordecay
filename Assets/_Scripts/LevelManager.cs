﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager: MonoBehaviour {

	public void LoadArea () {
		GlobalData.worldmap = false;
		SceneManager.LoadScene (GlobalData.nextEntry);
	}
}
