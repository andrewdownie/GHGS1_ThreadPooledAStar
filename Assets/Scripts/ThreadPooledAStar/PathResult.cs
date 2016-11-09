using UnityEngine;

public class PathResult
{
    Vector2[] path;

    long timeTaken;

    public PathResult(Vector2[] path, long timeTaken)
    {
        this.timeTaken = timeTaken;
        this.path = path;
    }

    
    public Vector2[] Path
    {
        get { return path; }
    }


    public long TimeTakenMili
    {
        get { return timeTaken; }
    }

    public float TimeTaken
    {
        get { return (float)timeTaken / 1000f; }
    }

}
