﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class AttackArc : MonoBehaviour {

	public float TTL;
	public int damage = 0;
	public float stunfactor = 0f;
	public string origin;
	public bool isAOE = false;
	private int enemiesHit;

	void OnTriggerEnter2D (Collider2D collider)
	{

		switch (origin) {
			case "player":
				if (collider.tag == "EnemyHitbox") {
					this.enemiesHit++;
					if (this.enemiesHit == 1 || isAOE) {
						Monster enemy = collider.transform.parent.gameObject.GetComponent <Monster> ();
						int effectiveDMG = (damage - enemy.protection);
						if (effectiveDMG > 0) {
							enemy.isStunned = true;
							enemy.stunCooldown = stunfactor;
							//Quaternion direction = Quaternion.LookRotation (Vector3.forward, collider.transform.position - transform.position);
							//collider.attachedRigidbody.velocity = direction * Vector3.one * 10000f;
							GlobalData.Gamelog += (Environment.NewLine + "You have hit " + enemy.name + " for " + effectiveDMG + " damage.");
							//Debug.Log ("You have hit " + enemy.name + " for " + effectiveDMG + "damage.");
							enemy.Health -= effectiveDMG;
						}

					}
				}
				break;
			case "enemy":
				if (collider.tag == "PlayerHitbox") {
					Player player = collider.transform.parent.gameObject.GetComponent <Player> ();
					int effectiveDMG = (damage - player.Protection);
					if (effectiveDMG > 0) {
						GlobalData.Gamelog += (Environment.NewLine + "You were hit for " + effectiveDMG + " damage.");
						//Debug.Log ("You were hit for " + effectiveDMG + " damage.");
						player.Health -= effectiveDMG;
					}
				}
				break;
			default:
				break;			
		}
	}

	void Start() {
		this.enemiesHit = 0;
	}

	// Update is called once per frame
	void Update () {
		TTL -= Time.deltaTime;
		if (TTL < 0) {
			Destroy (gameObject);
		}
		
	}
}