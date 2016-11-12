using System.Collections.Generic;

class AstarHeap
{
    List<int2> xy;
    List<int2> costs;
    

    public bool Contains(int2 node)
    {
        return xy.Contains(node);
    }

    public bool Contains(int x, int y)
    {
        return xy.Contains(new int2(x, y));
    }

    public AstarHeap()
    {
        xy = new List<int2>();
        costs = new List<int2>();
    }

    public int Count
    {
        get
        {
            return xy.Count;
        }
    }

    public void Add(int2 xy, int2 hCost_FCost)
    {
        this.xy.Add(xy);
        this.costs.Add(hCost_FCost);
        Heapify();
    }

    public void Add(int x, int y, int fCost, int hCost)
    {
        this.xy.Add(new int2(x, y));
        this.costs.Add(new int2(fCost, hCost));
        Heapify();
    }

    public void Delete()
    {
        int last = xy.Count - 1;

        xy[0] = xy[last];
        xy.RemoveAt(last);

        costs[0] = costs[last];
        costs.RemoveAt(last);
        Heapify();
    }

    public void PopMinValue(out int x, out int y)
    {
        if (xy.Count > 0)
        {
            x = xy[0].x;
            y = xy[0].y;
            Delete();
        }
        else
        {
            x = -1;
            y = -1;
        }
        
    }

    public void PopMinValue(out int x, out int y, out int fCost, out int hCost)
    {
        if (this.xy.Count > 0)
        {
            x = xy[0].fCost;
            y = xy[0].hCost;
            
            hCost = costs[0].hCost;
            fCost = costs[0].fCost;

            Delete();
        }
        else
        {
            x = -1;
            y = -1;
            fCost = -1;
            hCost = -1;
        }
       
    }

    public void Heapify()
    {
        for (int i = xy.Count - 1; i > 0; i--)
        {
            int parentPosition = (i + 1) / 2 - 1;
            parentPosition = parentPosition >= 0 ? parentPosition : 0;

            if (ParentGreater(parentPosition, i))
            {
                int2 tmpKey = xy[parentPosition];
                xy[parentPosition] = xy[i];
                xy[i] = tmpKey;

                int2 tmpVal = costs[parentPosition];
                costs[parentPosition] = costs[i];
                costs[i] = tmpVal;
            }
        }
    }

    private bool ParentGreater(int parentPosition, int childPosition)
    {
        int2 parentCosts = costs[parentPosition];
        int2 childCosts = costs[childPosition];


        if (parentCosts.fCost > childCosts.fCost)
        {
            return true;
        }
        else if(parentCosts.fCost == childCosts.fCost)
        {
            if(parentCosts.hCost > childCosts.hCost)
            {
                return true;
            }
        }


        return false;
    }
}