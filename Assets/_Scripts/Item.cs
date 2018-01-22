using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item {

	public enum Type {None, Weapon, Offhand, Bodywear, Helmet, Pants, Boots, Consumable, Other}
	public enum Effect {None, Healing, StatusEffect, Lootbox}
	public enum AttackType {None, Melee, Single, Cone}

	public string name;
	public Type type = Type.None;
	public AttackType attack = AttackType.None;
	public int damage = 0;
	public bool isAOE;

	public float weight = 0.1f;
	public int cost = 0;
	public int protection = 0;
	public bool isEquippable = false;
	public bool isQuestItem = false;
	public string itemsprite;
	public Effect effect = Item.Effect.None;
	public int quantity = 1;
	public int effectvalue = 0;

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

	public Item (Type itemType, string Name, AttackType AttackType, int Damage, bool AOE, float Weight, int Cost, int Protection, bool Equippable, bool Questitem, string Sprite, Effect itemEffect, int Quantity) {
		name = Name;
		type = itemType;
		attack = AttackType;
		damage = Damage;
		isAOE = AOE;
		weight = Weight;
		cost = Cost;
		protection = Protection;
		isEquippable = Equippable;
		isQuestItem = Questitem;
		itemsprite = Sprite;
		effect = itemEffect;
		quantity = Quantity;
	}

	// Weapons
	public Item (Type itemType, string Name, AttackType AttackType, int Damage, bool AOE, float Weight, int Cost, bool Questitem, string Sprite, Effect itemEffect) {
		type = itemType;
		name = Name;
		attack = AttackType;
		damage = Damage;
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
		protection = Protection;
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
				name = "Army helmet";
				weight = 1f;
				cost = 20;
				protection = 2;
				itemsprite = "Sprites/UI/Items/armyHelmet";
				isEquippable = true;
				break;

			default: 
				break;
			}
		}

		// Bodyarmor

		if (itemtype == Type.Bodywear) {
			switch (ItemName) {
			case "Kevlar vest":
				type = Type.Bodywear;
				name = "Kevlar Vest";
				weight = 4f;
				cost = 40;
				protection = 5;
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
				name = "Camo pants";
				weight = 2f;
				cost = 15;
				protection = 2;
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
				name = "Leather boots";
				weight = 2f;
				cost = 25;
				protection = 2;
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
				name = "Riot shield";
				weight = 5f;
				cost = 50;
				protection = 5;
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
			case "Baseball bat":
				type = Type.Weapon;
				attack = AttackType.Melee;
				name = "Baseball bat";
				weight = 1f;
				cost = 10;
				damage = 30;
				isAOE = true;
				itemsprite = "Sprites/UI/Items/baseballBat";
				isEquippable = true;
				break;

			case "Sword":
				type = Type.Weapon;
				attack = AttackType.Melee;
				name = "Sword";
				weight = 1f;
				cost = 20;
				damage = 50;
				isAOE = false;
				itemsprite = "Sprites/UI/Items/sword";
				isEquippable = true;
				break;
			

			default: 
				break;
			}
		}
	}

}
