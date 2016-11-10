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
    KeyCode runThreaded;
    [SerializeField]
    bool fakeButtonThreaded;
    [SerializeField]
    bool loopThreaded;

    [Header("Testing non-Threaded")]
    [SerializeField]
    KeyCode runNonThreaded;
    [SerializeField]
    bool fakeButton;
    [SerializeField]
    bool loop;

    [Header("Start pos (end defaults to [max, max])")]
    [SerializeField]
    Vector2 startPos;

    Vector2 endPos;

    System.Diagnostics.Stopwatch stopwatch;
    double latency;
    double lastTimeTaken;
    List<GameObject> pathRep;

    bool pathNotFound;

    void Start()
    {
        endPos = new Vector2(grid.Width - 1, grid.Height - 1);
        pathRep = new List<GameObject>();
        latency = 0;
        pathNotFound = false;
    }


    void Update()
    {
        if(Input.GetKey(runThreaded) || fakeButtonThreaded == true || loopThreaded == true)
        {
            fakeButtonThreaded = false;
            ClearPathRep();
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
            PathRequest request = new PathRequest(grid.Walkable, startPos, endPos, this);
            pathFinder.RequestPath(request);
        }


        if (Input.GetKey(runNonThreaded) || fakeButton == true || loop == true)
        {
            ClearPathRep();
            fakeButton = false;
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
            PathRequest request = new PathRequest(grid.Walkable, startPos, endPos, this);
            AStar.FindPath(request);
        }
    }
    

    void ClearPathRep()
    {
        if (pathRep != null)
        {
            for (int i = pathRep.Count - 1; i >= 0; i--)
            {
                Destroy(pathRep[i]);
            }

            pathRep.Clear();
        }
    }

    void PathFinderCallback.PathRequestResult(PathResult result)
    {
        lastTimeTaken = result.TimeTaken;
        latency = stopwatch.Elapsed.TotalMilliseconds;
        stopwatch.Stop();
        

        if(result.Path == null)
        {
            pathNotFound = true;
            return;
        }


        foreach(Vector2 v in result.Path)
        {
            GameObject go = (GameObject)Instantiate(pathModel, new Vector3(v.x, grid.FloorLevel, v.y), Quaternion.identity, transform);
            pathRep.Add(go);
        }
    }


    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 500, 22), "Paths run from bottom left to top right:");
        GUI.Label(new Rect(20, 40, 500, 22), "\tPress '" + runNonThreaded.ToString() + "' to request path on the main unity thread");
        GUI.Label(new Rect(20, 60, 500, 22), "\tPress '" + runThreaded.ToString() + "' to request path on a thread-pool thread");


        if (latency > 0)
        {
            GUI.Label(new Rect(20, 80, 500, 22), "AStar time taken: " + lastTimeTaken + " milliseconds");
        }
        if (latency > 0)
        {
            GUI.Label(new Rect(20, 100, 500, 22), "Request latency: " + latency + " milliseconds");
        }
       

        if (pathNotFound)
        {
            GUI.Label(new Rect(20, 120, 500, 22), "PATH NOT POSSIBLE");
        }
    }
}
