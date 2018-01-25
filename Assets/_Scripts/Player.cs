using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	// masks for layers
	int playerMask;
	int enemyMask; 
	int lineOfSightMask;

	// Player stats

	public int strength = 5;
	public int perception = 5;
	public int intelligence = 5;
	public int toughness = 5;

	// Depletable resources
	int maxhealth = 100;
	int health;
	int maxsanity = 100;
	int sanity;
	float maxstamina = 100;
	float stamina;

	int bluntMinDamage = 3;
	int bluntMaxDamage = 5;

	int pierceMinDamage = 0;
	int pierceMaxDamage = 0;

	int fireMinDamage = 0;
	int fireMaxDamage = 0;

	int coldMinDamage = 0;
	int coldMaxDamage = 0;

	int acidMinDamage = 0;
	int acidMaxDamage = 0;

	int bluntArmor = 0;
	int pierceArmor = 0;
	int fireArmor = 0;
	int coldArmor = 0;
	int acidArmor = 0;

	float moveSpeed = 4;
	float moveWeightModifier = 1;
	float moveSprintModifier = 1;
	float moveStaminaDrain = 20;
	float staminaRecovery = 20;
	float moveStaminaRecovery = 5;

	// Attack
	private float attackCooldownCounter;
	private bool isAttacking = false;
	private bool isAiming = false;

	public bool madeLoudSound = false;
	private float madeLoudSoundDuration = 1f;
	private float madeLoudSoundCounter;

	// Physics
	public Rigidbody2D pRigidbody;

	// Animation
	private Animator animator;
	private bool isMoving;
	private Vector2 lastMove;
	private static bool playerExists = false;
	private SpriteRenderer pSpriteRenderer;
	private SpriteRenderer helmetSpriteRenderer;
	private SpriteRenderer armorSpriteRenderer;
	private SpriteRenderer pantsSpriteRenderer;
	private SpriteRenderer bootsSpriteRenderer;

	private SpriteRenderer swordSpriteRenderer;
	private SpriteRenderer pistolSpriteRenderer;

	// World

	public Grid grid;

	// UI and menus
	public int aspectRatioX = 16;
	public int aspectRatioY = 9;
	private bool isPathChecking = false;

	GameObject overlay;
	AttackArc player_attack_shot;
	AttackArc player_attack_arc_short;
	AttackArc player_attack_arc_medium;
	AttackArc player_attack_arc_long;

	// Inventory
	public List<Item> inventory = new List<Item> ();

	Item weapon = GlobalData.punch;
	public Item offhand = GlobalData.empty_offhand;
	public Item helmet = GlobalData.naked_head;
	public Item bodyarmor = GlobalData.naked_body;
	public Item pants = GlobalData.naked_legs;
	public Item boots = GlobalData.naked_feet;

	public Item Weapon {
		get {
			return weapon;
		}
		set {
			weapon = value;
			SetDamageValues(Weapon);
		}
	}

	public float MoveSpeed {
		set {
			moveSpeed = value;
		}
		get {
			return moveSpeed * moveWeightModifier * moveSprintModifier;
		}
	}


//	List<Tile> waypoints;
//	Vector3 nextWaypoint;

// Health system

	public int Health {
		set {
			int newhealth = value;
			if (newhealth < health) {
				StartCoroutine (registerPlayerHit ());
				health = Mathf.Max (value, 0);
			} else {
				health = Mathf.Min (value, maxhealth);
			}
		}

		get {
			return health;
		}
	}

	public int MaxHealth {
		set {
			maxhealth = value;
		}

		get {
			return maxhealth;
		}
	}

	public int Sanity {
		set {
			float newsanity = value;
			if (newsanity < sanity) {
				sanity = Mathf.Max (value, 0);
			} else {
				sanity = Mathf.Min (value, maxsanity);
			}
		}

		get {
			return sanity;
		}

	}

	public int MaxSanity {
		set {
			maxsanity = value;
		}

		get {
			return maxsanity;
		}
	}

	public float Stamina {
		set {
			float newstamina = value;
			if (newstamina < stamina) {
				stamina = Mathf.Max (value, 0);
			} else {
				stamina = Mathf.Min (value, maxstamina);
			}
		}

		get {
			return stamina;
		}

	}

	public float MaxStamina {
		set {
			maxstamina = value;
		}

		get {
			return maxstamina;
		}
	}

	IEnumerator registerPlayerHit() {
		Image oImage = overlay.GetComponent<Image> ();
		Color c = Color.red;
		c.a = 0.2f;
		oImage.color = c;
		overlay.SetActive (true);
		yield return new WaitForSeconds (weapon.attackcooldown);
		overlay.SetActive (false);
		oImage.color = Color.white;
	}

// ARMOR TYPES - PUBLIC

	public int BluntArmor {
		get {
			return bluntArmor;
		}

		set {
			if (value < 0) {
				value = 0;
			}
			bluntArmor = value;
		}

	}

	public int PierceArmor {
		get {
			return pierceArmor;
		}

		set {
			if (value < 0) {
				value = 0;
			}
			pierceArmor = value;
		}

	}

	public int FireArmor {
		get {
			return fireArmor;
		}

		set {
			if (value < 0) {
				value = 0;
			}
			fireArmor = value;
		}

	}

	public int ColdArmor {
		get {
			return coldArmor;
		}

		set {
			if (value < 0) {
				value = 0;
			}
			coldArmor = value;
		}
	}

	public int AcidArmor {
		get {
			return acidArmor;
		}

		set {
			if (value < 0) {
				value = 0;
			}
			acidArmor = value;
		}

	}

// ARMOR TYPES END

// DAMAGE TYPES - PUBLIC

	public int BluntMinDamage {
		get {
			return bluntMinDamage;
		}
		set {
			bluntMinDamage = value;
		}
	}

	public int BluntMaxDamage {
		get {
			return bluntMaxDamage;
		}
		set {
			bluntMaxDamage = value;
		}
	}

	public int PierceMinDamage {
		get {
			return pierceMinDamage;
		}
		set {
			pierceMinDamage = value;
		}
	}

	public int PierceMaxDamage {
		get {
			return pierceMaxDamage;
		}
		set {
			pierceMaxDamage = value;
		}
	}
	public int FireMinDamage {
		get {
			return fireMinDamage;
		}
		set {
			fireMinDamage = value;
		}
	}

	public int FireMaxDamage {
		get {
			return fireMaxDamage;
		}
		set {
			fireMaxDamage = value;
		}
	}

	public int ColdMinDamage {
		get {
			return coldMinDamage;
		}
		set {
			coldMinDamage = value;
		}
	}

	public int ColdMaxDamage {
		get {
			return coldMaxDamage;
		}
		set {
			coldMaxDamage = value;
		}
	}

	public int AcidMinDamage {
		get {
			return acidMinDamage;
		}
		set {
			acidMinDamage = value;
		}
	}

	public int AcidMaxDamage {
		get {
			return acidMaxDamage;
		}
		set {
			acidMaxDamage = value;
		}
	}
// DAMAGE TYPES END

	void SetDamageValues (Item equipitem) {
		BluntMinDamage = equipitem.bluntMinDamage;
		BluntMaxDamage = equipitem.bluntMaxDamage;
		PierceMinDamage = equipitem.pierceMinDamage;
		PierceMaxDamage = equipitem.pierceMaxDamage;
		FireMinDamage = equipitem.fireMinDamage;
		FireMaxDamage = equipitem.fireMaxDamage;
		ColdMinDamage = equipitem.coldMinDamage;
		ColdMaxDamage = equipitem.coldMaxDamage;
		AcidMinDamage = equipitem.acidMinDamage;
		AcidMaxDamage = equipitem.acidMaxDamage;
	}

// GUI

	void DrawRelative () {
		pSpriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);

		if (helmet != GlobalData.naked_head) {
			helmetSpriteRenderer.enabled = true;
			helmetSpriteRenderer.sortingOrder = pSpriteRenderer.sortingOrder + 1;
			helmetSpriteRenderer.color = helmet.color;
		} else {
			helmetSpriteRenderer.enabled = false;
		}

		if (bodyarmor != GlobalData.naked_body) {
			armorSpriteRenderer.enabled = true;
			armorSpriteRenderer.sortingOrder = pSpriteRenderer.sortingOrder + 1;
		} else {
			armorSpriteRenderer.enabled = false;
		}

		if (pants != GlobalData.naked_legs) {
			pantsSpriteRenderer.enabled = true;
			pantsSpriteRenderer.sortingOrder = pSpriteRenderer.sortingOrder + 1;
		} else {
			pantsSpriteRenderer.enabled = false;
		}

		if (boots != GlobalData.naked_feet) {
			bootsSpriteRenderer.enabled = true;
			bootsSpriteRenderer.sortingOrder = pSpriteRenderer.sortingOrder + 2;
		} else {
			bootsSpriteRenderer.enabled = false;
		}

		// Draw weapons

		if (weapon.spritetype == Item.SpriteType.Sword) {
			swordSpriteRenderer.enabled = true;
			swordSpriteRenderer.sortingOrder = pSpriteRenderer.sortingOrder + 3;
		} else {
			swordSpriteRenderer.enabled = false;
		}

		if (weapon.spritetype == Item.SpriteType.Pistol) {
			pistolSpriteRenderer.enabled = true;
			pistolSpriteRenderer.sortingOrder = pSpriteRenderer.sortingOrder + 3;
		} else {
			pistolSpriteRenderer.enabled = false;
		}
	}

	public bool IsPathChecking {
		get {
			return isPathChecking;
		}

		set {
			isPathChecking = value;
			if (IsPathChecking) {
				foreach (GameObject tileobject in GameObject.FindGameObjectsWithTag("tile_go")) {
					tileobject.transform.Find("cell").gameObject.GetComponent<SpriteRenderer> ().enabled = true;
				}
			} else {
				foreach (GameObject tileobject in GameObject.FindGameObjectsWithTag("tile_go")) {
					tileobject.transform.Find("cell").gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				}
			}

		}
	}

// COOLDOWN ACTIONS

	public void Cooldown () {
		attackCooldownCounter -= Time.deltaTime;
		if (attackCooldownCounter <= 0) {
			isAttacking = false;
		} else {
		}
		madeLoudSoundCounter -= Time.deltaTime;
		if (madeLoudSoundCounter < 0) {
			madeLoudSound = false;
		}
	}

// CONTROLS 

	public void AltHighLight () {

		if (GlobalData.debugmode) {

			if (Input.GetKeyDown (KeyCode.LeftAlt)) {
				IsPathChecking = true;
			}

			if (Input.GetKeyUp (KeyCode.LeftAlt)) {
				IsPathChecking = false;
			}
		}
	}

	// TODO remove/hide the below function which currently serve to give items for free, for testing purposes

	public void testFreeStuff () { 
		if (Input.GetKeyDown (KeyCode.H)) {
			Item newItem = new Item(Item.Type.Weapon, "Baseball bat");// actually gives baseball bat
			Item newItem2 = new Item(Item.Type.Weapon, "Sword");// actually gives sword
			Item newItem3 = new Item(Item.Type.Weapon, "Pistol");// actually gives pistol
			Item newItem4 = new Item(Item.Type.Offhand, "Riot shield");// actually gives riot shield
			Item newItem5 = new Item(Item.Type.Helmet, "Army helmet");// actually gives army helmet
			Item newItem6 = new Item(Item.Type.Bodyarmor, "Kevlar vest");// actually gives kevlar vest
			Item newItem7 = new Item(Item.Type.Consumable, "MedKit");//GlobalData.MedKit;
			Item newItem8 = new Item(Item.Type.Pants, "Camo pants");// actually gives camo pants
			Item newItem9 = new Item(Item.Type.Boots, "Leather boots");// actually gives leather boots
			getItem (newItem);
			getItem (newItem2);
			getItem (newItem3);
			getItem (newItem4);
			getItem (newItem5);
			getItem (newItem6);
			getItem (newItem7);
			getItem (newItem8);
			getItem (newItem9);
		}
	}


	public void KeyboardControls ()
	{
		if (Input.GetKey (KeyCode.LeftShift) && isMoving) {
			Stamina -= moveStaminaDrain * Time.deltaTime;
			if (Stamina > 0) {
				moveSprintModifier = 1.5f;
			} else {
				moveSprintModifier = 1f;
			}
		} else {
			if (Stamina < MaxStamina) {
				if (isMoving) {
					Stamina += moveStaminaRecovery * Time.deltaTime;					
				} else {
					Stamina += staminaRecovery * Time.deltaTime;
				}
			}
			moveSprintModifier = 1f;
		}
	}

	public void MouseControls ()
	{
		if (Input.GetKeyDown (KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1)) {
			if (attackCooldownCounter < 0) {
				if (weapon.attacktype == Item.AttackType.Melee) {
					Vector2 mousePosition = new Vector2 (Input.mousePosition.x / Screen.width * aspectRatioX, Input.mousePosition.y / Screen.height * aspectRatioY);
					Vector2 centerPosition = new Vector2 (8.0f, 4.5f);
					Vector3 spawnPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
					Quaternion direction = Quaternion.LookRotation (Vector3.forward, centerPosition - mousePosition);
					Vector3 offset = direction * (new Vector3 (0f, 0f, 0f));
					//Debug.Log (mousePosition.x + " " + mousePosition.y); // checking mouse position at click
					AttackArc sweep;

					if (weapon.attackrange == "short") {
						sweep = Instantiate (player_attack_arc_short, spawnPosition - offset, direction);
					} else if (weapon.attackrange == "medium") {
						sweep = Instantiate (player_attack_arc_medium, spawnPosition - offset, direction);
					} else {
						sweep = Instantiate (player_attack_arc_long, spawnPosition - offset, direction);
					}

					sweep.origin = "player";
					sweep.TTL = 0.2f;
					sweep.bluntDamage = Random.Range (BluntMinDamage, BluntMaxDamage + 1);
					sweep.pierceDamage = Random.Range (PierceMinDamage, PierceMaxDamage + 1);
					sweep.fireDamage = Random.Range (FireMinDamage, FireMaxDamage + 1);
					sweep.coldDamage = Random.Range (ColdMinDamage, ColdMaxDamage + 1);
					sweep.acidDamage = Random.Range (AcidMinDamage, AcidMaxDamage + 1);
					sweep.stunfactor = Weapon.stunfactor * (1 + (strength - 5)*GlobalData.stunfactorPerStrength);
					sweep.isAOE = Weapon.isAOE;
					attackCooldownCounter = weapon.attackcooldown;
					isAttacking = true;

					if (weapon.isLoud) {
						madeLoudSound = true;
						madeLoudSoundCounter = madeLoudSoundDuration;
					}

				} else if (weapon.attacktype == Item.AttackType.RangedSingle){ 
					Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint (mousePosition);

					Vector3 targetPosition = new Vector3 (mouseInWorld.x, mouseInWorld.y, 0f);
					AttackArc sweep;

					if (!Physics2D.Linecast (transform.position, targetPosition, lineOfSightMask)) {

					
						sweep = Instantiate (player_attack_shot, targetPosition, Quaternion.identity);

						sweep.origin = "player";
						sweep.TTL = 0.2f;
						sweep.bluntDamage = Random.Range (BluntMinDamage, BluntMaxDamage + 1);
						sweep.pierceDamage = Random.Range (PierceMinDamage, PierceMaxDamage + 1);
						sweep.fireDamage = Random.Range (FireMinDamage, FireMaxDamage + 1);
						sweep.coldDamage = Random.Range (ColdMinDamage, ColdMaxDamage + 1);
						sweep.acidDamage = Random.Range (AcidMinDamage, AcidMaxDamage + 1);
						sweep.stunfactor = Weapon.stunfactor;
						sweep.isAOE = Weapon.isAOE;
						attackCooldownCounter = weapon.attackcooldown;
						isAttacking = true;

						if (weapon.isLoud) {
							madeLoudSound = true;
							madeLoudSoundCounter = madeLoudSoundDuration;
						}
					} else {
						Debug.Log ("Cannot shoot outside of line of sight!");
						DrawLine(transform.position, targetPosition, Color.red, 0.1f);
					}
				}
			}
		}

		/* if (Input.GetKeyDown (KeyCode.Mouse1)) {
			madeLoudSound = true;
			madeLoudSoundCounter = madeLoudSoundDuration;
		} */

		if (Input.GetKey (KeyCode.Mouse1)) {
			isAiming = true;
		} else {
			isAiming = false;
		}
	}

	void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 1f)
	{
		GameObject myLine = new GameObject();
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.SetColors(color, color);
		lr.SetWidth(0.1f, 0.1f);
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
		lr.startWidth = 0.02f;
		lr.endWidth = 0.02f;
		lr.sortingLayerName = "LineRenderer";
		GameObject.Destroy(myLine, duration);
	}

/*	public void ClickToPath (Grid grid) {

		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint (mousePosition);
			//Debug.Log (mouseInWorld.x + " and " + mouseInWorld.y);

			int playerX = (int)Mathf.Floor (transform.position.x);
			int playerY = (int)Mathf.Floor (transform.position.y);

			int mouseX = (int)Mathf.Floor (mouseInWorld.x);
			int mouseY = (int)Mathf.Floor (mouseInWorld.y);

			AStar astar = new AStar ();
			waypoints = astar.FindPath(grid.GetTileAt(playerX, playerY), grid.GetTileAt(mouseX, mouseY), grid, false);
			//waypoints.Remove (waypoints [0]);

		}
	}
	public void MovePlayerToPosition (Vector3 position) {
			Quaternion direction = Quaternion.LookRotation (Vector3.forward, position - transform.position);
			pRigidbody.velocity = direction * Vector3.up * moveSpeed;
	}*/

	public void MovementControls () {
		isMoving = false;
		Vector2 dirX = new Vector2 (0f, 0f);
		Vector2 dirY = new Vector2 (0f, 0f);

		if (!isAttacking && !isAiming) {
			if (Input.GetAxisRaw ("Horizontal") > 0.5f || Input.GetAxisRaw ("Horizontal") < -0.5f) {
				//transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
				// We use physics velocity for movement to avoid "jerky bump" into colliders
				dirX = new Vector2 (Input.GetAxisRaw ("Horizontal"), 0f);
				isMoving = true;
				lastMove = new Vector2 (Input.GetAxisRaw ("Horizontal"), 0f);
			}

			if (Input.GetAxisRaw ("Vertical") > 0.5f || Input.GetAxisRaw ("Vertical") < -0.5f) {
				//transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
				// We use physics velocity for movement to avoid "jerky bump" into colliders
				dirY = new Vector2 (0f, Input.GetAxisRaw ("Vertical"));
				isMoving = true;
				lastMove = new Vector2 (0f, Input.GetAxisRaw ("Vertical"));
			}
			/*
			if (Input.GetAxisRaw ("Horizontal") < 0.5f && Input.GetAxisRaw ("Horizontal") > -0.5) {
				pRigidbody.velocity = new Vector2 (0f, pRigidbody.velocity.y);
			}

			if (Input.GetAxisRaw ("Vertical") < 0.5f && Input.GetAxisRaw ("Vertical") > -0.5) {
				pRigidbody.velocity = new Vector2 (pRigidbody.velocity.x, 0f);
			}*/

			pRigidbody.velocity = (dirX + dirY).normalized * MoveSpeed;
		} else {
			pRigidbody.velocity = new Vector2 (0f, 0f); // Do not move while attacking
		}

		if (isAiming) {
			Vector2 mousePosition = new Vector2 (Input.mousePosition.x / Screen.width * aspectRatioX, Input.mousePosition.y / Screen.height * aspectRatioY);
			Vector2 centerPosition = new Vector2 (8.0f, 4.5f);
			//Quaternion direction = Quaternion.LookRotation (Vector3.forward, centerPosition - mousePosition);
			if (Mathf.Abs(mousePosition.x - centerPosition.x) > Mathf.Abs(mousePosition.y - centerPosition.y)) {
				//Debug.Log (Mathf.Abs(mousePosition.x - centerPosition.x) + " is greater than " + Mathf.Abs(mousePosition.y - centerPosition.y));
				if (mousePosition.x > centerPosition.x) {
					lastMove.x = 1;
					lastMove.y = 0;
				} else {
					lastMove.x = -1;
					lastMove.y = 0;
				}
			} else {
				//Debug.Log (Mathf.Abs(mousePosition.x - centerPosition.x) + " is less than " + Mathf.Abs(mousePosition.y - centerPosition.y));
				if (mousePosition.y > centerPosition.y) {
					lastMove.x = 0;
					lastMove.y = 1;
				} else {
					lastMove.x = 0;
					lastMove.y = -1;
				}
			}
		}

		animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
		animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
		animator.SetBool("IsMoving", isMoving);
		animator.SetFloat("LastMoveX", lastMove.x);
		animator.SetFloat("LastMoveY", lastMove.y);
		animator.SetBool("IsAiming", isAiming);

	}

	public void PauseControls () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			GlobalData.paused = !GlobalData.paused;
			GlobalData.inventoryON = !GlobalData.inventoryON;
			GlobalData.groundSlotOffset = 0;
		}
	}

// Grid interaction
	public Tile GetPlayerTile (Grid grid) {
		int x = (int)Mathf.Floor (transform.position.x);
		int y = (int)Mathf.Floor (transform.position.y);

		return grid.GetTileAt (x, y);		
	}

// Inventory section //

	// Equipment buttons, remove an item from equipment and put it into inventory
	public void EquipmentButtonClicked (string slotname) {
		//Debug.Log ("Interacting with an item in slot " + slotname);
		UnequipItemBySlotname (slotname);
	}

	// Inventory buttons, remove an item from inventory and put it on (in equipment slots)
	public void InventoryButtonClicked (int index) {
		//Debug.Log ("Interacting with an index " + index);
		if (inventory [index].type == Item.Type.Consumable) {
			inventory [index].Use(this, index);
		} else {
			equipItemByIndex (index);
		}
	}

	// Red cross button next to equipment/inventory slots, drops an item onto the ground
	void DropItemOnGround (Item item) {
		Tile currenttile = GetPlayerTile(grid);
		currenttile.groundItems.Add (item);
		if (currenttile.groundItems.Count > 0) {
			GameObject.Find ("Tile_" + currenttile.X + "_" + currenttile.Y).GetComponent <SpriteRenderer> ().enabled = true;
		} else {
			GameObject.Find ("Tile_" + currenttile.X + "_" + currenttile.Y).GetComponent <SpriteRenderer> ().enabled = false;
		}
	}

	// Ground inventory handling

	// Increase/decrease of a global counter to display items on the ground, because there's only 5 slots
	public void DecreaseGroundSlotdOffset () {
		GlobalData.groundSlotOffset = Mathf.Max (0, GlobalData.groundSlotOffset - 1);
	}

	public void IncreaseGroundSlotOffset () {
		int numItemsOnGround = GetPlayerTile (grid).groundItems.Count;

		GlobalData.groundSlotOffset = Mathf.Min (numItemsOnGround-5, GlobalData.groundSlotOffset + 1);
		if (GlobalData.groundSlotOffset < 0) {
			GlobalData.groundSlotOffset = 0;
		}
	}

	// Ground slot buttons to pickup items from the ground

	public void PickupItemsByIndex (int index) {
		if (inventory.Count < GlobalData.inventorySize) {
			Tile currenttile = GetPlayerTile(grid);
			int realindex = index + GlobalData.groundSlotOffset;

			if (currenttile.groundItems.Count > realindex) {
				if (currenttile.groundItems.Count > 0) {
					inventory.Add (currenttile.groundItems [realindex]);
					currenttile.groundItems.Remove (currenttile.groundItems [realindex]);
					DecreaseGroundSlotdOffset();
				}
				if (currenttile.groundItems.Count == 0) {
					GameObject.Find ("Tile_" + currenttile.X + "_" + currenttile.Y).GetComponent <SpriteRenderer> ().enabled = false;
				}
			} else {
				Debug.LogError ("Tried to reference an index outside of number of items on the ground in PickupItemsByIndex: " + realindex + ", actual number of items on the ground: " + currenttile.groundItems.Count);
			}

		} else {
			Debug.Log ("Inventory is full!");
		}
	}

	public void dropItemByIndex (int index) {
		Debug.Log ("Dropping item: " + inventory [index].name);
		DropItemOnGround (inventory [index]);
		inventory.Remove (inventory[index]);
	}

	public void dropItemBySlotname (string slotname) {

		switch (slotname) {

		case "helmet": 
			DropItemOnGround (helmet);
			Debug.Log ("Dropping item from equipment slot: " + slotname + ", item name: " + helmet.name);
			BluntArmor -= helmet.bluntArmor;
			PierceArmor -= helmet.pierceArmor;
			FireArmor -= helmet.fireArmor;
			ColdArmor -= helmet.coldArmor;
			AcidArmor -= helmet.acidArmor;
			helmet = GlobalData.naked_head;
			break;
		case "bodyarmor":
			DropItemOnGround (bodyarmor);
			Debug.Log ("Dropping item from equipment slot: " + slotname + ", item name: " + bodyarmor.name);
			BluntArmor -= bodyarmor.bluntArmor;
			PierceArmor -= bodyarmor.pierceArmor;
			FireArmor -= bodyarmor.fireArmor;
			ColdArmor -= bodyarmor.coldArmor;
			AcidArmor -= bodyarmor.acidArmor;
			bodyarmor = GlobalData.naked_body;
			break;
		case "pants":
			DropItemOnGround (pants);
			Debug.Log ("Dropping item from equipment slot: " + slotname + ", item name: " + pants.name);
			BluntArmor -= pants.bluntArmor;
			PierceArmor -= pants.pierceArmor;
			FireArmor -= pants.fireArmor;
			ColdArmor -= pants.coldArmor;
			AcidArmor -= pants.acidArmor;
			pants = GlobalData.naked_legs;
			break;
		case "boots":
			DropItemOnGround (boots);
			Debug.Log ("Dropping item from equipment slot: " + slotname + ", item name: " + boots.name);
			BluntArmor -= boots.bluntArmor;
			PierceArmor -= boots.pierceArmor;
			FireArmor -= boots.fireArmor;
			ColdArmor -= boots.coldArmor;
			AcidArmor -= boots.acidArmor;
			boots = GlobalData.naked_feet;
			break;
		case "Weapon":
			DropItemOnGround (Weapon);
			Debug.Log ("Dropping item from equipment slot: " + slotname + ", item name: " + Weapon.name);
			BluntArmor -= Weapon.bluntArmor;
			PierceArmor -= Weapon.pierceArmor;
			FireArmor -= Weapon.fireArmor;
			ColdArmor -= Weapon.coldArmor;
			AcidArmor -= Weapon.acidArmor;
			Weapon = GlobalData.punch;
			break;
		case "offhand":
			DropItemOnGround (offhand);
			Debug.Log ("Dropping item from equipment slot: " + slotname + ", item name: " + offhand.name);
			BluntArmor -= offhand.bluntArmor;
			PierceArmor -= offhand.pierceArmor;
			FireArmor -= offhand.fireArmor;
			ColdArmor -= offhand.coldArmor;
			AcidArmor -= offhand.acidArmor;
			offhand = GlobalData.empty_offhand;
			break;
		}
	}

	void equipItemByIndex (int index) {
		if (inventory[index].isEquippable) {
			Debug.Log ("Equipped item from inventory: " + inventory[index].name);

			Item equipitem = inventory[index];

			BluntArmor += equipitem.bluntArmor;
			PierceArmor += equipitem.pierceArmor;
			FireArmor += equipitem.fireArmor;
			ColdArmor += equipitem.coldArmor;
			AcidArmor += equipitem.acidArmor;


			if (inventory [index].type == Item.Type.Weapon) {
				if (Weapon != GlobalData.punch) {
					if (UnequipItem (Weapon)) {
						Weapon = inventory [index];
						Debug.Log ("Equipped new Weapon: " + Weapon.name);
						Debug.Log ("Removing item " + inventory [index].name + " from inventory");
						inventory.Remove (inventory [index]);
					}
				} else {
					Weapon = inventory [index];
					Debug.Log ("Equipped new Weapon: " + Weapon.name);
					Debug.Log ("Removing item " + inventory [index].name + " from inventory");
					inventory.Remove (inventory [index]);
				}
			} else if (inventory [index].type == Item.Type.Offhand) {
				if (offhand != GlobalData.empty_offhand) {
					if (UnequipItem (offhand)) {
						offhand = inventory [index];
						Debug.Log ("Equipped new offhand: " + offhand.name);
						Debug.Log ("Removing item " + inventory [index].name + " from inventory");
						inventory.Remove (inventory [index]);
					}
				} else {
					offhand = inventory [index];
					Debug.Log ("Equipped new offhand: " + offhand.name);
					Debug.Log ("Removing item " + inventory [index].name + " from inventory");
					inventory.Remove (inventory [index]);
				}
			} else if (inventory [index].type == Item.Type.Helmet) {
				if (helmet != GlobalData.naked_head) {
					if (UnequipItem (helmet)) {
						helmet = inventory [index];
						Debug.Log ("Equipped new helmet: " + helmet.name);
						Debug.Log ("Removing item " + inventory [index].name + " from inventory");
						inventory.Remove (inventory [index]);
					}
				} else {
					helmet = inventory [index];
					Debug.Log ("Equipped new helmet: " + helmet.name);
					Debug.Log ("Removing item " + inventory [index].name + " from inventory");
					inventory.Remove (inventory [index]);
				}
			} else if (inventory [index].type == Item.Type.Bodyarmor) {
				if (bodyarmor != GlobalData.naked_body) {
					if (UnequipItem (bodyarmor)) {
						bodyarmor = inventory [index];
						Debug.Log ("Equipped new bodyarmor: " + bodyarmor.name);
						Debug.Log ("Removing item " + inventory [index].name + " from inventory");
						inventory.Remove (inventory [index]);
					}
				} else {
					bodyarmor = inventory [index];
					Debug.Log ("Equipped new bodyarmor: " + bodyarmor.name);
					Debug.Log ("Removing item " + inventory [index].name + " from inventory");
					inventory.Remove (inventory [index]);
				}
			} else if (inventory [index].type == Item.Type.Pants) {
				if (pants != GlobalData.naked_legs) {
					if (UnequipItem (pants)) {
						pants = inventory [index];
						Debug.Log ("Equipped new pants: " + pants.name);
						Debug.Log ("Removing item " + inventory [index].name + " from inventory");
						inventory.Remove (inventory [index]);
					}
				} else {
					pants = inventory [index];
					Debug.Log ("Equipped new pants: " + pants.name);
					Debug.Log ("Removing item " + inventory [index].name + " from inventory");
					inventory.Remove (inventory [index]);
				}
			} else if (inventory [index].type == Item.Type.Boots) {
				if (boots != GlobalData.naked_feet) {
					if (UnequipItem (boots)) {
						boots = inventory [index];
						Debug.Log ("Equipped new boots: " + boots.name);
						Debug.Log ("Removing item " + inventory [index].name + " from inventory");
						inventory.Remove (inventory [index]);
					}
				} else {
					boots = inventory [index];
					Debug.Log ("Equipped new boots: " + boots.name);
					Debug.Log ("Removing item " + inventory [index].name + " from inventory");
					inventory.Remove (inventory [index]);
				}
			} else {
				Debug.LogError ("Attempted to equip an item marked as 'equippable' and yet it's none of the known equippable item types: Weapon, Offhand, Helmet, bodyarmor, Pants, Boots. Item name: " + inventory[index].name);
			}
		} else {
			Debug.Log ("Item " + inventory[index].name + " cannot be equipped due its nature.");
		}
	}

	void getItem (Item item) {
		if (inventory.Count < GlobalData.inventorySize) {
			inventory.Add (item);
			Debug.Log ("Received an item in inventory: " + item.name);
		} else {
			Debug.Log ("Inventory is full");
			DropItemOnGround (item);
		}
	}

	bool UnequipItem (Item item) {
		bool success = false;
		if (inventory.Count < GlobalData.inventorySize) {
			Debug.Log ("Unequipping and adding to inventory: " + item.name);
			inventory.Add(item);
			BluntArmor -= item.bluntArmor;
			PierceArmor -= item.pierceArmor;

			if (item.type == Item.Type.Weapon) {
				Weapon = GlobalData.punch;
			}

			if (item.type == Item.Type.Offhand) {
				offhand = GlobalData.empty_offhand;
			}


			if (item.type == Item.Type.Helmet) {
				helmet = GlobalData.naked_head;
			}

			if (item.type == Item.Type.Bodyarmor) {
				bodyarmor = GlobalData.naked_body;
			}

			if (item.type == Item.Type.Pants) {
				pants = GlobalData.naked_legs;
			}


			if (item.type == Item.Type.Boots) {
				boots = GlobalData.naked_feet;
			}
			success = true;
		} else {
			success = false;
			Debug.Log("Inventory is full, cannot unequip! Free up some inventory space first.");
		}
		return success;
	}

	void UnequipItemBySlotname (string slotname) {
		Item item = null;

		if (inventory.Count < GlobalData.inventorySize) {

			switch (slotname) {
			case "helmet":
				item = helmet;
				break;
			case "bodyarmor":
				item = bodyarmor;
				break;
			case "pants":
				item = pants;
				break;
			case "boots":
				item = boots;
				break;
			case "weapon":
				item = Weapon;
				break;
			case "offhand":
				item = offhand;
				break;
			}

			if (item.type == Item.Type.Weapon) {
				Weapon = GlobalData.punch;
				Debug.Log ("Unequipping and adding to inventory: " + item.name);
				inventory.Add (item);
			}

			if (item.type == Item.Type.Offhand) {
				offhand = GlobalData.empty_offhand;
				Debug.Log ("Unequipping and adding to inventory: " + item.name);
				inventory.Add (item);
			}


			if (item.type == Item.Type.Helmet) {
				helmet = GlobalData.naked_head;
				Debug.Log ("Unequipping and adding to inventory: " + item.name);
				inventory.Add (item);
			}

			if (item.type == Item.Type.Bodyarmor) {
				bodyarmor = GlobalData.naked_body;
				Debug.Log ("Unequipping and adding to inventory: " + item.name);
				inventory.Add (item);
			}

			if (item.type == Item.Type.Pants) {
				pants = GlobalData.naked_legs;
				Debug.Log ("Unequipping and adding to inventory: " + item.name);
				inventory.Add (item);
			}


			if (item.type == Item.Type.Boots) {
				boots = GlobalData.naked_feet;
				Debug.Log ("Unequipping and adding to inventory: " + item.name);
				inventory.Add (item);
			}


			BluntArmor -= item.bluntArmor;
			PierceArmor -= item.pierceArmor;
		} else {
			Debug.Log("Inventory is full, cannot unequip! Free up some inventory space first.");
		}
	}

	void RecalculateStatsInfluence() {
		maxhealth = 100 + (toughness - 5) * GlobalData.healthPerToughness;
		maxsanity = 100 + (intelligence - 5) * GlobalData.sanityPerIntelligence;
		maxstamina = 100 + (toughness - 5) * GlobalData.staminaPerToughness;
	}

// Awake/Start/update functions

	void Awake() {
		maxhealth = 100 + (toughness - 5) * GlobalData.healthPerToughness;
		health = maxhealth;

		maxsanity = 100 + (intelligence - 5) * GlobalData.sanityPerIntelligence;
		sanity = maxsanity;

		maxstamina = 100 + (toughness - 5) * GlobalData.staminaPerToughness;
		stamina = maxstamina;

		playerMask = 1 << LayerMask.NameToLayer ("Player"); // enemies dont block visibility of other enemies, and player collider should not block the visibility of the player,
		enemyMask = 1 << LayerMask.NameToLayer ("Enemy");   // so we "revert" these two masks with ~ to skip them from check in line of sight check.
		lineOfSightMask = ~(playerMask | enemyMask);
	}

	void Start () {
		// Inventory
		/*
		for (int i = 0; i < 25; i++) {
			Item emptyItem = GlobalData.nothing;
			inventory.Add(emptyItem);
			Debug.Log (emptyItem.name + " added to inventory to slot " + i);
		}

		for (int i = 0; i < 25; i++) {
			Debug.Log ("Item in inventory slot " + i + ": " + inventory [i].name);
		}*/

		// GUI
		overlay = GameObject.Find ("Hiteffect");
		overlay.SetActive(false);

		// Pathfinding
//		waypoints = new List<Tile>();

		// Attack arcs
		player_attack_shot = Resources.Load<AttackArc> ("Prefabs/UI/shot");
		player_attack_arc_short = Resources.Load<AttackArc> ("Prefabs/UI/player-attack-arc-short");
		player_attack_arc_medium = Resources.Load<AttackArc> ("Prefabs/UI/player-attack-arc-medium");
		player_attack_arc_long = Resources.Load<AttackArc> ("Prefabs/UI/player-attack-arc-long");

		// Animation/physics
		animator = GetComponent<Animator>();
		pRigidbody = GetComponent<Rigidbody2D> ();
		pSpriteRenderer = GetComponent<SpriteRenderer> ();
		helmetSpriteRenderer = transform.Find("Helmet").gameObject.GetComponent <SpriteRenderer> ();
		armorSpriteRenderer = transform.Find("Armor").gameObject.GetComponent <SpriteRenderer> ();
		pantsSpriteRenderer = transform.Find("Pants").gameObject.GetComponent <SpriteRenderer> ();
		bootsSpriteRenderer = transform.Find("Boots").gameObject.GetComponent <SpriteRenderer> ();
		pistolSpriteRenderer = transform.Find("Pistol").gameObject.GetComponent <SpriteRenderer> ();
		swordSpriteRenderer = transform.Find("Sword").gameObject.GetComponent <SpriteRenderer> ();

		// Do not destroy player after ot was created, do not create new players
		if (!playerExists) {
			playerExists = true;
			DontDestroyOnLoad (transform.gameObject);
		} else {
			Destroy (gameObject);
		}

		
	}

	void Update ()
	{
		DrawRelative ();
		if (health <= 0) {
			this.gameObject.SetActive(false);
		}
		/*
		if (waypoints.Count > 0) {
			nextWaypoint = new Vector3 (waypoints [0].X + 0.5f, waypoints[0].Y + 0.5f, 0f);
			if (nextWaypoint != null) {
				if (Vector3.Distance (transform.position, nextWaypoint) > 0.05) {
					MovePlayerToPosition (nextWaypoint);
				} else {
					waypoints.Remove (waypoints [0]);
					pRigidbody.velocity = new Vector2 (0f, 0f);
				}
			}
		}*/
	}

}
