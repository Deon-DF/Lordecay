using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour {

	private static bool guiExists = false;


	// Stats


	[HideInInspector] public GameObject strengthText;
	[HideInInspector] public GameObject perceptionText;
	[HideInInspector] public GameObject intelligenceText;
	[HideInInspector] public GameObject toughnessText;

	[HideInInspector] public GameObject healthText;
	[HideInInspector] public GameObject sanityText;
	[HideInInspector] public GameObject staminaText;
	[HideInInspector] public GameObject accuracyText;

	[HideInInspector] public GameObject bluntDamageText;
	[HideInInspector] public GameObject pierceDamageText;
	[HideInInspector] public GameObject fireDamageText;
	[HideInInspector] public GameObject coldDamageText;
	[HideInInspector] public GameObject acidDamageText;

	[HideInInspector] public GameObject bluntArmorText;
	[HideInInspector] public GameObject pierceArmorText;
	[HideInInspector] public GameObject fireArmorText;
	[HideInInspector] public GameObject coldArmorText;
	[HideInInspector] public GameObject acidArmorText;


	// Inventory GUI

	[HideInInspector] public GameObject inventoryBG;
	[HideInInspector] public GameObject inventoryTooltip;

	[HideInInspector] public GameObject equipmentBox;
	[HideInInspector] public GameObject equipmentSlotHelmet;
	[HideInInspector] public GameObject equipmentSlotMask;
	[HideInInspector] public GameObject equipmentSlotBodyarmor;
	[HideInInspector] public GameObject equipmentSlotClothing;
	[HideInInspector] public GameObject equipmentSlotPants;
	[HideInInspector] public GameObject equipmentSlotBoots;
	[HideInInspector] public GameObject equipmentSlotWeapon;
	[HideInInspector] public GameObject equipmentSlotOffhand;

	[HideInInspector] public GameObject removeAttachmentsButton;

	[HideInInspector] public GameObject inventoryBox;
	[HideInInspector] public GameObject inventorySlot1;
	[HideInInspector] public GameObject inventorySlot2;
	[HideInInspector] public GameObject inventorySlot3;
	[HideInInspector] public GameObject inventorySlot4;
	[HideInInspector] public GameObject inventorySlot5;
	[HideInInspector] public GameObject inventorySlot6;
	[HideInInspector] public GameObject inventorySlot7;
	[HideInInspector] public GameObject inventorySlot8;
	[HideInInspector] public GameObject inventorySlot9;
	[HideInInspector] public GameObject inventorySlot10;
	[HideInInspector] public GameObject inventorySlot11;
	[HideInInspector] public GameObject inventorySlot12;
	[HideInInspector] public GameObject inventorySlot13;
	[HideInInspector] public GameObject inventorySlot14;
	[HideInInspector] public GameObject inventorySlot15;
	[HideInInspector] public GameObject inventorySlot16;
	[HideInInspector] public GameObject inventorySlot17;
	[HideInInspector] public GameObject inventorySlot18;
	[HideInInspector] public GameObject inventorySlot19;
	[HideInInspector] public GameObject inventorySlot20;
	[HideInInspector] public GameObject inventorySlot21;
	[HideInInspector] public GameObject inventorySlot22;
	[HideInInspector] public GameObject inventorySlot23;
	[HideInInspector] public GameObject inventorySlot24;
	[HideInInspector] public GameObject inventorySlot25;

	[HideInInspector] public GameObject inventorySlot1q;
	[HideInInspector] public GameObject inventorySlot2q;
	[HideInInspector] public GameObject inventorySlot3q;
	[HideInInspector] public GameObject inventorySlot4q;
	[HideInInspector] public GameObject inventorySlot5q;
	[HideInInspector] public GameObject inventorySlot6q;
	[HideInInspector] public GameObject inventorySlot7q;
	[HideInInspector] public GameObject inventorySlot8q;
	[HideInInspector] public GameObject inventorySlot9q;
	[HideInInspector] public GameObject inventorySlot10q;
	[HideInInspector] public GameObject inventorySlot11q;
	[HideInInspector] public GameObject inventorySlot12q;
	[HideInInspector] public GameObject inventorySlot13q;
	[HideInInspector] public GameObject inventorySlot14q;
	[HideInInspector] public GameObject inventorySlot15q;
	[HideInInspector] public GameObject inventorySlot16q;
	[HideInInspector] public GameObject inventorySlot17q;
	[HideInInspector] public GameObject inventorySlot18q;
	[HideInInspector] public GameObject inventorySlot19q;
	[HideInInspector] public GameObject inventorySlot20q;
	[HideInInspector] public GameObject inventorySlot21q;
	[HideInInspector] public GameObject inventorySlot22q;
	[HideInInspector] public GameObject inventorySlot23q;
	[HideInInspector] public GameObject inventorySlot24q;
	[HideInInspector] public GameObject inventorySlot25q;

	[HideInInspector] public GameObject groundBox;
	[HideInInspector] public GameObject leftArrow;
	[HideInInspector] public GameObject rightArrow;
	[HideInInspector] public GameObject groundSlot1;
	[HideInInspector] public GameObject groundSlot2;
	[HideInInspector] public GameObject groundSlot3;
	[HideInInspector] public GameObject groundSlot4;
	[HideInInspector] public GameObject groundSlot5;

	// Player stats and health

	[HideInInspector] public GameObject healthbar;
	[HideInInspector] public GameObject staminabar;
	[HideInInspector] public Image WeaponUI;


	// Enemy stats and health

	[HideInInspector] public GameObject enemyBox;
	[HideInInspector] public GameObject enemyNameUI;
	[HideInInspector] public GameObject enemyStatusUI;
	[HideInInspector] public GameObject enemyHealthBar;
	[HideInInspector] public static string enemyName = "Monster";
	[HideInInspector] public static string enemyStatus = "";


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

		// Do not destroy player after ot was created, do not create new players
		if (!guiExists) {
			guiExists = true;
			DontDestroyOnLoad (transform.gameObject);
		} else {
			Destroy (gameObject);
		}

		// Find UI elements

		healthbar = GameObject.Find ("Healthbar");
		staminabar = GameObject.Find ("Staminabar");
		WeaponUI = GameObject.Find ("WeaponUI").GetComponent<Image> ();

		enemyNameUI = GameObject.Find("EnemyName").gameObject;
		enemyStatusUI = GameObject.Find("EnemyStatus").gameObject;
		enemyBox = GameObject.Find ("EnemyGUI").gameObject;

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
