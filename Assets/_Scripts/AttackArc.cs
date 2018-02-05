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
						int effectiveDMG = (Mathf.Max(0, (pierceDamage - enemy.pierceArmor)) +
											Mathf.Max(0, (bluntDamage - enemy.bluntArmor)) +
											Mathf.Max(0, (fireDamage - enemy.fireArmor)) +
											Mathf.Max(0, (coldDamage - enemy.coldArmor)) +
											Mathf.Max(0, (acidDamage - enemy.acidArmor)));
						if (effectiveDMG > 0) {
							enemy.Stun (stunfactor);
							Debug.Log ("Stunned enemy for " + stunfactor);
							Debug.Log ("Rolled " + pierceDamage + " pierce damange and " + bluntDamage + " blunt damage, but pierce armor is " + enemy.pierceArmor + " and blunt armor is " + enemy.bluntArmor);
							GlobalData.Gamelog += (Environment.NewLine + "You have hit " + enemy.name + " for " + effectiveDMG + " damage.");
							enemy.Health -= effectiveDMG;
							//Quaternion direction = Quaternion.LookRotation (Vector3.forward, collider.transform.position - transform.position);
							//collider.attachedRigidbody.velocity = direction * Vector3.one * 10000f;
							//Debug.Log ("You have hit " + enemy.name + " for " + effectiveDMG + "damage.");
						} else {
							Debug.Log ("Rolled " + pierceDamage + " pierce damange and " + bluntDamage + " blunt damage, but pierce armor is " + enemy.pierceArmor + " and blunt armor is " + enemy.bluntArmor);
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
					int effectiveDMG = (Mathf.Max(0, (bluntDamage - player.BluntArmor)) +
										Mathf.Max(0, (pierceDamage - player.PierceArmor)) +
										Mathf.Max(0, (fireDamage - player.FireArmor)) +
										Mathf.Max(0, (coldDamage - player.ColdArmor)) +
										Mathf.Max(0, (acidDamage - player.AcidArmor)));
					if (effectiveDMG > 0) {
						GlobalData.Gamelog += (Environment.NewLine + "You were hit for " + effectiveDMG + " damage.");
						//Debug.Log ("You were hit for " + effectiveDMG + " damage.");
						player.Health -= effectiveDMG;
				} else {
						Debug.Log ("Rolled " + pierceDamage + " pierce damange and " + bluntDamage + " blunt damage, but pierce armor is " + player.PierceArmor + " and blunt armor is " + player.BluntArmor);
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
