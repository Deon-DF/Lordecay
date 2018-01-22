using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar {
  
  class Node {
    public int x;
    public int y;
    public float G;
    public float H;
    public float F;
    public Node parent;
    public Tile cell;
    public Node (int x, int y, float G, float F, float H, Node parent, Tile c) {
      this.x = x;
      this.y = y;
      this.G = G;
      this.H = H;
      this.F = F;
      this.parent = parent;
      this.cell = c;
    }
  }
  
  List<Node> openList;
  List<Node> closeList;
  List<Node> neighbours;
  List<Node> finalPath;
  Node start;
  Node end;

  Grid grid;
  int mapWidth;
  int mapHeight;
  
  public AStar () {
    openList = new List<Node>();
    closeList = new List<Node>();
    neighbours = new List<Node>();
    finalPath = new List<Node>();
  }
  
	public List<Tile> FindPath(Tile startCell, Tile goalCell, Grid grid, bool targetCellMustBeFree) {
    this.grid = grid;
	this.mapWidth = grid.Width;
	this.mapHeight = grid.Height;
    
    start = new Node((int)startCell.X, (int)startCell.Y, 0, 0, 0, null, startCell);
    end = new Node((int)goalCell.X, (int)goalCell.Y, 0, 0, 0, null, goalCell);
    openList.Add(start);
    bool keepSearching = true;
    bool pathExists = true;
    
    while ((keepSearching) && (pathExists)) {
      Node currentNode = ExtractBestNodeFromOpenList();
      if (currentNode == null) {
        pathExists = false;
        break;
      }
      closeList.Add(currentNode);
      if (NodeIsGoal(currentNode))
      keepSearching = false;
      else {
        if (targetCellMustBeFree)
        //FindValidFourNeighbours(currentNode); for x/y movement only
		  FindValidSixNeighbours(currentNode); // for diagonal movement
        else
        //FindValidFourNeighboursIgnoreTargetCell(currentNode); for x/y movement only
		  FindValidSixNeighboursIgnoreTargetCell(currentNode); // for diagonal movement

        foreach (Node neighbour in neighbours) {
          if (FindInCloseList(neighbour) != null)
          continue;
          Node inOpenList = FindInOpenList(neighbour);
          if (inOpenList == null) {
            openList.Add (neighbour);
          }
          else {
            if (neighbour.G < inOpenList.G) {
              inOpenList.G = neighbour.G;
              inOpenList.F = inOpenList.G + inOpenList.H;
              inOpenList.parent = currentNode;
            }
          }   
        }
      }
    }
    
    if (pathExists) {
      Node n = FindInCloseList(end);
      while (n != null) {
        finalPath.Add (n);
        n = n.parent;
      }
    }

	finalPath.Reverse ();
	List<Tile> tilepath = new List<Tile>();
	foreach (Node n in finalPath) {
		tilepath.Add(grid.GetTileAt(n.x, n.y));
		//Debug.Log (n.x + " and " + n.y);
	}
	return tilepath;
  }

  public List<int> PointsFromPath() {
    List<int> points = new List<int>();
    foreach (Node n in finalPath) {
      points.Add (n.x);
      points.Add (n.y);   
    }
    return points;
  }
  
  public List<Tile> CellsFromPath() {
    List<Tile> path = new List<Tile> ();
    foreach (Node n in finalPath) {
      path.Add(n.cell);   
    }

    if (path.Count != 0) {
      path.Reverse ();
      path.RemoveAt(0);
    }
    return path;
  }
  
  Node ExtractBestNodeFromOpenList() {
    float minF = float.MaxValue;
    Node bestOne = null;
    foreach (Node n in openList) {
      if (n.F < minF) {
        minF = n.F;
        bestOne = n;
      }
    }
    if (bestOne != null)
    openList.Remove(bestOne);
    return bestOne;
  }
  
  bool NodeIsGoal(Node node) {
    return ((node.x == end.x) && (node.y == end.y));
  }
  
  void FindValidFourNeighbours(Node n) {
    neighbours.Clear();
		if ((n.y-1 >= 0) && ((grid.GetTileAt(n.x, n.y-1).IsPassable))) {
      Node vn = PrepareNewNodeFrom(n, 0, -1);
      neighbours.Add (vn);
    }
		if ((n.y+1 <= mapHeight-1) && ((grid.GetTileAt(n.x, n.y+1).IsPassable))) {
      Node vn = PrepareNewNodeFrom(n, 0, +1);
      neighbours.Add (vn);
    }
		if ((n.x-1 >= 0) && ((grid.GetTileAt(n.x-1, n.y).IsPassable))){
      Node vn = PrepareNewNodeFrom(n, -1, 0);
      neighbours.Add (vn);
    }
		if ((n.x+1 <= mapWidth-1) && ((grid.GetTileAt(n.x+1, n.y).IsPassable))){
      Node vn = PrepareNewNodeFrom(n, 1, 0);
      neighbours.Add (vn);
    }
  }

	void FindValidSixNeighbours(Node n) {
		neighbours.Clear();
		if ((n.y-1 >= 0) && ((grid.GetTileAt(n.x, n.y-1).IsPassable))) {
			Node vn = PrepareNewNodeFrom(n, 0, -1);
			neighbours.Add (vn);
		}
		if ((n.y+1 <= mapHeight-1) && ((grid.GetTileAt(n.x, n.y+1).IsPassable))) {
			Node vn = PrepareNewNodeFrom(n, 0, +1);
			neighbours.Add (vn);
		}
		if ((n.x-1 >= 0) && ((grid.GetTileAt(n.x-1, n.y).IsPassable))){
			Node vn = PrepareNewNodeFrom(n, -1, 0);
			neighbours.Add (vn);
		}
		if ((n.x+1 <= mapWidth-1) && ((grid.GetTileAt(n.x+1, n.y).IsPassable))){
			Node vn = PrepareNewNodeFrom(n, +1, 0);
			neighbours.Add (vn);
		}

		if ((n.x+1 <= mapWidth-1) && (n.y+1 <= mapHeight-1) && ((grid.GetTileAt(n.x+1, n.y+1).IsPassable))){
			Node vn = PrepareNewNodeFrom(n, +1, +1);
			neighbours.Add (vn);
		}

		if ((n.x+1 <= mapWidth-1) && (n.y-1 >= 0) && ((grid.GetTileAt(n.x+1, n.y-1).IsPassable))){
			Node vn = PrepareNewNodeFrom(n, +1, -1);
			neighbours.Add (vn);
		}

		if ((n.x-1 >= 0) && (n.y+1 <= mapHeight-1) && ((grid.GetTileAt(n.x-1, n.y+1).IsPassable))){
			Node vn = PrepareNewNodeFrom(n, -1, +1);
			neighbours.Add (vn);
		}
	
		if ((n.x-1 >= 0) && (n.y-1 >= 0) && ((grid.GetTileAt(n.x-1, n.y-1).IsPassable))){
			Node vn = PrepareNewNodeFrom(n, -1, -1);
			neighbours.Add (vn);
		}
	}

  /* Last cell does not need to be walkable in the game */
  void FindValidFourNeighboursIgnoreTargetCell(Node n) {
    neighbours.Clear();
		if ((n.y-1 >= 0) && ((grid.GetTileAt(n.x, n.y-1).IsPassable) || grid.GetTileAt(n.x, n.y-1) == end.cell)) {
      Node vn = PrepareNewNodeFrom(n, 0, -1);
      neighbours.Add (vn);
    }
		if ((n.y+1 <= mapHeight-1) && ((grid.GetTileAt(n.x, n.y+1).IsPassable) || grid.GetTileAt(n.x, n.y+1) == end.cell)) {
      Node vn = PrepareNewNodeFrom(n, 0, +1);
      neighbours.Add (vn);
    }
		if ((n.x-1 >= 0) && ((grid.GetTileAt(n.x-1, n.y).IsPassable) || grid.GetTileAt(n.x-1, n.y) == end.cell)){
      Node vn = PrepareNewNodeFrom(n, -1, 0);
      neighbours.Add (vn);
    }
		if ((n.x+1 <= mapWidth-1) && ((grid.GetTileAt(n.x+1, n.y).IsPassable) || grid.GetTileAt(n.x+1, n.y) == end.cell)){
      Node vn = PrepareNewNodeFrom(n, 1, 0);
      neighbours.Add (vn);
    }
  }

	/* Last cell does not need to be walkable in the game */
	void FindValidSixNeighboursIgnoreTargetCell(Node n) {
		neighbours.Clear();
		if ((n.y-1 >= 0) && ((grid.GetTileAt(n.x, n.y-1).IsPassable) || grid.GetTileAt(n.x, n.y-1) == end.cell)) {
			Node vn = PrepareNewNodeFrom(n, 0, -1);
			neighbours.Add (vn);
		}
		if ((n.y+1 <= mapHeight-1) && ((grid.GetTileAt(n.x, n.y+1).IsPassable) || grid.GetTileAt(n.x, n.y+1) == end.cell)) {
			Node vn = PrepareNewNodeFrom(n, 0, +1);
			neighbours.Add (vn);
		}
		if ((n.x-1 >= 0) && ((grid.GetTileAt(n.x-1, n.y).IsPassable) || grid.GetTileAt(n.x-1, n.y) == end.cell)){
			Node vn = PrepareNewNodeFrom(n, -1, 0);
			neighbours.Add (vn);
		}
		if ((n.x+1 <= mapWidth-1) && ((grid.GetTileAt(n.x+1, n.y).IsPassable) || grid.GetTileAt(n.x+1, n.y) == end.cell)){
			Node vn = PrepareNewNodeFrom(n, 1, 0);
			neighbours.Add (vn);
		}
		if ((n.x+1 <= mapWidth-1) && (n.y+1 <= mapHeight-1) && ((grid.GetTileAt(n.x+1, n.y+1).IsPassable) || grid.GetTileAt(n.x+1, n.y+1) == end.cell)){
			Node vn = PrepareNewNodeFrom(n, 1, 1);
			neighbours.Add (vn);
		}
		if ((n.x-1 >=0) && (n.y-1 >= 0) && ((grid.GetTileAt(n.x-1, n.y-1).IsPassable) || grid.GetTileAt(n.x-1, n.y-1) == end.cell)){
			Node vn = PrepareNewNodeFrom(n, -1, -1);
			neighbours.Add (vn);
		}
		if ((n.x+1 <= mapWidth-1) && (n.y-1 >= 0) && ((grid.GetTileAt(n.x+1, n.y-1).IsPassable) || grid.GetTileAt(n.x+1, n.y-1) == end.cell)){
			Node vn = PrepareNewNodeFrom(n, 1, -1);
			neighbours.Add (vn);
		}
		if ((n.x-1 >=0) && (n.y+1 <= mapHeight-1) && ((grid.GetTileAt(n.x-1, n.y+1).IsPassable) || grid.GetTileAt(n.x-1, n.y+1) == end.cell)){
			Node vn = PrepareNewNodeFrom(n, -1, +1);
			neighbours.Add (vn);
		}
	}
  
  Node PrepareNewNodeFrom(Node n, int x, int y) {
		Node newNode = new Node(n.x + x, n.y + y, 0, 0, 0, n, grid.GetTileAt(n.x + x, n.y + y));
    newNode.G = n.G + MovementCost(n, newNode);
    newNode.H = Heuristic(newNode);
    newNode.F = newNode.G + newNode.H;
    newNode.parent = n;
    return newNode;
  }
  
  float Heuristic (Node n) {
    return Mathf.Sqrt((n.x - end.x)*(n.x - end.x) + (n.y - end.y)*(n.y - end.y));
  }
  
  float MovementCost(Node a, Node b) {
		return grid.GetTileAt(b.x, b.y).MovementCost;
  }
  
  Node FindInCloseList(Node n) {
    foreach (Node nn in closeList) {
      if ((nn.x == n.x) && (nn.y == n.y))
      return nn;
    }
    return null;
  }
  
  Node FindInOpenList(Node n) {
    foreach (Node nn in openList) {
      if ((nn.x == n.x) && (nn.y == n.y))
      return nn;
    }
    return null;
  }
}