public class PathResult
{
    VInt2[] path;

    public PathResult(VInt2[] path)
    {
        this.path = path;
    }

    public VInt2 this[int index]
    {
        get { return path[index]; }
    }

}
