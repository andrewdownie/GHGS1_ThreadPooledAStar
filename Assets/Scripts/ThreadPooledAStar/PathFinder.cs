using UnityEngine;
using System.Threading;
using System.Collections.Generic;


public class PathFinder : MonoBehaviour
{
    //static SafePathResults spr = new SafePathResults();
    static SafeQueue<PathResult> pathResults = new SafeQueue<PathResult>();

    float timer;
    float timerAmount = 1f;


    static System.Random rnd;

    public static void EnqueuePathResult(PathResult result)
    {
        pathResults.SafeEnque(result);
    }


    // Use this for initialization
    void Start()
    {
        timer = 0;
        rnd = new System.Random();
        ThreadPool.SetMaxThreads(10, 5);
        

    }

    public void RequestPath(PathRequest request)
    {
        Debug.Log("Threaded path request");
        ThreadPool.QueueUserWorkItem(
            new WaitCallback(AStar.FindPath), 
            request
        );
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;


        

        if(timerAmount <= timer)
        {
            //Run();
            timer = 0;

            ReadResult();
        }
        

    }

    void ReadResult()
    {
        if(pathResults.UnsafeIsEmpty == true)
        {
            return;
        }
        

        PathResult pr = pathResults.SafeDequeue();

        while (pr != null)
        {

            if(pr.Path == null || pr.Path.Length == 0)
            {
                Debug.Log("path was null or had zero elements");
                
            }
            else
            {
                Debug.Log("Time taken: " + pr.TimeTaken);
                foreach(Vector2 v in pr.Path)
                {
                    Debug.Log(v.x + ":" + v.y);//Need to print the path found
                }
                
            }

            
 
            pr = pathResults.SafeDequeue();
            
        }

        
    }
    

    

    static void Run()
    {
        
        for (int x = 0; x < 10; x++)
        {
            ThreadPool.QueueUserWorkItem(
                new WaitCallback(AStar.FindPath),
                new PathRequest(null, new Vector2(x, x), new Vector2(x, x))
            );
            
        }
    }
}
