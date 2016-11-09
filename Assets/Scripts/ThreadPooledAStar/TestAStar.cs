using UnityEngine;
using System.Collections;

public class TestAStar : MonoBehaviour {

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

    [Header("Start and end")]
    [SerializeField]
    Vector2 startPos;
    [SerializeField]
    Vector2 endPos;

    void Start()
    {
        //Debug.Log(System.Threading.Thread.CurrentThread.ManagedThreadId);
    }


    void Update()
    {
        if(fakeButtonThreaded == true || loopThreaded == true)
        {
            fakeButtonThreaded = false;

            PathRequest request = new PathRequest(grid.Walkable, startPos, endPos);
            pathFinder.RequestPath(request);
        }


        if (fakeButton == true || loop == true)
        {
            fakeButton = false;

            PathRequest request = new PathRequest(grid.Walkable, startPos, endPos);
            AStar.FindPath(request);
        }
    }




}
