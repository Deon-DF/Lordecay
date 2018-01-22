using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item {

	public enum Type {None, Weapon, Offhand, Bodywear, Helmet, Pants, Boots, Consumable, Other}
	public enum Effect {None, Healing, StatusEffect, Lootbox}
	public enum AttackType {None, Melee, Single, Cone}

	public string name;
	public Type type;
	public AttackType attack;
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
}
