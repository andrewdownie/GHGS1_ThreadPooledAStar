using UnityEngine;

[System.Serializable]
public class PathResult
{
    [SerializeField]
    double timeTaken;
    [SerializeField]
    Vector2[] path;
    [SerializeField]
    PathFinderCallback requester;

    public PathResult(Vector2[] path, PathFinderCallback requester, double timeTaken)
    {
        this.timeTaken = timeTaken;
        this.requester = requester;
        this.path = path;
    }
    

    public void CallbackPathResult()
    {
        requester.PathRequestResult(this);
    }
    
    public Vector2[] Path
    {
        get { return path; }
    }
    
    public double TimeTaken
    {
        get { return timeTaken; }
    }
}
