using UnityEngine;
using System.Threading;
using System.Collections.Generic;


public class PathFinder : MonoBehaviour
{
    static SafePathResults spr = new SafePathResults();

    float timer;
    float timerAmount = 1f;


    static System.Random rnd;

    // Use this for initialization
    void Start()
    {
        timer = 0;
        rnd = new System.Random();
        ThreadPool.SetMaxThreads(10, 5);
        

    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;


        ReadResult();

        if(timerAmount <= timer)
        {
            Run();
            timer = 0;
        }
        

    }

    void ReadResult()
    {

        PathResult pr = spr.PopResult();

        while (pr != null)
        {
            Debug.Log(pr[0].X + ":" + pr[0].Y);//Need to print the path found

            pr = spr.PopResult();
        }

        
    }
    

    static void DoSomething(object n)//Astar goes here
    {
        int num = (int)n;
        spr.AddResult(
            new PathResult(
                new VInt2[] { new VInt2(num, rnd.Next(0, 2)) }
            )
        );
        
    }

    static void Run()
    {
        
        for (int x = 0; x < 10; x++)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(DoSomething), x);//x becomes the request,
            
        }
    }
}
