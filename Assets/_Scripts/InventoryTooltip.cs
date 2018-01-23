using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTooltip : MonoBehaviour {

	public bool isActive = true;
	int slotindex;
	int groundindex;
	string slotName;

	Text nametext;
	Text description;
	Text effect;
	Text cost;
	Image image;
	Image icon;

	Player player;
	Item currentItem;

	public void ShowInventoryTooltip (int index) {
		slotindex = index;
		isActive = true;
		image.enabled = true;
	}

	public void ShowGroundTooltip (int GroundIndex) {
		groundindex = GroundIndex;
		slotindex = -2;
		isActive = true;
		image.enabled = true;
	}

	public void ShowEquipmentTooltip (string slot) {
		slotName = slot;
		slotindex = -1;
		isActive = true;
		image.enabled = true;
	}

	public void HideInventoryTooltip () {
		isActive = false;
		image.enabled = false;
	}

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent <Player> ();

		image = this.GetComponent <Image> ();
		nametext = this.transform.Find ("Name").gameObject.GetComponent <Text> ();
		description = this.transform.Find ("Description").gameObject.GetComponent <Text> ();
		effect = this.transform.Find ("Effect").gameObject.GetComponent <Text> ();
		cost = this.transform.Find ("Cost").gameObject.GetComponent <Text> ();
		icon = this.transform.Find ("Sprite").gameObject.GetComponent <Image> ();
	}

	void Start () {
		image.enabled = false;
	}

	void Update () {

		if (!GlobalData.paused) {
			HideInventoryTooltip ();
		}

		if (!isActive) {
			nametext.text = "";
			description.text = "";
			effect.text = "";
			cost.text = "";
			icon.sprite = Resources.Load <Sprite> ("Sprites/UI/nothing_empty");
		} else {
			if (slotindex != -1 && slotindex != -2) {
				if (slotindex < player.inventory.Count) {
					currentItem = player.inventory [slotindex];
				} else {
					HideInventoryTooltip ();
				}
			} else if (slotindex == -2) {
				Tile currenttile = player.GetPlayerTile(GlobalData.grid);

				if ((groundindex + GlobalData.groundSlotOffset) < currenttile.groundItems.Count) {
					currentItem = currenttile.groundItems [groundindex + GlobalData.groundSlotOffset];
				} else {
					HideInventoryTooltip ();
				}
			} else {
				switch (slotName) {
				case "helmet":
					if (player.helmet != GlobalData.naked_head) {
						currentItem = player.helmet;
					}
					break;
				case "bodyarmor":
					if (player.bodyarmor != GlobalData.naked_body) {
						currentItem = player.bodyarmor;
					}
					break;
				case "pants":
					if (player.pants != GlobalData.naked_legs) {
						currentItem = player.pants;
					}
					break;
				case "boots":
					if (player.boots != GlobalData.naked_feet) {
						currentItem = player.boots;
					}
					break;
				case "weapon":
					if (player.weapon != GlobalData.punch) {
						currentItem = player.weapon;
					}
					break;
				case "offhand":
					if (player.offhand != GlobalData.empty_offhand) {
						currentItem = player.offhand;
					}
					break;
				}
			}

			nametext.text = currentItem.name;
			if (currentItem.isAOE) {
				effect.text = "Effect: " + "Area damage";
			} else {
				effect.text = "";
			}
			icon.sprite = Resources.Load <Sprite> (currentItem.itemsprite);
			if (currentItem.type == Item.Type.Weapon) {
				description.text = "Damage: " + currentItem.mindamage + "-" + currentItem.maxdamage;
				description.text += "\nWeight: " + currentItem.weight;
			} else if (currentItem.type == Item.Type.Helmet
			          || currentItem.type == Item.Type.Bodyarmor
			          || currentItem.type == Item.Type.Pants
			          || currentItem.type == Item.Type.Boots
			          || currentItem.type == Item.Type.Offhand) {
				description.text = "Protection: " + currentItem.protection;
				description.text += "\nWeight: " + currentItem.weight;
			} else if (currentItem.type == Item.Type.Consumable) {
				description.text = "Uses left: " + currentItem.quantity;
				if (currentItem.effect == Item.Effect.Healing) {
					effect.text = "Effect: Heals for ";
					effect.text += currentItem.effectvalue + "HP";
				}
			}
			cost.text = "Cost: " + currentItem.cost;
		}
	}
}
