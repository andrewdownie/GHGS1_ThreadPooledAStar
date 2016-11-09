using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
    [SerializeField]
    int width = 20, height = 20;

    [SerializeField]
    GameObject walkableTile, nonWalkableTile;


    GameObject[,] model;
    bool[,] walkable;

    public Cell this[int x, int y]
    {
        get { return new Cell(x, y, walkable[x, y], model[x, y]); }
    }

    public bool[,] Walkable
    {
        get { return (bool[,])walkable.Clone(); }
    }

    void Start()
    {
        model = new GameObject[width, height];
        walkable = new bool[width, height];


        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                bool w = Random.Range(0, 5) > 0;
                walkable[x, y] = w;


                GameObject go;
                if (w)
                {
                    go = (GameObject)Instantiate(walkableTile, new Vector3(x, y), Quaternion.identity, transform);
                }
                else
                {
                    go = (GameObject)Instantiate(nonWalkableTile, new Vector3(x, y), Quaternion.identity, transform);
                }



            }
        }
    }
    



}


