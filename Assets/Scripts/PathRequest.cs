public struct PathRequest {

    public VInt2 startPos;
    public VInt2 endPos;
    public bool[,] grid;
    
   public PathRequest(bool[,] grid, VInt2 startPos, VInt2 endPos)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.grid = grid;
    }


}
