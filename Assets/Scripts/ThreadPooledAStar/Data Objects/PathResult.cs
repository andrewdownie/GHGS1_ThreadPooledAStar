using UnityEngine;

[System.Serializable]
public class PathResult
{
    [SerializeField]
    double timeTaken;
    [SerializeField]
    int2[] path;

    public PathResult(int2[] path, double timeTaken)
    {
        this.timeTaken = timeTaken;
        this.path = path;
    }
    
    public int2[] Path
    {
        get { return path; }
    }
    
    public double TimeTaken
    {
        get { return timeTaken; }
    }
}
