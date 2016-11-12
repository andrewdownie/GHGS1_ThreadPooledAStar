using UnityEngine;
using System.Collections.Generic;

public static class AStar
{

    public static void FindPath(object pathRequest)
    {
        System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();


        PathRequest pr = (PathRequest)pathRequest;


        AstarHeap openset = new AstarHeap();


        
        List<int2> closedSet = new List<int2>();


        int gridWidth = pr.grid.GetLength(0);
        int gridHeight = pr.grid.GetLength(1);


        int2[,] parent = new int2[gridWidth, gridHeight];
        int[,] hCost = new int[gridWidth, gridHeight];
        int[,] fCost = new int[gridWidth, gridHeight];
     

        int endX = (int)pr.endPos.x;
        int endY = (int)pr.endPos.y;

        int startX = (int)pr.startPos.x;
        int startY = (int)pr.startPos.y;
        openset.Add(startX, startY, 0, Distance(startX, startY, endX, endY));


        int curX = -1, curY = -1;

        
        while (true)
        {

            if (openset.Count == 0)
            {
                pr.mailbox.SafeAddResult(new PathResult(null, stopwatch.Elapsed.TotalMilliseconds));
                stopwatch.Stop();
                return;
            }


            openset.PopMinValue(out curX, out curY);
            closedSet.Add(new int2(curX, curY));



            if (curX == endX && curY == endY)
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
                     

                    int neighbourX = curX + offX;
                    int neighbourY = curY + offY;

                    if (offX == 0 && offY == 0)
                    {
                        continue;
                    }
                    if (curX + offX < 0 || curX + offX >= pr.grid.GetLength(0))
                    {
                        continue;
                    }
                    if (curY + offY < 0 || curY + offY >= pr.grid.GetLength(1))
                    {
                        continue;
                    }

                    
                    Vector2 neighbour = new Vector2(neighbourX, neighbourY);

                    if (pr.grid[neighbourX, neighbourY] == false || closedSet.Contains(new int2(neighbourX, neighbourY)))
                    {
                        continue;
                    }
                    hCost[neighbourX, neighbourY] = Distance(neighbourX, neighbourY, endX, endY);

                    int dist = Distance(curX, curY, neighbourX, neighbourY);
                    

                    if (openset.Contains(neighbourX, neighbourY) == false ||
                       gCost(fCost, hCost, curX, curY) + dist < gCost(fCost, hCost, neighbourX, neighbourY))
                    {
                        fCost[neighbourX, neighbourY] = fCost[curX, curY] + dist;
                        parent[neighbourX, neighbourY] = new int2(curX, curY);
                        

                        if (openset.Contains(neighbourX, neighbourY) == false)
                        {
                            openset.Add(neighbourX, neighbourY, fCost[neighbourX, neighbourY], hCost[neighbourX, neighbourY]);
                        }
                    }

                    
                    
                }
            }
        }



        PathResult result = new PathResult(RebuildPath(startX, startY, endX, endY, parent), stopwatch.Elapsed.TotalMilliseconds);
        pr.mailbox.SafeAddResult(result);
        stopwatch.Stop();
    }


    private static int2[] RebuildPath(int startX, int startY, int endX, int endY, int2[,] parent)
    {

        List<int2> path = new List<int2>();
        int2 current = new int2(endX, endY);

        while (!current.Equals(new int2(startX, startY)))
        {
            path.Add(current);
            current = parent[(int)current.x, (int)current.y];
        }

        path.Add(new int2(startX, startY));

        path.Reverse();


        return path.ToArray();
    }


    private static int gCost(int[,] fCost, int[,] hCost, int x, int y)
    {
        return fCost[x, y] + hCost[x, y];
    }


    public static int Distance(int2 start, int2 end)
    {
        int dx = Mathf.Abs(end.x - start.x);
        int dy = Mathf.Abs(end.y - start.y);
        

        if(dx < dy)
        {
            return dx * 14 + dy * 10;
        }

        return dy * 14 + dx * 10;
    }

    public static int Distance(int startX, int startY, int endX, int endY)
    {
        int dx = Mathf.Abs(endX - startX);
        int dy = Mathf.Abs(endY - startY);


        if (dx < dy)
        {
            return dx * 14 + dy * 10;
        }

        return dy * 14 + dx * 10;
    }




    /*private static void LogOnMain(string message)
    {
        if (System.Threading.Thread.CurrentThread.ManagedThreadId == 1)
        {
            Debug.Log(message);
        }
    }*/

}