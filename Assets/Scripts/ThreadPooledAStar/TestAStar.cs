using UnityEngine;
using System.Collections.Generic;
using System;

public class TestAStar : MonoBehaviour, PathFinderCallback{

    [SerializeField]
    private Grid grid;

    [SerializeField]
    PathFinder pathFinder;

    [SerializeField]
    GameObject pathModel;


    [Header("Testing Threaded")]
    [SerializeField]
    bool fakeButtonThreaded;
    [SerializeField]
    bool loopThreaded;

    [Header("Testing non-Threaded")]
    [SerializeField]
    bool fakeButton;
    [SerializeField]
    bool loop;

    [Header("Start pos (end defaults to [max, max])")]
    [SerializeField]
    Vector2 startPos;

    Vector2 endPos;



    List<GameObject> pathRep;

    void Start()
    {
        endPos = new Vector2(grid.Width - 1, grid.Height - 1);
        pathRep = new List<GameObject>();
        //Debug.Log(System.Threading.Thread.CurrentThread.ManagedThreadId);
    }


    void Update()
    {
        if(fakeButtonThreaded == true || loopThreaded == true)
        {
            fakeButtonThreaded = false;

            PathRequest request = new PathRequest(grid.Walkable, startPos, endPos, this);
            pathFinder.RequestPath(request);
        }


        if (fakeButton == true || loop == true)
        {
            fakeButton = false;

            PathRequest request = new PathRequest(grid.Walkable, startPos, endPos, this);
            AStar.FindPath(request);
        }
    }
    

    void PathFinderCallback.PathRequestResult(PathResult result)
    {
        if(pathRep != null) {
            for(int i =  pathRep.Count - 1; i > 0; i--)
            {
                Destroy(pathRep[i]);
            }

            pathRep.Clear();
        }


        foreach(Vector2 v in result.Path)
        {
            GameObject go = (GameObject)Instantiate(pathModel, new Vector3(v.x, grid.FloorLevel, v.y), Quaternion.identity, transform);
            pathRep.Add(go);
        }
    }
}
