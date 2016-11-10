using UnityEngine;
using System.Threading;
using System.Collections.Generic;


public class PathFinder : MonoBehaviour
{
    
    void Start()
    {
        ThreadPool.SetMaxThreads(10, 5);
    }

    public void RequestPath(PathRequest request)
    {
        //Debug.Log("Threaded path request");
        ThreadPool.QueueUserWorkItem(
            new WaitCallback(AStar.FindPath), 
            request
        );
    }

    
}
