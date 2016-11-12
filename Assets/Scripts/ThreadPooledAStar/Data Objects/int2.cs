public struct int2
{
    public int x;
    public int y;

    public int2(int _x, int _y)
    {
        x = _x;
        y = _y;


    }
    public int fCost
    {
        get { return x; }
    }

    public int hCost
    {
        get { return y; }
    }
}
