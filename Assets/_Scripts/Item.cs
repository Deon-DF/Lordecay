using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item {

	public enum Type {None, Weapon, Offhand, Bodyarmor, Helmet, Pants, Boots, Consumable, Other}
	public enum Effect {None, Healing, StatusEffect, Lootbox}
	public enum AttackType {None, Melee, RangedSingle, RangedCone}
	public enum SpriteType {None, Bat, Sword, Pistol, Shotgun, Uzi, Machinegun}

	public bool isLoud = false;

	public string name;
	public Type type = Type.None;
	public AttackType attacktype = AttackType.None;
	public SpriteType spritetype = SpriteType.None;
	public string attackrange = "long";
	public float attackcooldown = 0f;

	// blunt damage
	public int bluntMinDamage = 0;
	public int bluntMaxDamage = 0;
	// pierce damage
	public int pierceMinDamage = 0;
	public int pierceMaxDamage = 0;
	// fire damage
	public int fireMinDamage = 0;
	public int fireMaxDamage = 0;
	// cold damage
	public int coldMinDamage = 0;
	public int coldMaxDamage = 0;
	// acid damage
	public int acidMinDamage = 0;
	public int acidMaxDamage = 0;

	public float stunfactor = 0f;
	public bool isAOE;

	public float weight = 0.1f;
	public int cost = 0;

	public int bluntArmor = 0;
	public int pierceArmor = 0;
	public int fireArmor = 0;
	public int coldArmor = 0;
	public int acidArmor = 0;

	public bool isEquippable = false;
	public bool isQuestItem = false;
	public string itemsprite;
	public Effect effect = Item.Effect.None;
	public int quantity = 1;
	public int effectvalue = 0;

	public Color color = Color.white;

	/*
	public void randomBonus (Item item) {
		if (item.type = Type.Weapon) {
			int chance = Random.Range (0, 100);
		}
	}*/

	public void Use (Player player, int index) {
		Item item = player.inventory [index];
		if (item.effect == Item.Effect.Healing) {
			if (player.Health < player.MaxHealth) {
				player.Health += item.effectvalue;
				quantity--;
			} else {
				Debug.Log("Health is already at maximum!");
			}

		}
		if (item.quantity <= 0) {
			player.inventory.Remove (item);
		}
	}
	/*
	public Item (Type itemType, string Name, AttackType AttackType, int minDamage, int maxDamage, bool AOE, float Weight, int Cost, int Protection, bool Equippable, bool Questitem, string Sprite, Effect itemEffect, int Quantity) {
		name = Name;
		type = itemType;
		attacktype = AttackType;
		bluntMinDamage = minDamage;
		bluntMaxDamage = maxDamage;
		isAOE = AOE;
		weight = Weight;
		cost = Cost;
		bluntArmor = Protection;
		isEquippable = Equippable;
		isQuestItem = Questitem;
		itemsprite = Sprite;
		effect = itemEffect;
		quantity = Quantity;
	}*/

	// Weapons
	public Item (Type itemType, string Name, AttackType AttackType, float attackCooldown, string attackRange, int minDamage, int maxDamage, bool AOE, float Weight, int Cost, bool Questitem, string Sprite, Effect itemEffect) {
		type = itemType;
		name = Name;
		attacktype = AttackType;
		attackcooldown = attackCooldown;
		attackrange = attackRange;
		bluntMinDamage = minDamage;
		bluntMaxDamage = maxDamage;
		isAOE = AOE;
		weight = Weight;
		cost = Cost;
		isEquippable = true;
		isQuestItem = Questitem;
		itemsprite = Sprite;
		effect = itemEffect;
	}

	// Armors/helmets/boots/pants/offhand
	public Item (Type itemType, string Name, float Weight, int Cost, int Protection, bool Questitem, string Sprite, Effect itemEffect) {
		name = Name;
		type = itemType;
		weight = Weight;
		cost = Cost;
		bluntArmor = Protection;
		isEquippable = true;
		isQuestItem = Questitem;
		itemsprite = Sprite;
		effect = itemEffect;
	}

	// Consumable
	public Item (Type itemType, string Name, float Weight, int Cost, string Sprite, Effect itemEffect, int Effectvalue, int Quantity) {
		name = Name;
		type = itemType;
		weight = Weight;
		cost = Cost;
		isEquippable = false;
		itemsprite = Sprite;
		effect = itemEffect;
		effectvalue = Effectvalue;
		quantity = Quantity;
	}



	public Item (Type itemtype, string ItemName)
	{
		if (itemtype == Type.Consumable) {

			// Consumables
			switch (ItemName) {
			case "MedKit":

				type = Type.Consumable;
				name = "MedKit";
				weight = 1f;
				cost = 50;
				itemsprite = "Sprites/UI/Items/medkit";
				effect = Effect.Healing;
				effectvalue = 20;
				quantity = 5;
				break;

			default: 
				break;
			}
		}

		// Helmets
		if (itemtype == Type.Helmet) {
			switch (ItemName) {
			case "Army helmet":
				type = Type.Helmet;
				name = "Army Helmet";
				weight = 1f;
				cost = 20;
				bluntArmor = 3;
				pierceArmor = 3;
				fireArmor = 1;
				itemsprite = "Sprites/UI/Items/armyHelmet";
				isEquippable = true;
				color = new Color(0.47f, 0.59f, 0.27f);
				break;

			default: 
				break;
			}
		}

		// Bodyarmor

		if (itemtype == Type.Bodyarmor) {
			switch (ItemName) {
			case "Kevlar vest":
				type = Type.Bodyarmor;
				name = "Kevlar Vest";
				weight = 4f;
				cost = 40;
				bluntArmor = 2;
				pierceArmor = 5;
				itemsprite = "Sprites/UI/Items/kevlarVest";
				isEquippable = true;
				break;

			default: 
				break;
			}
		}

		// Pants

		if (itemtype == Type.Pants) {
			switch (ItemName) {
			case "Camo pants":
				type = Type.Pants;
				name = "Camo Pants";
				weight = 2f;
				cost = 15;
				bluntArmor = 1;
				pierceArmor = 1;
				fireArmor = 1;
				coldArmor = 1;
				itemsprite = "Sprites/UI/Items/camoPants";
				isEquippable = true;
				break;

			default: 
				break;
			}
		}

		// Boots

		if (itemtype == Type.Boots) {
			switch (ItemName) {
			case "Leather boots":
				type = Type.Boots;
				name = "Leather Boots";
				weight = 2f;
				cost = 25;
				pierceArmor = 2;
				bluntArmor = 2;
				fireArmor = 1;
				coldArmor = 1;
				acidArmor = 1;
				itemsprite = "Sprites/UI/Items/leatherBoots";
				isEquippable = true;
				break;

			default: 
				break;
			}
		}

		// Offhand

		if (itemtype == Type.Offhand) {
			switch (ItemName) {
			case "Riot shield":
				type = Type.Offhand;
				name = "Riot Shield";
				weight = 5f;
				cost = 50;
				bluntArmor = 5;
				pierceArmor = 5;
				fireArmor = 2;
				acidArmor = 2;
				itemsprite = "Sprites/UI/Items/riotShield";
				isEquippable = true;
				break;

			default: 
				break;
			}
		}
	
	// Weapons

		if (itemtype == Type.Weapon) {
			switch (ItemName) {
			// melee
			case "Baseball bat":
				type = Type.Weapon;
				attacktype = AttackType.Melee;
				attackcooldown = 0.3f;
				attackrange = "long";
				name = "Baseball Bat";
				weight = 1f;
				cost = 10;
				bluntMinDamage = 15;
				bluntMaxDamage = 30;
				stunfactor = 0.4f;
				isAOE = true;
				itemsprite = "Sprites/UI/Items/baseballBat";
				isEquippable = true;
				break;

			case "Sword":
				type = Type.Weapon;
				spritetype = SpriteType.Sword;
				attacktype = AttackType.Melee;
				attackcooldown = 0.3f;
				attackrange = "long";
				name = "Sword";
				weight = 1f;
				cost = 20;
				pierceMinDamage = 25;
				pierceMaxDamage = 40;
				stunfactor = 0.2f;
				isAOE = false;
				itemsprite = "Sprites/UI/Items/sword";
				isEquippable = true;
				break;

			// ranged

			case "Pistol":
				type = Type.Weapon;
				spritetype = SpriteType.Pistol;
				attacktype = AttackType.RangedSingle;
				attackcooldown = 0.4f;
				name = "Pistol";
				weight = 0.5f;
				cost = 100;
				pierceMinDamage = 25;
				pierceMaxDamage = 50;
				stunfactor = 0.5f;
				isAOE = false;
				isLoud = true;
				itemsprite = "Sprites/UI/Items/pistol";
				isEquippable = true;
				break;
			

			default: 
				break;
			}
		}
	}

}
