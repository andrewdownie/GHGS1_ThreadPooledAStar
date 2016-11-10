using UnityEngine;
using System.Collections;
using System;

public class TestAStar : MonoBehaviour, PathFinderCallback{

    [SerializeField]
    private Grid grid;

    [SerializeField]
    PathFinder pathFinder;


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


    [SerializeField]
    PathResult lastPathResult;

    void Start()
    {
        endPos = new Vector2(grid.Width - 1, grid.Height - 1);
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int i = 0; i < lastPathResult.Path.Length - 1; i++)
        {
            Gizmos.DrawLine(lastPathResult.Path[i], lastPathResult.Path[i + 1]);
        }
    }

    void PathFinderCallback.PathRequestResult(PathResult result)
    {
        lastPathResult = result;
    }
}
