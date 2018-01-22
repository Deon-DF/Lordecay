using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {
	public Tile[,] tiles;
	int width;
	int height;

	public int Width {
		get {
			return width;
		}
	}

	public int Height {
		get {
			return height;
		}
	}

	public Tile GetTileAt (int x, int y) {
		return tiles [x, y];
	}

	public Grid (int width = 100, int height = 100) {
		this.width = width;
		this.height = height;

		tiles = new Tile[width, height];

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				tiles [x, y] = new Tile (x, y, true);
			}
		}
	}
}

