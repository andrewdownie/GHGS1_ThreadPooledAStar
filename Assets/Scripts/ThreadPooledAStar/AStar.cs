using UnityEngine;
using System.Collections.Generic;

public static class AStar {

	public static void FindPath(object pathRequest)
    {
        System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();


        PathRequest pr = (PathRequest)pathRequest;
        List<Vector2> openSet = new List<Vector2>();
        List<Vector2> closedSet = new List<Vector2>();


        int gridWidth = pr.grid.GetLength(0);
        int gridHeight = pr.grid.GetLength(1);
        Vector2[,] parent = new Vector2[gridWidth, gridHeight];
        int[,] hCost = new int[gridWidth, gridHeight];
        int[,] fCost = new int[gridWidth, gridHeight];

        Vector2 current = pr.startPos;
        Vector2 target = pr.endPos;
        int currentHCost = Distance(current, target);

        openSet.Add(current);

        while (true)
        {
            if(openSet.Count == 0)
            {
                LogOnMain("Could not find target node");
                stopwatch.Stop();
                PathFinder.EnqueuePathResult(new PathResult(null, pr.requester, stopwatch.ElapsedMilliseconds));
                return;
            }


            current = LowestFCost(fCost, hCost, openSet);
            currentHCost = hCost[ (int)current.x, (int)current.y ];
            openSet.Remove(current);
            closedSet.Add(current);


            if(current == target)
            {
                break;
            }


            ///
            /// Go thorugh each neighbour, and do the entire algo
            ///
            for (int offX = -1; offX <= 1; offX++)
            {
                for (int offY = -1; offY <= 1; offY++)
                {
                    int curX = Mathf.RoundToInt(current.x);
                    int curY = Mathf.RoundToInt(current.y);

                    if(curX + offX < 0 || curX + offX >= pr.grid.GetLength(0))
                    {
                        continue;
                    }
                    if (curY + offY < 0 || curY + offY >= pr.grid.GetLength(1))
                    {
                        continue;
                    }

                    int vX = curX + offX;
                    int vY = curY + offY;
                    Vector2 v = new Vector2(vX, vY);

                    if(pr.grid[vX, vY] == false || closedSet.Contains(v))
                    {
                        continue;
                    }
                    hCost[vX, vY] = Distance(v, target);

                    int dist = Distance(current, v);

                    if (openSet.Contains(v) == false ||
                       gCost(fCost, hCost, curX, curY) + dist < gCost(fCost, hCost, vX, vY) )
                    {
                        fCost[vX, vY] = fCost[curX, curY] + dist;
                        parent[vX, vY] = current;

                        if(openSet.Contains(v) == false)
                        {
                            openSet.Add(v);
                        }
                    }

                }
            }
        }




        stopwatch.Stop();
        PathResult result = new PathResult(RebuildPath(pr.startPos, pr.endPos, parent), pr.requester, stopwatch.ElapsedMilliseconds);
        PathFinder.EnqueuePathResult(result);
    }


    private static Vector2 LowestFCost(int[,] fCost, int[,] hCost, List<Vector2> vList)
    {
        Vector2 lowest = vList[0];


        foreach(Vector2 v in vList)
        {
            if (fCost[(int)v.x, (int)v.y] < fCost[(int)lowest.x, (int)lowest.y])
            {
                lowest = v;
            }
            else if(fCost[(int)v.x, (int)v.y] == fCost[(int)lowest.x, (int)lowest.y])
            {
                if(gCost(fCost, hCost, (int)v.x, (int)v.y) < gCost(fCost, hCost, (int)lowest.x, (int)lowest.y)){
                    lowest = v;
                }
            }
        }


        return lowest;
    }

    private static Vector2[] RebuildPath(Vector2 startNode, Vector2 endNode, Vector2[,] parent)
    {


        List<Vector2> path = new List<Vector2>();
        Vector2 current = endNode;

        //LogOnMain("Current v2: " + current);

        while(current != startNode)
        {
            path.Add(current);
            current = parent[(int)current.x, (int)current.y];
        }

        path.Add(startNode);

        path.Reverse();

        
        return path.ToArray();
    }


    private static int gCost(int[,] fCost, int[,] hCost, int x, int y)
    {
        return fCost[x, y] + hCost[x, y];
    }


    public static int Distance(Vector2 start, Vector2 end)
    {
        int dx = Mathf.RoundToInt(end.x) - Mathf.RoundToInt(start.x);
        int dy = Mathf.RoundToInt(end.y) - Mathf.RoundToInt(start.y);


        int angled = Mathf.Min(dx, dy);
        int straight = Mathf.Max(dx, dy) - angled;

        return angled * 14 + straight * 10;
    }


   

    private static void LogOnMain(string message)
    {
        if(System.Threading.Thread.CurrentThread.ManagedThreadId == 1)
        {
            Debug.Log(message);
        }
    }
    
}
