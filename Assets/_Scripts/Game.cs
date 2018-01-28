﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	private Player player;
	GameObject overlay;

	GameObject passable_tile;
	GameObject impassable_tile;

	GameObject tiles_parent;
	GUI gui;

	Grid grid;

	// UI
	GameObject healthbar;
	GameObject staminabar;

	Image WeaponUI;


	// masks for layers
	int playerMask;
	int furnitureMask;
	int enemyMask; 
	int lineOfSightMask;
	int pathFindingMask;

	// GameObject picker 

	GameObject PickRandomGameObject (List<GameObject> list) {
		int result = Random.Range(0, list.Count);
		return list[result];
	}


	// PLAYER CONTROLS AND ENEMY ACTIONS BEGIN

	void CheckLocation (Monster monster) {
		if ((monster.transform.position.x < 1) ||
			(monster.transform.position.x > 99) ||
			(monster.transform.position.y < 1) ||
			(monster.transform.position.y > 99)) {
				monster.transform.position = new Vector3 (1.5f, 1.5f, 0);
			}
	}

	void EnemyActions (Monster monster, Player player) {
		monster.FollowWaypoints ();
		if (monster.RefreshAggro (player, lineOfSightMask)) {// ~(mask1|mask2) means NOT mask1+mask2; we dont want the aggro code to think that players/enemies block line of sight of each other
		} else {
			monster.aggroRememberCounter -= Time.deltaTime; // if monster did not refresh aggro on player, count down the aggro timer
		}
		if (monster.isAggressive) {
			monster.TargetPlayer (player, lineOfSightMask, grid);  // ~(mask1|mask2) means NOT mask1+mask2; we dont want the targeting code to think that players/enemies block line of sight of each other
		} else {
			monster.Roam ();
			monster.isPathFinding = false;
		}
	}

	public void HandleControls (Player player) {
		//PlayerRotateToMouse (player);
		//WASD (player);
		//Mouse0 (player);
		//SpaceKey (player);
		//player.ClickToPath (grid);
		if (!GlobalData.worldmap) {
			player.KeyboardControls ();
			player.MouseControls ();
			player.MovementControls ();
			player.AltHighLight ();

			// TODO Test inventory stuff, must be removed/hidden
			player.testFreeStuff (); // Gives controls to get free items
		} 
	}

	// PLAYER CONTROLS AND ENEMY ACTIONS END
	// PATHFINDING BEGIN

	/*
	Tile GetMonsterTile (Monster monster, Grid grid) {
		int x = (int)Mathf.Floor (monster.transform.position.x);
		int y = (int)Mathf.Floor (monster.transform.position.y);

		return grid.GetTileAt (x, y);		
	}*/

	void PathfindingRedraw (Grid grid, Player player) {
		for (int x = 0; x < grid.Width; x++) {
			for (int y = 0; y < grid.Height; y++) {
				if ((!Physics2D.Linecast (new Vector3 (x+0.5f, y+0.5f, 0f), new Vector3 (x + 0.6f, y+0.5f, 0f), pathFindingMask))) {
					GameObject tile_go = Instantiate (passable_tile, new Vector3 (x, y, 0f), Quaternion.identity);
					tile_go.name = "Tile_" + x + "_" + y;
					tile_go.tag = "tile_go";
					tile_go.transform.parent = tiles_parent.transform;
					tile_go.GetComponent <SpriteRenderer> ().enabled = false;
					tile_go.transform.Find ("cell").gameObject.GetComponent<SpriteRenderer> ().enabled = false;
					grid.tiles [x, y].IsPassable = true;
				} else {
					GameObject tile_go = Instantiate (impassable_tile, new Vector3 (x, y, 0f), Quaternion.identity);
					tile_go.name = "Tile_" + x + "_" + y;
					tile_go.tag = "tile_go";
					tile_go.transform.parent = tiles_parent.transform;
					tile_go.GetComponent <SpriteRenderer> ().enabled = false;
					tile_go.transform.Find ("cell").gameObject.GetComponent<SpriteRenderer> ().enabled = false;
					grid.tiles [x, y].IsPassable = false;
				}
			}
		}
		player.grid = grid;
	}

	// PATHFINDING END
	// UI begin

	void UpdateUI (Player player, GUI gui)
	{

		if (!GlobalData.worldmap && !GlobalData.inventoryON) {

			gui.enemyNameUI.GetComponent<Text> ().text = GUI.enemyName;
			gui.enemyStatusUI.GetComponent<Text> ().text = GUI.enemyStatus;

			//Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (!GlobalData.inventoryON) {
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				if (hit.collider != null) {
					if (hit.collider.gameObject.tag == "EnemyHitbox") {
						GameObject enemy = hit.collider.transform.parent.gameObject;
						gui.enemyBox.SetActive (true);
						GUI.enemyName = enemy.name;
						Monster monster = enemy.GetComponent<Monster> ();

						gui.enemyHealthBar.transform.localScale = new Vector3 (monster.Health * 1.0f / monster.maxhealth, 1, 1);

						GUI.enemyStatus = "";
						if (monster.isDisarmed) {
							GUI.enemyStatus += "Disarmed";
						}
						if (monster.isRooted) {
							GUI.enemyStatus += "Rooted";
						}
						if (monster.isStunned) {
							GUI.enemyStatus += "Stunned";
						}
					} 
				} else {
					gui.enemyBox.SetActive (false);
					GUI.enemyName = "";
				}				
			} else {
					gui.enemyBox.SetActive (false);
					GUI.enemyName = "";}
		}

		if (!GlobalData.worldmap) {
			WeaponUI.sprite = Resources.Load<Sprite>(player.Weapon.itemsprite);
			healthbar.transform.localScale = new Vector3 (player.Health * 1.0f / player.MaxHealth, 1, 1);
			staminabar.transform.localScale = new Vector3 (player.Stamina * 1.0f / player.MaxStamina, 1, 1);

			if (GlobalData.inventoryON) {
				gui.inventoryBG.SetActive (true);
				gui.inventoryBox.SetActive (true);
				gui.inventoryTooltip.SetActive (true);
				gui.equipmentBox.SetActive (true);
				gui.groundBox.SetActive (true);
			} else {
				gui.inventoryBG.SetActive (false);
				gui.inventoryBox.SetActive (false);
				gui.inventoryTooltip.SetActive (false);
				gui.equipmentBox.SetActive (false);
				gui.groundBox.SetActive (false);
			}


			if (GlobalData.inventoryON) {

				// Handle drawing of inventory slots
				int numInventoryItems = player.inventory.Count;
				Tile playerTile = player.GetPlayerTile (grid);
				int numItemsOnGround = playerTile.groundItems.Count;

				gui.strengthText.GetComponent<Text> ().text = player.strength.ToString();
				gui.perceptionText.GetComponent<Text> ().text = player.perception.ToString();
				gui.intelligenceText.GetComponent<Text> ().text = player.intelligence.ToString();
				gui.toughnessText.GetComponent<Text> ().text = player.toughness.ToString();

				gui.healthText.GetComponent<Text> ().text = player.Health + "/" + player.MaxHealth;
				gui.sanityText.GetComponent<Text> ().text = player.Sanity + "/" + player.MaxSanity;
				gui.staminaText.GetComponent<Text> ().text = Mathf.Round(player.Stamina) + "/" + player.MaxStamina;
				gui.accuracyText.GetComponent<Text> ().text = player.Accuracy + "%";

				gui.bluntDamageText.GetComponent<Text> ().text = player.Weapon.bluntMinDamage + "-" + player.Weapon.bluntMaxDamage;
				gui.pierceDamageText.GetComponent<Text> ().text = player.PierceMinDamage + "-" + player.PierceMaxDamage;
				gui.fireDamageText.GetComponent<Text> ().text = player.FireMinDamage + "-" + player.FireMaxDamage;
				gui.coldDamageText.GetComponent<Text> ().text = player.ColdMinDamage + "-" + player.ColdMaxDamage;
				gui.acidDamageText.GetComponent<Text> ().text = player.AcidMinDamage + "-" + player.AcidMaxDamage;

				gui.bluntArmorText.GetComponent<Text> ().text = player.BluntArmor.ToString();
				gui.pierceArmorText.GetComponent<Text> ().text = player.PierceArmor.ToString();
				gui.fireArmorText.GetComponent<Text> ().text = player.FireArmor.ToString();
				gui.coldArmorText.GetComponent<Text> ().text = player.ColdArmor.ToString();
				gui.acidArmorText.GetComponent<Text> ().text = player.AcidArmor.ToString();

				// Ground slots //
				// Assign item images to ground slots with items
				if (GlobalData.groundSlotOffset == 0) {
					gui.leftArrow.SetActive (false);
				} else {
					gui.leftArrow.SetActive (true);
				}

				if ((player.GetPlayerTile (grid).groundItems.Count - 5) <= GlobalData.groundSlotOffset) {
					gui.rightArrow.SetActive (false);
				} else {
					gui.rightArrow.SetActive (true);
				}

				if (numItemsOnGround > 0) {
					for (int i = 0; i < Mathf.Min (numItemsOnGround, 5); i++) {
						gui.GetGroundSlotByIndex (i).SetActive (true);
						gui.GetGroundSlotByIndex (i).GetComponent <Image> ().sprite = Resources.Load <Sprite> (playerTile.groundItems [i + GlobalData.groundSlotOffset].itemsprite);
					}
				}
				// Disable item slots without items
				for (int i = numItemsOnGround; i < 5; i++) {
					gui.GetGroundSlotByIndex (i).SetActive (false);
				}

				// Inventory slots //
				// Assign item images to inventory slots with items
				if (numInventoryItems > 0) {
					for (int i = 0; i < numInventoryItems; i++) {
						gui.GetSlotByIndex (i).SetActive (true);
						gui.GetSlotByIndex (i).GetComponent <Image> ().sprite = Resources.Load <Sprite> (player.inventory [i].itemsprite);
						if (player.inventory [i].type == Item.Type.Consumable) {
							gui.GetSlotQByIndex (i).GetComponent <Text> ().text = player.inventory [i].quantity.ToString ();
						} else {
							gui.GetSlotQByIndex (i).GetComponent <Text> ().text = "";
						}
					}
				}
				// Disable inventory slots without items
				for (int i = numInventoryItems; i < GlobalData.inventorySize; i++) {
					gui.GetSlotByIndex (i).SetActive (false);
				}

				// Remove attachments button
				if (player.Weapon.attachment1.type != Attachment.Type.None || player.Weapon.attachment2.type != Attachment.Type.None) {
					gui.removeAttachmentsButton.SetActive(true);
				} else {
					gui.removeAttachmentsButton.SetActive(false);
				}

				// Handle drawing of equipment slots
				if (player.Weapon != GlobalData.no_weapon) {
					gui.equipmentSlotWeapon.SetActive (true);
					gui.equipmentSlotWeapon.GetComponent <Image> ().sprite = Resources.Load <Sprite> (player.Weapon.itemsprite);
				} else {
					gui.equipmentSlotWeapon.SetActive (false);
				}
				if (player.offhand != GlobalData.no_offhand) {
					gui.equipmentSlotOffhand.SetActive (true);
					gui.equipmentSlotOffhand.GetComponent <Image> ().sprite = Resources.Load <Sprite> (player.offhand.itemsprite);
				} else {
					gui.equipmentSlotOffhand.SetActive (false);
				}
				if (player.helmet != GlobalData.no_helmet) {
					gui.equipmentSlotHelmet.SetActive (true);
					gui.equipmentSlotHelmet.GetComponent <Image> ().sprite = Resources.Load <Sprite> (player.helmet.itemsprite);
				} else {
					gui.equipmentSlotHelmet.SetActive (false);
				}
				if (player.mask != GlobalData.no_mask) {
					gui.equipmentSlotMask.SetActive (true);
					gui.equipmentSlotMask.GetComponent <Image> ().sprite = Resources.Load <Sprite> (player.mask.itemsprite);
				} else {
					gui.equipmentSlotMask.SetActive (false);
				}
				if (player.bodyarmor != GlobalData.no_armor) {
					gui.equipmentSlotBodyarmor.SetActive (true);
					gui.equipmentSlotBodyarmor.GetComponent <Image> ().sprite = Resources.Load <Sprite> (player.bodyarmor.itemsprite);
				} else {
					gui.equipmentSlotBodyarmor.SetActive (false);
				}
				if (player.clothing != GlobalData.no_clothing) {
					gui.equipmentSlotClothing.SetActive (true);
					gui.equipmentSlotClothing.GetComponent <Image> ().sprite = Resources.Load <Sprite> (player.clothing.itemsprite);
				} else {
					gui.equipmentSlotClothing.SetActive (false);
				}
				if (player.pants != GlobalData.no_pants) {
					gui.equipmentSlotPants.SetActive (true);
					gui.equipmentSlotPants.GetComponent <Image> ().sprite = Resources.Load <Sprite> (player.pants.itemsprite);
				} else {
					gui.equipmentSlotPants.SetActive (false);
				}
				if (player.boots != GlobalData.no_boots) {
					gui.equipmentSlotBoots.SetActive (true);
					gui.equipmentSlotBoots.GetComponent <Image> ().sprite = Resources.Load <Sprite> (player.boots.itemsprite);
				} else {
					gui.equipmentSlotBoots.SetActive (false);
				}

			}
		}
	}

	// UI end

	void Awake ()
	{

		// Create city map for Slums

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent <Player> ();
		if (GlobalData.worldmap != true) {
			gui = GameObject.Find ("GUI").GetComponent <GUI> ();

			healthbar = GameObject.Find ("Healthbar");
			staminabar = GameObject.Find ("Staminabar");
//		staminabar = GameObject.Find ("Staminabar");
			WeaponUI = GameObject.Find ("WeaponUI").GetComponent<Image> ();

			// Load resources and variables
			passable_tile = Resources.Load ("Prefabs/UI/passable_tile") as GameObject;
			impassable_tile = Resources.Load ("Prefabs/UI/impassable_tile") as GameObject;

			tiles_parent = GameObject.Find ("PathfindingTiles");


			playerMask = 1 << LayerMask.NameToLayer ("Player"); // enemies dont block visibility of other enemies, and player collider should not block the visibility of the player,
			furnitureMask = 1 << LayerMask.NameToLayer ("Furniture"); // should be able to see/shoot through furniture
			enemyMask = 1 << LayerMask.NameToLayer ("Enemy");   // so we "revert" these two masks with ~ to skip them from check in line of sight check.
			lineOfSightMask = ~(playerMask | enemyMask | furnitureMask);
			pathFindingMask = ~(playerMask | enemyMask); // Furniture still blocks walking
		}


		if (GlobalData.nextEntry != null) {
			GameObject entrylist = GameObject.FindGameObjectWithTag ("Entrypoint");
			Transform entrypoint = entrylist.transform.Find (GlobalData.nextEntry);

			if (!GlobalData.worldmap) {
				GlobalData.playerSpawnX = entrypoint.position.x;
				GlobalData.playerSpawnY = entrypoint.position.y;

			} else {
				GlobalData.playerSpawnX = -100;
				GlobalData.playerSpawnY = -100;


				GameObject mapWalker = GameObject.Find ("MapWalker");
				mapWalker.transform.position = new Vector3 (entrypoint.position.x, entrypoint.position.y, 0);
				Debug.Log (entrypoint.name);
			}
		}
	}

	void Start () {
		player.transform.position = new Vector3 (GlobalData.playerSpawnX, GlobalData.playerSpawnY, player.transform.position.z);

		if (!GlobalData.worldmap) {
			grid = new Grid ();
			GlobalData.grid = grid;
			PathfindingRedraw (grid, player);
		}


	}

	void Update ()
	{

		// UI stuff
		UpdateUI(player, gui);
		player.PauseControls ();

		// Player controls and behavior
		if (!GlobalData.paused) {
			HandleControls (player);
			player.Cooldown ();
		} else {
			player.pRigidbody.velocity = new Vector2 (0f, 0f);
		}

		// Monster controls and behavior
		Monster[] enemies = GameObject.FindObjectsOfType<Monster> ();
		foreach (Monster enemy in enemies) {
			if (!GlobalData.paused) {
				CheckLocation(enemy);
				EnemyActions (enemy, player);
				enemy.CoolDown ();
				if (!Physics2D.Linecast (player.transform.position, enemy.transform.position, lineOfSightMask)) {
					enemy.isVisibleToPlayers = true;
				} else {
					enemy.isVisibleToPlayers = false;
				}
			} else {
				enemy.eRigidBody.velocity = new Vector2 (0f, 0f);
			}
		}
	}
}
