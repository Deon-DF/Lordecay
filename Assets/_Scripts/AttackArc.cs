using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

public class AttackArc : MonoBehaviour {

	public float TTL;
	public int bluntDamage = 0;
	public int pierceDamage = 0;
	public int fireDamage = 0;
	public int coldDamage = 0;
	public int acidDamage = 0;
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
						int effectiveDMG = ((pierceDamage - enemy.pierceArmor) +
						                    (bluntDamage - enemy.bluntArmor) +
						                    (fireDamage - enemy.fireArmor) +
						                    (coldDamage - enemy.coldArmor) +
						                    (acidDamage - enemy.acidArmor));
						if (effectiveDMG > 0) {
							enemy.Stun (stunfactor);
							GlobalData.Gamelog += (Environment.NewLine + "You have hit " + enemy.name + " for " + effectiveDMG + " damage.");
							enemy.Health -= effectiveDMG;
							//Quaternion direction = Quaternion.LookRotation (Vector3.forward, collider.transform.position - transform.position);
							//collider.attachedRigidbody.velocity = direction * Vector3.one * 10000f;
							//Debug.Log ("You have hit " + enemy.name + " for " + effectiveDMG + "damage.");
						} else {
							GlobalData.Gamelog += (Environment.NewLine + "Enemy armor absorbed the hit!");
						}
						if (!enemy.isAggressive) {
							enemy.isAggressive = true;
							enemy.aggroRememberCounter = enemy.aggroRememberTime;
						}

					} else {
						Debug.Log ("Hit more than one enemy, ignoring others.");
					}
				}
				break;
			case "enemy":
				if (collider.tag == "PlayerHitbox") {
					Player player = collider.transform.parent.gameObject.GetComponent <Player> ();
					int effectiveDMG = ((bluntDamage - player.BluntArmor) +
					                    (pierceDamage - player.PierceArmor) +
					                    (fireDamage - player.FireArmor) +
					                    (coldDamage - player.ColdArmor) +
					                    (acidDamage - player.AcidArmor));
					if (effectiveDMG > 0) {
						GlobalData.Gamelog += (Environment.NewLine + "You were hit for " + effectiveDMG + " damage.");
						//Debug.Log ("You were hit for " + effectiveDMG + " damage.");
						player.Health -= effectiveDMG;
					} else {
						GlobalData.Gamelog += (Environment.NewLine + "Your armor absorbed the hit!");
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
