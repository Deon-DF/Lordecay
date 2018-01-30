using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI : MonoBehaviour {

	public GameObject inventoryBG;
	public GameObject inventoryTooltip;

	// Stats

	public GameObject strengthText;
	public GameObject perceptionText;
	public GameObject intelligenceText;
	public GameObject toughnessText;

	public GameObject healthText;
	public GameObject sanityText;
	public GameObject staminaText;
	public GameObject accuracyText;

	public GameObject bluntDamageText;
	public GameObject pierceDamageText;
	public GameObject fireDamageText;
	public GameObject coldDamageText;
	public GameObject acidDamageText;

	public GameObject bluntArmorText;
	public GameObject pierceArmorText;
	public GameObject fireArmorText;
	public GameObject coldArmorText;
	public GameObject acidArmorText;

	// Inventory GUI

	public GameObject equipmentBox;
	public GameObject equipmentSlotHelmet;
	public GameObject equipmentSlotMask;
	public GameObject equipmentSlotBodyarmor;
	public GameObject equipmentSlotClothing;
	public GameObject equipmentSlotPants;
	public GameObject equipmentSlotBoots;
	public GameObject equipmentSlotWeapon;
	public GameObject equipmentSlotOffhand;

	public GameObject removeAttachmentsButton;

	public GameObject inventoryBox;
	public GameObject inventorySlot1;
	public GameObject inventorySlot2;
	public GameObject inventorySlot3;
	public GameObject inventorySlot4;
	public GameObject inventorySlot5;
	public GameObject inventorySlot6;
	public GameObject inventorySlot7;
	public GameObject inventorySlot8;
	public GameObject inventorySlot9;
	public GameObject inventorySlot10;
	public GameObject inventorySlot11;
	public GameObject inventorySlot12;
	public GameObject inventorySlot13;
	public GameObject inventorySlot14;
	public GameObject inventorySlot15;
	public GameObject inventorySlot16;
	public GameObject inventorySlot17;
	public GameObject inventorySlot18;
	public GameObject inventorySlot19;
	public GameObject inventorySlot20;
	public GameObject inventorySlot21;
	public GameObject inventorySlot22;
	public GameObject inventorySlot23;
	public GameObject inventorySlot24;
	public GameObject inventorySlot25;

	public GameObject inventorySlot1q;
	public GameObject inventorySlot2q;
	public GameObject inventorySlot3q;
	public GameObject inventorySlot4q;
	public GameObject inventorySlot5q;
	public GameObject inventorySlot6q;
	public GameObject inventorySlot7q;
	public GameObject inventorySlot8q;
	public GameObject inventorySlot9q;
	public GameObject inventorySlot10q;
	public GameObject inventorySlot11q;
	public GameObject inventorySlot12q;
	public GameObject inventorySlot13q;
	public GameObject inventorySlot14q;
	public GameObject inventorySlot15q;
	public GameObject inventorySlot16q;
	public GameObject inventorySlot17q;
	public GameObject inventorySlot18q;
	public GameObject inventorySlot19q;
	public GameObject inventorySlot20q;
	public GameObject inventorySlot21q;
	public GameObject inventorySlot22q;
	public GameObject inventorySlot23q;
	public GameObject inventorySlot24q;
	public GameObject inventorySlot25q;

	public GameObject groundBox;
	public GameObject leftArrow;
	public GameObject rightArrow;
	public GameObject groundSlot1;
	public GameObject groundSlot2;
	public GameObject groundSlot3;
	public GameObject groundSlot4;
	public GameObject groundSlot5;

	// Enemy stats and health

	public GameObject enemyBox;
	public GameObject enemyNameUI;
	public GameObject enemyStatusUI;
	public GameObject enemyHealthBar;
	public static string enemyName = "";
	public static string enemyStatus = "";


	public GameObject GetGroundSlotByIndex (int index) {
		switch (index) {
		case 0:
			return groundSlot1;
		case 1:
			return groundSlot2;
		case 2:
			return groundSlot3;
		case 3:
			return groundSlot4;
		case 4:
			return groundSlot5;
		default:
			Debug.LogError ("Wrong index for GetGroundSlotByIndex specified: " + index);
			return null;
		}
	}

	public GameObject GetSlotByIndex (int index) {
		switch (index) {
		case 0:
			return inventorySlot1;
		case 1:
			return inventorySlot2;
		case 2:
			return inventorySlot3;
		case 3:
			return inventorySlot4;
		case 4:
			return inventorySlot5;
		case 5:
			return inventorySlot6;
		case 6:
			return inventorySlot7;
		case 7:
			return inventorySlot8;
		case 8:
			return inventorySlot9;
		case 9:
			return inventorySlot10;
		case 10:
			return inventorySlot11;
		case 11:
			return inventorySlot12;
		case 12:
			return inventorySlot13;
		case 13:
			return inventorySlot14;
		case 14:
			return inventorySlot15;
		case 15:
			return inventorySlot16;
		case 16:
			return inventorySlot17;
		case 17:
			return inventorySlot18;
		case 18:
			return inventorySlot19;
		case 19:
			return inventorySlot20;
		case 20:
			return inventorySlot21;
		case 21:
			return inventorySlot22;
		case 22:
			return inventorySlot23;
		case 23:
			return inventorySlot24;
		case 24:
			return inventorySlot25;
		default:
			Debug.LogError ("Wrong index for GetInventorySlotByIndex specified: " + index);
			return null;
		}
	}

	public GameObject GetSlotQByIndex (int index) {
		switch (index) {
		case 0:
			return inventorySlot1q;
		case 1:
			return inventorySlot2q;
		case 2:
			return inventorySlot3q;
		case 3:
			return inventorySlot4q;
		case 4:
			return inventorySlot5q;
		case 5:
			return inventorySlot6q;
		case 6:
			return inventorySlot7q;
		case 7:
			return inventorySlot8q;
		case 8:
			return inventorySlot9q;
		case 9:
			return inventorySlot10q;
		case 10:
			return inventorySlot11q;
		case 11:
			return inventorySlot12q;
		case 12:
			return inventorySlot13q;
		case 13:
			return inventorySlot14q;
		case 14:
			return inventorySlot15q;
		case 15:
			return inventorySlot16q;
		case 16:
			return inventorySlot17q;
		case 17:
			return inventorySlot18q;
		case 18:
			return inventorySlot19q;
		case 19:
			return inventorySlot20q;
		case 20:
			return inventorySlot21q;
		case 21:
			return inventorySlot22q;
		case 22:
			return inventorySlot23q;
		case 23:
			return inventorySlot24q;
		case 24:
			return inventorySlot25q;
		default:
			Debug.LogError ("Wrong index for GetInventorySlotQByIndex specified: " + index);
			return null;
		}
	}

	void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
	{
		GameObject myLine = new GameObject();
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.startColor = color;
		lr.startWidth = 0.1f;
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
		GameObject.Destroy(myLine, duration);
	}

	void Awake () {

		DontDestroyOnLoad (transform.gameObject);

		// Find UI elements

		enemyBox = GameObject.Find ("EnemyGUI").gameObject;
		enemyNameUI = enemyBox.transform.Find("EnemyName").gameObject;
		enemyStatusUI = enemyBox.transform.Find("EnemyStatus").gameObject;
		enemyHealthBar = enemyBox.transform.Find("EnemyHealthBar").gameObject;

		//enemyBox.SetActive (false);

		inventoryBG = GameObject.Find ("InventoryBackground").gameObject;
		inventoryTooltip = GameObject.Find ("InventoryTooltip").gameObject;

		strengthText = GameObject.Find("StrengthText");
		perceptionText = GameObject.Find("PerceptionText");
		intelligenceText = GameObject.Find("IntelligenceText");
		toughnessText = GameObject.Find("ToughnessText");

		healthText = GameObject.Find ("HealthText");
		sanityText = GameObject.Find ("SanityText");
		staminaText = GameObject.Find ("StaminaText");
		accuracyText = GameObject.Find ("AccuracyText");

		bluntDamageText = GameObject.Find("Blunt damage");
		pierceDamageText = GameObject.Find("Pierce damage");
		fireDamageText = GameObject.Find("Fire damage");
		coldDamageText = GameObject.Find("Cold damage");
		acidDamageText = GameObject.Find("Acid damage");

		bluntArmorText = GameObject.Find ("Blunt armor");
		pierceArmorText = GameObject.Find ("Pierce armor");
		fireArmorText = GameObject.Find ("Fire armor");
		coldArmorText = GameObject.Find ("Cold armor");
		acidArmorText = GameObject.Find ("Acid armor");

		equipmentBox = GameObject.Find ("EquipmentBox");
		equipmentSlotHelmet = equipmentBox.transform.Find ("EquipmentSlotHelmet").gameObject;
		equipmentSlotMask = equipmentBox.transform.Find ("EquipmentSlotMask").gameObject;
		equipmentSlotBodyarmor = equipmentBox.transform.Find ("EquipmentSlotBodyarmor").gameObject;
		equipmentSlotClothing = equipmentBox.transform.Find ("EquipmentSlotClothing").gameObject;
		equipmentSlotPants = equipmentBox.transform.Find ("EquipmentSlotPants").gameObject;
		equipmentSlotBoots = equipmentBox.transform.Find ("EquipmentSlotBoots").gameObject;
		equipmentSlotWeapon = equipmentBox.transform.Find ("EquipmentSlotWeapon").gameObject;
		equipmentSlotOffhand = equipmentBox.transform.Find ("EquipmentSlotOffhand").gameObject;

		removeAttachmentsButton = equipmentBox.transform.Find ("RemoveAttachments").gameObject;


		groundBox = GameObject.Find ("GroundBox");
		leftArrow = groundBox.transform.Find ("LeftArrow").gameObject;
		rightArrow = groundBox.transform.Find ("RightArrow").gameObject;
		groundSlot1 = groundBox.transform.Find ("GroundSlot1").gameObject;
		groundSlot2 = groundBox.transform.Find ("GroundSlot2").gameObject;
		groundSlot3 = groundBox.transform.Find ("GroundSlot3").gameObject;
		groundSlot4 = groundBox.transform.Find ("GroundSlot4").gameObject;
		groundSlot5 = groundBox.transform.Find ("GroundSlot5").gameObject;

		inventoryBox = GameObject.Find ("InventoryBox");
		inventorySlot1 = inventoryBox.transform.Find ("InventorySlot1").gameObject;
		inventorySlot1q = inventorySlot1.transform.Find ("Quantity").gameObject;
		inventorySlot2 = inventoryBox.transform.Find ("InventorySlot2").gameObject;
		inventorySlot2q = inventorySlot2.transform.Find ("Quantity").gameObject;
		inventorySlot3 = inventoryBox.transform.Find ("InventorySlot3").gameObject;
		inventorySlot3q = inventorySlot3.transform.Find ("Quantity").gameObject;
		inventorySlot4 = inventoryBox.transform.Find ("InventorySlot4").gameObject;
		inventorySlot4q = inventorySlot4.transform.Find ("Quantity").gameObject;
		inventorySlot5 = inventoryBox.transform.Find ("InventorySlot5").gameObject;
		inventorySlot5q = inventorySlot5.transform.Find ("Quantity").gameObject;
		inventorySlot6 = inventoryBox.transform.Find ("InventorySlot6").gameObject;
		inventorySlot6q = inventorySlot6.transform.Find ("Quantity").gameObject;
		inventorySlot7 = inventoryBox.transform.Find ("InventorySlot7").gameObject;
		inventorySlot7q = inventorySlot7.transform.Find ("Quantity").gameObject;
		inventorySlot8 = inventoryBox.transform.Find ("InventorySlot8").gameObject;
		inventorySlot8q = inventorySlot8.transform.Find ("Quantity").gameObject;
		inventorySlot9 = inventoryBox.transform.Find ("InventorySlot9").gameObject;
		inventorySlot9q = inventorySlot9.transform.Find ("Quantity").gameObject;
		inventorySlot10 = inventoryBox.transform.Find ("InventorySlot10").gameObject;
		inventorySlot10q = inventorySlot10.transform.Find ("Quantity").gameObject;
		inventorySlot11 = inventoryBox.transform.Find ("InventorySlot11").gameObject;
		inventorySlot11q = inventorySlot11.transform.Find ("Quantity").gameObject;
		inventorySlot12 = inventoryBox.transform.Find ("InventorySlot12").gameObject;
		inventorySlot12q = inventorySlot12.transform.Find ("Quantity").gameObject;
		inventorySlot13 = inventoryBox.transform.Find ("InventorySlot13").gameObject;
		inventorySlot13q = inventorySlot13.transform.Find ("Quantity").gameObject;
		inventorySlot14 = inventoryBox.transform.Find ("InventorySlot14").gameObject;
		inventorySlot14q = inventorySlot14.transform.Find ("Quantity").gameObject;
		inventorySlot15 = inventoryBox.transform.Find ("InventorySlot15").gameObject;
		inventorySlot15q = inventorySlot15.transform.Find ("Quantity").gameObject;
		inventorySlot16 = inventoryBox.transform.Find ("InventorySlot16").gameObject;
		inventorySlot16q = inventorySlot16.transform.Find ("Quantity").gameObject;
		inventorySlot17 = inventoryBox.transform.Find ("InventorySlot17").gameObject;
		inventorySlot17q = inventorySlot17.transform.Find ("Quantity").gameObject;
		inventorySlot18 = inventoryBox.transform.Find ("InventorySlot18").gameObject;
		inventorySlot18q = inventorySlot10.transform.Find ("Quantity").gameObject;
		inventorySlot19 = inventoryBox.transform.Find ("InventorySlot19").gameObject;
		inventorySlot19q = inventorySlot19.transform.Find ("Quantity").gameObject;
		inventorySlot20 = inventoryBox.transform.Find ("InventorySlot20").gameObject;
		inventorySlot20q = inventorySlot20.transform.Find ("Quantity").gameObject;
		inventorySlot21 = inventoryBox.transform.Find ("InventorySlot21").gameObject;
		inventorySlot21q = inventorySlot21.transform.Find ("Quantity").gameObject;
		inventorySlot22 = inventoryBox.transform.Find ("InventorySlot22").gameObject;
		inventorySlot22q = inventorySlot22.transform.Find ("Quantity").gameObject;
		inventorySlot23 = inventoryBox.transform.Find ("InventorySlot23").gameObject;
		inventorySlot23q = inventorySlot23.transform.Find ("Quantity").gameObject;
		inventorySlot24 = inventoryBox.transform.Find ("InventorySlot24").gameObject;
		inventorySlot24q = inventorySlot24.transform.Find ("Quantity").gameObject;
		inventorySlot25 = inventoryBox.transform.Find ("InventorySlot25").gameObject;
		inventorySlot25q = inventorySlot25.transform.Find ("Quantity").gameObject;

		inventoryBG.SetActive (false);
	}

}
