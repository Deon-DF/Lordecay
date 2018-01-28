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
	public static float playerSpawnX = 3f;
	public static float playerSpawnY = 3f;

	// Game log

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

	// Player stats influence

	public static int healthPerToughness = 20;
	public static int accuracyPerPerception = 10;
	public static int sanityPerIntelligence = 20;
	public static int staminaPerToughness = 20;

	public static float stunfactorPerStrength = 0.2f;


	// Inventory

	public static int inventorySize = 25;
	public static int groundSlotOffset = 0;

	// Item definition
	/*
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
		1);							// quantity*/


	// standard empty equipment (nothing equipped)

	public static Item no_weapon = new Item (Item.Type.Weapon, "Bare hands", Item.AttackType.Melee, 0.1f, "short",3, 5, false, 0f, 0, false, "Sprites/UI/Items/fist", Item.Effect.None);
	public static Item no_offhand = new Item (Item.Type.Offhand, "Empty offhand", 0, 0, 0, false, "Sprites/Characters/Blob", Item.Effect.None);
	public static Item no_helmet = new Item (Item.Type.Helmet, "Naked head", 0, 0, 0, false, "Sprites/Characters/Blob", Item.Effect.None);
	public static Item no_mask = new Item (Item.Type.Mask, "Naked head", 0, 0, 0, false, "Sprites/Characters/Blob", Item.Effect.None);
	public static Item no_armor = new Item (Item.Type.Bodyarmor, "No armor", 0, 0, 0, false, "Sprites/Characters/Blob", Item.Effect.None);
	public static Item no_clothing = new Item (Item.Type.Clothing, "Naked body", 0, 0, 0, false, "Sprites/Characters/Blob", Item.Effect.None);
	public static Item no_pants = new Item (Item.Type.Pants, "Naked legs", 0, 0, 0, false, "Sprites/Characters/Blob", Item.Effect.None);
	public static Item no_boots = new Item (Item.Type.Boots, "Naked feet", 0, 0, 0, false, "Sprites/Characters/Blob", Item.Effect.None);

}