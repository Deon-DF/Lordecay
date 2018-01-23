using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class GlobalData {

	// Menu system
	public static bool paused = false;
	public static bool inventoryON = false;

	public static Grid grid;

	public static bool debugmode = true;

	// World map/transition

	public static bool worldmap=false;
	public static float mapMovementSpeed = 0.5f;
	public static string nextEntry;
	public static float playerSpawnX = 20f;
	public static float playerSpawnY = 96f;

	// Log

	static string gamelog = "";

	public static string Gamelog {
		get {
			return gamelog;
		}

		set {
			var numberLineBreaks = Regex.Matches (gamelog, Environment.NewLine).Count;
			if (numberLineBreaks >= 11) {
				gamelog = value.Substring (gamelog.IndexOf (Environment.NewLine) + 1);
			} else {
				gamelog = value;
			}
		}
	}


	// Inventory

	public static int inventorySize = 25;
	public static int groundSlotOffset = 0;

	// Item definition

	public static Item nothing = new Item (
		Item.Type.None,				// item type
		"Empty", 					// name
		Item.AttackType.None, 		// attack type
		0,							// mindamage
		0,							// maxdamage
		false,						// is AOE
		0f,							// weight
		0,							// cost
		0,							// protection
		false,						// is equippable
		false,						// is quest item
		"Sprites/Characters/Blob",	// sprite
		Item.Effect.None,			// effect
		1);							// quantity


	// standard empty equipment (nothing equipped)

	public static Item punch = new Item (Item.Type.Weapon, "Bare hands", Item.AttackType.Melee, 3, 5, false, 0f, 0, 0, false, false, "Sprites/Characters/Blob", Item.Effect.None, 1);
	public static Item empty_offhand = new Item (Item.Type.Offhand, "Empty offhand", Item.AttackType.None, 0, 0, false, 0f, 0, 0, false, false, "Sprites/Characters/Blob", Item.Effect.None, 1);
	public static Item naked_head = new Item (Item.Type.Helmet, "Naked head", Item.AttackType.None, 0, 0, false, 0f, 0, 0, false, false, "Sprites/Characters/Blob", Item.Effect.None, 1);
	public static Item naked_body = new Item (Item.Type.Bodyarmor, "Naked body", Item.AttackType.None, 0, 0, false, 0f, 0, 0, false, false, "Sprites/Characters/Blob", Item.Effect.None, 1);
	public static Item naked_legs = new Item (Item.Type.Pants, "Naked legs", Item.AttackType.None, 0, 0, false, 0f, 0, 0, false, false, "Sprites/Characters/Blob", Item.Effect.None, 1);
	public static Item naked_feet = new Item (Item.Type.Boots, "Naked feet", Item.AttackType.None, 0, 0, false, 0f, 0, 0, false, false, "Sprites/Characters/Blob", Item.Effect.None, 1);

}
