using System.Collections.Generic;
using UnityEngine;

public struct PathRequest {

    public PathResultMailBox mailbox;
    public Vector2 startPos;
    public Vector2 endPos;
    public bool[,] grid;
    
   public PathRequest(bool[,] grid, Vector2 startPos, Vector2 endPos, PathResultMailBox mailbox)
    {
        this.mailbox = mailbox;
        this.startPos = startPos;
        this.endPos = endPos;
        this.grid = grid;
    }

    
}
