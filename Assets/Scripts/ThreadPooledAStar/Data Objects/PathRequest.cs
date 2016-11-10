using System.Collections.Generic;
using UnityEngine;

public struct PathRequest {

    public PathFinderCallback requester;
    public Vector2 startPos;
    public Vector2 endPos;
    public bool[,] grid;
    
   public PathRequest(bool[,] grid, Vector2 startPos, Vector2 endPos, PathFinderCallback requester)
    {
        this.requester = requester;
        this.startPos = startPos;
        this.endPos = endPos;
        this.grid = grid;
    }

    
}
