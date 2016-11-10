using UnityEngine;

[System.Serializable]
public class PathResult
{
    [SerializeField]
    Vector2[] path;
    [SerializeField]
    PathFinderCallback requester;
    [SerializeField]
    long timeTaken;

    public PathResult(Vector2[] path, PathFinderCallback requester, long timeTaken)
    {
        this.requester = requester;
        this.timeTaken = timeTaken;
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


    public long TimeTakenMili
    {
        get { return timeTaken; }
    }

    public float TimeTaken
    {
        get { return (float)timeTaken / 1000f; }
    }

}
