using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attachment {
	public enum Type {None, MeleeHandle, MeleeBluntSurface, MeleeEdgeSurface, RangedHandle, RangedScope}
	public Type type = Attachment.Type.None;

	public string name = "None";
	public int bluntBonusDamage = 0;
	public int pierceBonusDamage = 0;
	public int fireBonusDamage = 0;
	public int coldBonusDamage = 0;
	public int acidBonusDamage = 0;

	public int bluntBonusArmor = 0;
	public int pierceBonusArmor = 0;
	public int fireBonusArmor = 0;
	public int coldBonusArmor = 0;
	public int acidBonusArmor = 0;

	public int accuracyBonus = 0;
	public float attackspeedBonus = 0;
	public float movespeedBonus = 0;
	public float weightBonus = 0;

	public Attachment (Type whichType, string whichName) {

		if (whichType == Type.None) {
			switch (whichName) {
			case "Nothing":
				type = Type.None;
				name = "Nothing";
				bluntBonusDamage = 0;
				pierceBonusDamage = 0;
				fireBonusDamage = 0;
				coldBonusDamage = 0;
				acidBonusDamage = 0;

				bluntBonusArmor = 0;
				pierceBonusArmor = 0;
				fireBonusArmor = 0;
				coldBonusArmor = 0;
				acidBonusArmor = 0;

				accuracyBonus = 0;
				attackspeedBonus = 0;
				movespeedBonus = 0;
				weightBonus = 0;

				break;

			}

		}

		if (whichType == Type.RangedScope) {

			// Ironsights
			switch (whichName) {
			case "Red Dot":

				type = Type.RangedScope;
				name = "Red Dot";
				accuracyBonus = 10;

				
				break;

			default: 
				break;
			}
		}
		if (whichType == Type.RangedHandle) {

			// Grips
			switch (whichName) {
			case "Custom Grip":

				type = Type.RangedHandle;
				name = "Custom Grip";
				accuracyBonus = 5;

				
				break;

			default: 
				break;
			}
		}
	}
}

public class Item {

	public enum Type {None, Weapon, Offhand, Bodyarmor, Clothing, Helmet, Mask, Pants, Boots, Consumable, WeaponAttachment, Other}
	public enum SubType {None,										// no subtype (naked/not displayed)
						Club, Sword,								// melee weapons
						Pistol, Rifle, Uzi, Shotgun, Machinegun,	// ranged weapons
						Shield,										// offhand
						Cap, Hat, Casque,							// headwear
						Facemask, Gasmask,							// masks
						Rags, Shirt,										// clothing
						Armorvest,									// bodyarmor
						Trousers,									// Legwear
						Boots,										// Footwear
						};
	public enum AttackType {None, Melee, RangedSingle, RangedCone}
	public enum Effect {None, Healing, StatusEffect, Lootbox}

	public bool isLoud = false;
	public float soundDistance = 1f;
	public float soundDuration = 0.1f;

	public string name;
	public Type type = Type.None;
	public SubType subtype = SubType.None;
	public AttackType attacktype = AttackType.None;
	public Attachment.Type attachmenttype = Attachment.Type.None;
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

	// Stun factor, speed and accuracy bonus
	public float stunfactor = 0f;

	public int accuracyBonus = 0;
	public float movespeedBonus = 0f;

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


	public Attachment attachment1 = new Attachment(Attachment.Type.None, "Nothing");
	public List<Attachment.Type> attachment1_types = new List<Attachment.Type>();

	public Attachment attachment2 = new Attachment(Attachment.Type.None, "Nothing");
	public List<Attachment.Type> attachment2_types = new List<Attachment.Type>();

	public string attachment1_description = "";
	public string attachment2_description = "";
	public string customdescription = "";


	// Components for improvement

	/*
	public void randomBonus (Item item) {
		if (item.type = Type.Weapon) {
			int chance = Random.Range (0, 100);
		}
	}*/

	public void AddAttachment1 (Attachment attachment) {

		if (attachment1.type == Attachment.Type.None){
			if (attachment.type != Attachment.Type.None) {
				Debug.Log("Adding attachment to slot 1, attachment name: " + attachment.name);
				bluntMinDamage += attachment.bluntBonusDamage;
				bluntMaxDamage += attachment.bluntBonusDamage;
				pierceMinDamage += attachment.pierceBonusDamage;
				pierceMaxDamage += attachment.pierceBonusDamage;
				fireMinDamage += attachment.fireBonusDamage;
				fireMaxDamage += attachment.fireBonusDamage;
				coldMinDamage += attachment.coldBonusDamage;
				coldMaxDamage += attachment.coldBonusDamage;
				acidMinDamage += attachment.acidBonusDamage;
				acidMaxDamage += attachment.acidBonusDamage;

				bluntArmor += attachment.bluntBonusArmor;
				pierceArmor += attachment.pierceBonusArmor;
				fireArmor += attachment.fireBonusArmor;
				coldArmor += attachment.coldBonusArmor;
				acidArmor += attachment.acidBonusArmor;

				accuracyBonus += attachment.accuracyBonus;
				attackcooldown += attachment.attackspeedBonus;
				movespeedBonus += attachment.movespeedBonus;
				weight += attachment.weightBonus;

				attachment1 = attachment;
			}
		} else {
			Debug.Log ("Attachment slot 1 is already busy");
		}
	}
	public void AddAttachment2 (Attachment attachment) {

		if (attachment2.type == Attachment.Type.None){
			if (attachment.type != Attachment.Type.None) {
				Debug.Log("Adding attachment to slot 2, attachment name: " + attachment.name);
				bluntMinDamage += attachment.bluntBonusDamage;
				bluntMaxDamage += attachment.bluntBonusDamage;
				pierceMinDamage += attachment.pierceBonusDamage;
				pierceMaxDamage += attachment.pierceBonusDamage;
				fireMinDamage += attachment.fireBonusDamage;
				fireMaxDamage += attachment.fireBonusDamage;
				coldMinDamage += attachment.coldBonusDamage;
				coldMaxDamage += attachment.coldBonusDamage;
				acidMinDamage += attachment.acidBonusDamage;
				acidMaxDamage += attachment.acidBonusDamage;

				bluntArmor += attachment.bluntBonusArmor;
				pierceArmor += attachment.pierceBonusArmor;
				fireArmor += attachment.fireBonusArmor;
				coldArmor += attachment.coldBonusArmor;
				acidArmor += attachment.acidBonusArmor;

				accuracyBonus += attachment.accuracyBonus;
				attackcooldown += attachment.attackspeedBonus;
				movespeedBonus += attachment.movespeedBonus;
				weight += attachment.weightBonus;

				attachment2 = attachment;
			}
		} else {
			Debug.Log ("Attachment slot 2 is already busy");
		}
	}

	public void RemoveAllAttachments () {
		if (attachment1.type != Attachment.Type.None) {
			bluntMinDamage -= attachment1.bluntBonusDamage;
			bluntMaxDamage -= attachment1.bluntBonusDamage;
			pierceMinDamage -= attachment1.pierceBonusDamage;
			pierceMaxDamage -= attachment1.pierceBonusDamage;
			fireMinDamage -= attachment1.fireBonusDamage;
			fireMaxDamage -= attachment1.fireBonusDamage;
			coldMinDamage -= attachment1.coldBonusDamage;
			coldMaxDamage -= attachment1.coldBonusDamage;
			acidMinDamage -= attachment1.acidBonusDamage;
			acidMaxDamage -= attachment1.acidBonusDamage;

			bluntArmor -= attachment1.bluntBonusArmor;
			pierceArmor -= attachment1.pierceBonusArmor;
			fireArmor -= attachment1.fireBonusArmor;
			coldArmor -= attachment1.coldBonusArmor;
			acidArmor -= attachment1.acidBonusArmor;

			accuracyBonus -= attachment1.accuracyBonus;
			attackcooldown -= attachment1.attackspeedBonus;
			movespeedBonus -= attachment1.movespeedBonus;
			weight -= attachment1.weightBonus;

			attachment1 = new Attachment(Attachment.Type.None, "Nothing");			
		}
		if (attachment2.type != Attachment.Type.None) {
			bluntMinDamage -= attachment2.bluntBonusDamage;
			bluntMaxDamage -= attachment2.bluntBonusDamage;
			pierceMinDamage -= attachment2.pierceBonusDamage;
			pierceMaxDamage -= attachment2.pierceBonusDamage;
			fireMinDamage -= attachment2.fireBonusDamage;
			fireMaxDamage -= attachment2.fireBonusDamage;
			coldMinDamage -= attachment2.coldBonusDamage;
			coldMaxDamage -= attachment2.coldBonusDamage;
			acidMinDamage -= attachment2.acidBonusDamage;
			acidMaxDamage -= attachment2.acidBonusDamage;

			bluntArmor -= attachment2.bluntBonusArmor;
			pierceArmor -= attachment2.pierceBonusArmor;
			fireArmor -= attachment2.fireBonusArmor;
			coldArmor -= attachment2.coldBonusArmor;
			acidArmor -= attachment2.acidBonusArmor;

			accuracyBonus -= attachment2.accuracyBonus;
			attackcooldown -= attachment2.attackspeedBonus;
			movespeedBonus -= attachment2.movespeedBonus;
			weight -= attachment2.weightBonus;

			attachment2 = new Attachment(Attachment.Type.None, "Nothing");			
		}
	}

	public void RemoveAttachmentByIndex (int attachmentSlot) {

		if (attachmentSlot == 1) {
			bluntMinDamage -= attachment1.bluntBonusDamage;
			bluntMaxDamage -= attachment1.bluntBonusDamage;
			pierceMinDamage -= attachment1.pierceBonusDamage;
			pierceMaxDamage -= attachment1.pierceBonusDamage;
			fireMinDamage -= attachment1.fireBonusDamage;
			fireMaxDamage -= attachment1.fireBonusDamage;
			coldMinDamage -= attachment1.coldBonusDamage;
			coldMaxDamage -= attachment1.coldBonusDamage;
			acidMinDamage -= attachment1.acidBonusDamage;
			acidMaxDamage -= attachment1.acidBonusDamage;

			bluntArmor -= attachment1.bluntBonusArmor;
			pierceArmor -= attachment1.pierceBonusArmor;
			fireArmor -= attachment1.fireBonusArmor;
			coldArmor -= attachment1.coldBonusArmor;
			acidArmor -= attachment1.acidBonusArmor;

			accuracyBonus -= attachment1.accuracyBonus;
			attackcooldown -= attachment1.attackspeedBonus;
			movespeedBonus -= attachment1.movespeedBonus;
			weight -= attachment1.weightBonus;

			attachment1 = new Attachment(Attachment.Type.None, "Nothing");
		}
	}

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
				subtype = SubType.Casque;
				name = "Army Helmet";
				weight = 1f;
				cost = 20;
				bluntArmor = 3;
				pierceArmor = 3;
				fireArmor = 1;
				itemsprite = "Sprites/UI/Items/armyHelmet";
				isEquippable = true;
				color = new Color (0.47f, 0.59f, 0.27f);
				break;

			default: 
				break;
			}
		}

		// Masks
		if (itemtype == Type.Mask) {
			switch (ItemName) {
			case "Gas Mask":
				type = Type.Mask;
				subtype = SubType.Gasmask;
				name = "Gas Mask";
				weight = 1f;
				cost = 30;
				bluntArmor = 1;
				pierceArmor = 1;
				fireArmor = 5;
				coldArmor = 5;
				acidArmor = 5;
				itemsprite = "Sprites/UI/Items/gasMask";
				isEquippable = true;
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
				subtype = SubType.Armorvest;
				name = "Kevlar Vest";
				weight = 4f;
				cost = 40;
				bluntArmor = 2;
				pierceArmor = 5;
				itemsprite = "Sprites/UI/Items/kevlarVest";
				isEquippable = true;
				color = new Color (0.3f, 0.3f, 0.3f);
				break;

			default: 
				break;
			}
		}

		// Clothing

		if (itemtype == Type.Clothing) {
			switch (ItemName) {

			case "Leather jacket":
				type = Type.Clothing;
				subtype = SubType.Shirt;
				name = "Leather Jacket";
				weight = 2.5f;
				cost = 40;
				bluntArmor = 3;
				pierceArmor = 3;
				fireArmor = 3;
				coldArmor = 3;
				acidArmor = 2;
				itemsprite = "Sprites/UI/Items/leatherJacket";
				color = new Color (0.3f, 0.3f, 0.3f);
				isEquippable = true;
				break;

			case "Rags":
				type = Type.Clothing;
				subtype = SubType.Rags;
				name = "Rags";
				weight = 0.5f;
				cost = 1;
				bluntArmor = 1;
				pierceArmor = 1;
				itemsprite = "Sprites/UI/Items/rags";
				color = new Color (0.6f, 0.5f, 0f);
				isEquippable = true;
				break;
			
			case "Shirt":
				type = Type.Clothing;
				subtype = SubType.Shirt;
				name = "Shirt";
				weight = 0.5f;
				cost = 10;
				fireArmor = 1;
				coldArmor = 1;
				itemsprite = "Sprites/UI/Items/shirt";
				isEquippable = true;
				break;

			case "Sports jacket":
				type = Type.Clothing;
				subtype = SubType.Shirt;
				name = "Sports Jacket";
				weight = 1f;
				cost = 20;
				bluntArmor = 1;
				pierceArmor = 1;
				fireArmor = 1;
				coldArmor = 1;
				itemsprite = "Sprites/UI/Items/sportsJacket";
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
				subtype = SubType.Trousers;
				name = "Camo Pants";
				weight = 2f;
				cost = 25;
				bluntArmor = 1;
				pierceArmor = 1;
				fireArmor = 1;
				coldArmor = 1;
				itemsprite = "Sprites/UI/Items/camoPants";
				isEquippable = true;
				break;

			case "Jeans":
				type = Type.Pants;
				subtype = SubType.Trousers;
				name = "Jeans";
				weight = 1.5f;
				cost = 15;
				pierceArmor = 1;
				fireArmor = 1;
				coldArmor = 1;
				itemsprite = "Sprites/UI/Items/jeans";
				color = new Color (0.3f, 0.3f, 1f);
				isEquippable = true;
				break;

			case "Sports pants":
				type = Type.Pants;
				subtype = SubType.Trousers;
				name = "Sports Pants";
				weight = 1f;
				cost = 10;
				fireArmor = 1;
				coldArmor = 1;
				itemsprite = "Sprites/UI/Items/sportsPants";
				color = new Color (0.3f, 0.3f, 0.3f);
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
				subtype = SubType.Boots;
				name = "Leather Boots";
				weight = 2f;
				cost = 20;
				pierceArmor = 2;
				bluntArmor = 2;
				fireArmor = 1;
				coldArmor = 1;
				acidArmor = 1;
				itemsprite = "Sprites/UI/Items/leatherBoots";
				movespeedBonus = 0.1f;
				isEquippable = true;
				color = new Color (0.47f, 0.31f, 0f);
				break;

			case "Sneakers":
				type = Type.Boots;
				subtype = SubType.Boots;
				name = "Sneakers";
				weight = 1.5f;
				cost = 25;
				itemsprite = "Sprites/UI/Items/sneakers";
				movespeedBonus = 0.25f;
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
				subtype = SubType.Shield;
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
			case "Heavy club":
				type = Type.Weapon;
				subtype = SubType.Club;
				attacktype = AttackType.Melee;
				attackcooldown = 0.5f;
				attackrange = "long";
				name = "Heavy club";
				weight = 1f;
				cost = 10;
				bluntMinDamage = 25;
				bluntMaxDamage = 30;
				stunfactor = 0.5f;
				isAOE = true;
				isLoud = true;
				soundDistance = 6f;
				itemsprite = "Sprites/UI/Items/heavyClub";
				color = new Color (0.9f, 0.7f, 0.31f);
				isEquippable = true;
				break;

			case "Baseball bat":
				type = Type.Weapon;
				subtype = SubType.Club;
				attacktype = AttackType.Melee;
				attackcooldown = 0.3f;
				attackrange = "long";
				name = "Baseball Bat";
				weight = 1f;
				cost = 10;
				bluntMinDamage = 15;
				bluntMaxDamage = 30;
				stunfactor = 0.3f;
				isAOE = false;
				isLoud = true;
				soundDistance = 5f;
				itemsprite = "Sprites/UI/Items/baseballBat";
				color = new Color (0.9f, 0.7f, 0.31f);
				isEquippable = true;
				break;

			case "Sword":
				type = Type.Weapon;
				subtype = SubType.Sword;
				attacktype = AttackType.Melee;
				attackcooldown = 0.3f;
				attackrange = "long";
				name = "Sword";
				weight = 1f;
				cost = 20;
				pierceMinDamage = 25;
				pierceMaxDamage = 40;
				stunfactor = 0.1f;
				isAOE = true;
				isLoud = true;
				soundDistance = 5f;
				itemsprite = "Sprites/UI/Items/sword";
				isEquippable = true;
				break;

			// ranged

			case "Pistol":
				type = Type.Weapon;
				subtype = SubType.Pistol;
				attacktype = AttackType.RangedSingle;
				attackcooldown = 0.3f;
				name = "Pistol";
				weight = 0.5f;
				cost = 100;
				pierceMinDamage = 25;
				pierceMaxDamage = 50;
				stunfactor = 0.5f;
				isAOE = false;
				isLoud = true;
				soundDistance = 20f;
				itemsprite = "Sprites/UI/Items/pistol";
				isEquippable = true;
				color = new Color (0.3f, 0.3f, 0.3f);

				attachment1_description = "Scope (red dot, reflex sight)";
				attachment2_description = "Handle (pistol grips)";

				attachment1_types.Add (Attachment.Type.RangedScope);
				attachment2_types.Add (Attachment.Type.RangedHandle);
				break;

			default: 
				break;

			}
		}

			// Weapon attachments

		if (itemtype == Type.WeaponAttachment) {
			switch (ItemName) {
			// ironsights
			case "Red Dot":
				type = Type.WeaponAttachment;
				attachmenttype = Attachment.Type.RangedScope;
				name = "Red Dot";
				customdescription = "+10% accuracy to pistols, rifles and machine guns.";
				weight = 0.1f;
				cost = 20;
				itemsprite = "Sprites/UI/Items/redDot";
				isEquippable = false;
				break;

			// grips
			case "Custom Grip":
				type = Type.WeaponAttachment;
				attachmenttype = Attachment.Type.RangedHandle;
				name = "Custom Grip";
				customdescription = "+5% accuracy to pistols.";
				weight = 0.1f;
				cost = 10;
				itemsprite = "Sprites/UI/Items/customGrip";
				isEquippable = false;
				break;

			default: 
				break;

			}
		}
	}
}
