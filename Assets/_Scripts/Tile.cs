using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
	int x;
	int y;
	bool isPassable;

	public float MovementCost = 1f;
	public List<Item> groundItems;

	public bool IsPassable {
		get {
			return isPassable;
		}

		set {
			isPassable = value;
		}
	}

	public int X {
		get {
			return x;
		}
	}

	public int Y {
		get {
			return y;
		}
	}

	public Tile (int X, int Y, bool Passable) {
		x = X;
		y = Y;
		isPassable = Passable;
		groundItems = new List<Item> () ;
	}

}
