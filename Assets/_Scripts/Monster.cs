using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour {

	// Stats
	public int maxhealth;
	int health = 100;
	public int bluntArmor = 0;
	public int pierceArmor = 0;
	public int fireArmor = 0;
	public int coldArmor = 0;
	public int acidArmor = 0;


	// Attack capability


	public int bluntMinDamage = 0;
	public int bluntMaxDamage = 0;
	public int pierceMinDamage = 0;
	public int pierceMaxDamage = 0;
	public int fireMinDamage = 0;
	public int fireMaxDamage = 0;
	public int coldMinDamage = 0;
	public int coldMaxDamage = 0;
	public int acidMinDamage = 0;
	public int acidMaxDamage = 0;
	public float attackCooldown = 2f;
	public float attackDistance = 1f;
	public float aggroDistance = 5f;
	public float hearingDistance = 10f;
	public float aggroRememberTime = 5f;
	public bool isAggressive;
	public bool isVisibleToPlayers;

	public float aggroRememberCounter = 0;
	private float attackCooldownCounter = 0;

	AttackArc monster_attack_arc;

	// effects
	public bool isDisarmed = false;
	public float disarmCooldown = 0f;
	public bool isRooted = false;
	public float rootCooldown = 0f;
	public bool isStunned = false;
	public float stunCooldown = 0f;

	// Movement

	public float moveSpeed;
	public float roamCooldown;
	public float roamDuration;

	private float roamCooldownCounter;
	private float roamDurationCounter;
	private Vector3 moveDirection;

	// Physics and sprites
	public Rigidbody2D eRigidBody;
	private SpriteRenderer eSpriteRenderer;
	private Vector3 lastKnownPlayerPosition;


	private Animator animator;
	float lastMoveX;
	float lastMoveY;
	public bool isMoving;

	List<Tile> waypoints;
	Vector3 nextWaypoint;
	public bool isPathFinding;


	// Health system

	public int Health {

		set {
			int newhealth = value;

			if (newhealth < health) {
				health = Mathf.Max(0, value);
				StartCoroutine("registerMonsterHit");

				if (health <= 0) {
					Die();
				}

			} else {
				health = Mathf.Min(value, maxhealth);
			}

		}
		get {
			return health;
		}
	}

	IEnumerator registerMonsterHit (){
		Color c = Color.red;
		Color w = Color.white;
		//c.a = 0.2f;
		eSpriteRenderer.color = c;
		yield return new WaitForSeconds (0.2f);
		eSpriteRenderer.color = w;

	}

	// Drawing/UI

	void DrawRelative ()
	{
		eSpriteRenderer.sortingOrder = Mathf.RoundToInt (-transform.position.y * 100);
		if (isVisibleToPlayers) {
			eSpriteRenderer.enabled = true;
		} else {
			eSpriteRenderer.enabled = false;
		}
	}

	/*
	void OnMouseOver() {
		Debug.Log (gameObject.name);
		GUI.enemyName = gameObject.name;
	}*/

	// Effects

	public void Disarm (float disarmDuration) {
		isDisarmed = true;
		disarmCooldown = disarmDuration;
	}
	public void Root (float rootDuration) {
		isRooted = true;
		rootCooldown = rootDuration;
	}
	public void Stun (float stunDuration) {
		isStunned = true;
		stunCooldown = stunDuration;
	}

	// Attacking

	public void AttackPlayer (Player player) {
		if (attackCooldownCounter < 0) {
				/*Vector3 spawnPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
				Quaternion direction = Quaternion.LookRotation (Vector3.forward, transform.position - player.transform.position);
				Vector3 offset = direction * (new Vector3 (0f, 0.3f, 0f));*/
				AttackArc sweep = Instantiate (monster_attack_arc, new Vector3 (player.transform.position.x, player.transform.position.y+0.5f, 0), Quaternion.identity);
				sweep.origin = "enemy";
				sweep.TTL = 0.2f;
				sweep.pierceDamage = UnityEngine.Random.Range(pierceMinDamage, pierceMaxDamage + 1);
				sweep.bluntDamage = UnityEngine.Random.Range(bluntMinDamage, bluntMaxDamage + 1);
				attackCooldownCounter = attackCooldown;
			}
	}

	public bool RefreshAggro (Player player, int layerMask)
	{
		if (Vector3.Distance (transform.position, player.transform.position) <= aggroDistance && !Physics2D.Linecast(transform.position, player.transform.position, layerMask)) {
			if (isAggressive == false) {
				isAggressive = true;
			}
			aggroRememberCounter = aggroRememberTime;
		} else if (Vector3.Distance (transform.position, player.transform.position) < hearingDistance && player.madeLoudSound ) {
			if (isAggressive == false) {
				isAggressive = true;
			}
			aggroRememberCounter = aggroRememberTime;			
		} else {
			return false;
		}

		return true;
	}

	void RayCasting(Vector3 sightStart, Vector3 sightEnd, Color color) {
		Debug.DrawLine(sightStart, sightEnd, color);
	}

	public void Die () {
		GlobalData.Gamelog += (Environment.NewLine + name + " dies!");
		GameObject.DestroyObject (this.gameObject);

		// TODO: add corpse spawning (below is an example from a different game).
		/*GameObject corpse = Instantiate (Resources.Load ("Prefabs/Creatures/Bloodspread"), enemy.transform.position, enemy.transform.rotation) as GameObject;
		corpse.AddComponent<SpriteRenderer> ();
		corpse.GetComponent<SpriteRenderer> ().sortingOrder = 5;
		if (Random.Range (0, 2) == 0) {
			corpse.GetComponent<SpriteRenderer> ().sprite = enemy.CurrentCorpse1;
		} else {
			corpse.GetComponent<SpriteRenderer> ().sprite = enemy.CurrentCorpse2;
		}
		DropLoot (enemy);
		StartCoroutine(bloodpool(enemy.transform.position, enemy.transform.rotation));*/
		//GameSettings.enemiesKilled++;
	}

	void CheckLastDirection (Vector3 direction)
	{
		// A check to stop animation from defaulting to IDLE/0/0 when not moving; we want to make animation face the last movement position
		if (Mathf.Abs (direction.x) == Mathf.Abs (direction.y) && !isMoving) {
		} else	if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
				lastMoveY = 0f;
				if (direction.x > 0f) {
					lastMoveX = 1f;
				} else if (direction.x < 0f){
					lastMoveX = -1f;
				}
		} else {
				lastMoveX = 0f;
				if (direction.y > 0f) {
					lastMoveY = 1f;
				} else if (direction.y < 0f){
					lastMoveY = -1f;
				}
			}
			// a hack to make enemy turn to player

			animator.SetBool("IsMoving", isMoving);
			animator.SetFloat("LastMoveX", lastMoveX);
			animator.SetFloat("LastMoveY", lastMoveY);
	}

	public void TargetPlayer (Player player, int layerMask, Grid grid)
	{
		if ((Vector3.Distance (transform.position, player.transform.position) < attackDistance) && !isStunned && !isDisarmed) {
			// Stop and attack
			//Debug.Log ("The distance is " + (Vector3.Distance (transform.position, player.transform.position)));
			eRigidBody.velocity = new Vector2 (0f, 0f);
			isPathFinding = false;
			isMoving = false;
			CheckLastDirection(player.transform.position - transform.position);
			AttackPlayer (player);
		} else {
			// Move towards player
			// check if the direction is blocked
			//Debug.Log ("The distance is still " + (Vector3.Distance (transform.position, player.transform.position)) + " which is less than " + attackDistance);
			if (!Physics2D.Linecast (transform.position, player.transform.position, layerMask)) { // checking collision with any other object which is not in enemy_and_playerMask
				lastKnownPlayerPosition = player.transform.position;
				Quaternion direction = Quaternion.LookRotation (Vector3.forward, lastKnownPlayerPosition - transform.position);
				CheckLastDirection(lastKnownPlayerPosition - transform.position);
				moveDirection = direction * Vector3.up;
				if (!isStunned && !isRooted) {
					eRigidBody.velocity = moveDirection * moveSpeed;
					isPathFinding = false;
					isMoving = true;
				} else {
					eRigidBody.velocity = new Vector2 (0f, 0f);
					isPathFinding = false;
					isMoving = false;
				}
			} else { 
				if (isPathFinding == false) {
					//Debug.Log(this.name + " is pathfinding to last player position!");
					AStar astar = new AStar ();
					int monsterX = (int)Mathf.Floor (transform.position.x);
					int monsterY = (int)Mathf.Floor (transform.position.y);
					int playerX = (int)Mathf.Floor (player.transform.position.x);
					int playerY = (int)Mathf.Floor (player.transform.position.y);
					waypoints = astar.FindPath (grid.GetTileAt (monsterX, monsterY), grid.GetTileAt (playerX, playerY), grid, false);
					isPathFinding = true;
				}
			}

		}

	}

	// Refreshing cooldowns

	public void CoolDown () {
		attackCooldownCounter -= Time.deltaTime;
		if (isMoving) {
			roamDurationCounter -= Time.deltaTime;			
		} else {
			roamCooldownCounter -= Time.deltaTime;
		}
		disarmCooldown -= Time.deltaTime;
		rootCooldown -= Time.deltaTime;
		stunCooldown -= Time.deltaTime;
		if (disarmCooldown <= 0) {
			isDisarmed = false;
		}
		if (rootCooldown <= 0) {
			isRooted = false;
		}
		if (stunCooldown <= 0) {
			isStunned = false;
		}
	}

	// Moving

	public void FollowWaypoints () {
		if (waypoints.Count > 0) {
			if (waypoints.Count > 0) {
				nextWaypoint = new Vector3 (waypoints [0].X + 0.5f, waypoints [0].Y + 0.5f, 0f);

				if (Vector3.Distance (transform.position, nextWaypoint) > 0.05) {
					Quaternion direction = Quaternion.LookRotation (Vector3.forward, nextWaypoint - transform.position);
					CheckLastDirection (nextWaypoint - transform.position);
					moveDirection = direction * Vector3.up;
					if (!isStunned && !isRooted) {
						eRigidBody.velocity = moveDirection * moveSpeed;
						isMoving = true;
					} else {
						eRigidBody.velocity = new Vector2 (0f, 0f);
						isPathFinding = false;
						isMoving = false;
					}

				} else {
					waypoints.Remove (waypoints [0]);
					eRigidBody.velocity = new Vector2 (0f, 0f);
//					if (nextWaypoint == null) {
//						isPathFinding = false;
//					}
				}
			}
		} else {
			isPathFinding = false;
		}
	}

	public void Roam () {
		
		if (isMoving) {
			eRigidBody.velocity = moveDirection * moveSpeed/2;
			if (roamDurationCounter < 0) {
				isMoving = false;
				roamDurationCounter = UnityEngine.Random.Range ((roamDuration * 0.75f), (roamDuration * 1.25f));
				CheckLastDirection(moveDirection);
			}
		} else {
			eRigidBody.velocity = new Vector2 (0f, 0f);
			if (roamCooldownCounter < 0) {
				isMoving = true;
				roamCooldownCounter = UnityEngine.Random.Range ((roamCooldown * 0.75f), (roamCooldown * 1.25f));

				moveDirection = new Vector3 (UnityEngine.Random.Range (-1f, 1f), UnityEngine.Random.Range (-1f, 1f), 0f);
				CheckLastDirection(moveDirection);
			}
		}
	}

	// Boring start/update part

	void Start () {

		animator = GetComponent<Animator>();
		eRigidBody = GetComponent<Rigidbody2D> ();
		eSpriteRenderer = GetComponent<SpriteRenderer> ();
		monster_attack_arc = Resources.Load <AttackArc> ("Prefabs/UI/monster-attack-arc");
		waypoints = new List<Tile> ();

		attackCooldownCounter = attackCooldown;

		roamCooldownCounter = UnityEngine.Random.Range ((roamCooldown * 0.75f), roamCooldown * (1.25f));
		roamDurationCounter = UnityEngine.Random.Range ((roamDuration * 0.75f), roamDuration * (1.25f));
	}
	

	void Update () {
		DrawRelative ();
	}
}
