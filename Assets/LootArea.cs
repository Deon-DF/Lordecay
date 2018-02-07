using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LootArea : MonoBehaviour {

	public string type;
	public int lootchance = 5;

	private bool isActive = false;
	private Collider2D playerCollider;

	List<Item> lockerItems = new List<Item> {
		new Item(Item.Type.Weapon, "Baseball bat"),
		new Item(Item.Type.Clothing, "Shirt"),
		new Item(Item.Type.Clothing, "Sports jacket"),
		new Item(Item.Type.Clothing, "Sports jacket"),
		new Item(Item.Type.Clothing, "Sports jacket"),
		new Item(Item.Type.Clothing, "Leather jacket"),
		new Item(Item.Type.Pants, "Sports pants"),
		new Item(Item.Type.Pants, "Sports pants"),
		new Item(Item.Type.Pants, "Sports pants"),
		new Item(Item.Type.Boots, "Leather boots"),
		new Item(Item.Type.Boots, "Sneakers"),
		new Item(Item.Type.Boots, "Sneakers"),
		new Item(Item.Type.Consumable, "MedKit"),
		new Item(Item.Type.Consumable, "MedKit"),
		new Item(Item.Type.Consumable, "MedKit"),
		new Item(Item.Type.Consumable, "MedKit"),
		new Item(Item.Type.Consumable, "MedKit")
	};

	List<Item> closetItems = new List<Item> {
		new Item(Item.Type.Clothing, "Shirt"),
		new Item(Item.Type.Clothing, "Shirt"),
		new Item(Item.Type.Clothing, "Shirt"),
		new Item(Item.Type.Clothing, "Sports jacket"),
		new Item(Item.Type.Clothing, "Leather jacket"),
		new Item(Item.Type.Pants, "Jeans"),
		new Item(Item.Type.Pants, "Jeans"),
		new Item(Item.Type.Pants, "Jeans"),
		new Item(Item.Type.Pants, "Sports pants"),
		new Item(Item.Type.Boots, "Leather boots"),
		new Item(Item.Type.Boots, "Leather boots"),
		new Item(Item.Type.Boots, "Leather boots"),
		new Item(Item.Type.Boots, "Sneakers"),
		new Item(Item.Type.Boots, "Sneakers"),
		new Item(Item.Type.Boots, "Sneakers"),
		new Item(Item.Type.Consumable, "MedKit"),
	};

	Item PickRandomItem (List<Item> list) {
		int result = UnityEngine.Random.Range(0, list.Count);
		return list[result];
	}

	void PromptLooting (Player player) {

		if (Input.GetKey(KeyCode.E)) {

			int random = UnityEngine.Random.Range(0, 100);

			if (random <= lootchance) {

				if (type == "locker") {
					Item newItem = PickRandomItem(lockerItems);
					player.getItem (newItem);
					GlobalData.Gamelog += (Environment.NewLine + "You have found " + newItem.name);
					Destroy(this.gameObject);
				}

				if (type == "closet") {
					Item newItem = PickRandomItem(closetItems);
					player.getItem (newItem);
					GlobalData.Gamelog += (Environment.NewLine + "You have found " + newItem.name);
					Destroy(this.gameObject);
				}
			} else {
				GlobalData.Gamelog += (Environment.NewLine + "You found nothing!");
				Destroy(this.gameObject);
			}

		}
	}

	void OnTriggerEnter2D (Collider2D collider) {

		if (collider.tag == "Player") {
			isActive = true;
			playerCollider = collider;
			Debug.Log (this.name + " has become active!");
		}
	}

	void OnTriggerExit2D (Collider2D collider) {

		if (collider.tag == "Player") {
			isActive = false;
			Debug.Log (this.name + " has become inactive!");
		}
	}

	void Update ()
	{
		if (isActive) {
			PromptLooting (playerCollider.GetComponent<Player> ());
		}
	}
}
