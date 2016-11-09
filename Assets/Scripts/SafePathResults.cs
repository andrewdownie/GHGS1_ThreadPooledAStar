using System.Collections.Generic;

public class SafePathResults {

    private List<PathResult> pathResults;

    public SafePathResults()
    {
        pathResults = new List<PathResult>();
    }

    public void AddResult(PathResult result)
    {
        lock (pathResults)
        {
            pathResults.Add(result);
        }
    }

    public PathResult PopResult()
    {
        lock (pathResults)
        {
            if(pathResults.Count > 0)
            {
                PathResult rp = pathResults[0];
                pathResults.RemoveAt(0);
                return rp;
            }
        }

        return null;
    }
    
}
