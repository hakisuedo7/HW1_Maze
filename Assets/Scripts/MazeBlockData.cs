using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBlockData
{
    public MazeBlockData(int x, int y, int wallType, bool visited)
    {
        this.x = x;
        this.y = y;
        this.wallType = wallType;
        this.visited = visited;
    }

    public MazeBlockData(int x, int y, int wallType)
    {
        this.x = x;
        this.y = y;
        this.wallType = wallType;
        this.visited = true;
    }

    public int getX() { return x; }
    public int getY() { return y; }
    public int getWallType() { return wallType; }
    public bool isVisited() { return visited; }
    public void setWallType(int type)
    {
        this.wallType = type;
    }
    public void setVisited()
    {
        this.visited = true;
    }

    private int x, y, wallType;
    private bool visited;
}
