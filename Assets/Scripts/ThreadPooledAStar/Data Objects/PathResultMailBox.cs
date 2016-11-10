using UnityEngine;

public class PathResultMailBox {
    [SerializeField]
    private PathResult result;

    public void SafeAddResult(PathResult result)
    {
        lock(result){
            this.result = result;
        }
        
    }

    public PathResult SafeGetResult()
    {
        lock (result)
        {
            PathResult r = result;
            result = null;
            return r;
        }
    }

    public bool UnsafeHasResult()
    {
        return result != null;
    }


	
}
