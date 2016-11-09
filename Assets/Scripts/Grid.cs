using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
    [SerializeField]
    int width = 20, height = 20;


    GameObject[,] model;
    bool[,] walkable;

    public Cell this[int x, int y]
    {
        get { return new Cell(x, y, walkable[x, y], model[x, y]); }
    }

    void Start()
    {
        Setup2DArrays();
    }

    void Setup2DArrays()
    {
        model = new GameObject[width, height];
        walkable = new bool[width, height];
    }



}

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
