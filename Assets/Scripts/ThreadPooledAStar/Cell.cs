using UnityEngine;

public class Cell
{
    int x, y;
    bool walkable;
    GameObject model;

    public Cell(int x, int y, bool walkable, GameObject model)
    {
        this.x = x;
        this.y = y;
        this.walkable = walkable;
        this.model = model;
    }


    public int X
    {
        get { return x; }
    }

    public int Y
    {
        get { return y; }
    }

    public bool Walkable
    {
        get { return walkable; }
    }

    public GameObject Model
    {
        get { return model; }
    }
}