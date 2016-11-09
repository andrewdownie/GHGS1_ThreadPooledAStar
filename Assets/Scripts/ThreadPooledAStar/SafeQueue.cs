using System.Collections.Generic;

public class SafeQueue<t> {
    private Queue<t> queue;

    public SafeQueue()
    {
        queue = new Queue<t>();
    }


    public bool UnsafeIsEmpty
    {
        get { return queue.Count == 0; }
    }


    public void SafeEnque(t item)
    {
        lock (queue)
        {
            queue.Enqueue(item);
        }
    }

    public t SafeDequeue()
    {
        lock (queue)
        {
            if (queue.Count > 0)
            {
                return queue.Dequeue();
            }
        }

        return default(t);
    }
    
	
}
