using UnityEngine;
using System.Collections;

public class TestHeap : MonoBehaviour {
    public bool testCosts;
    public bool testContains;

    public int randomMin = 1;
    public int randomMax = 30;
    
	void Update () {
        if (testCosts == true)
        {
            testCosts = false;

            TestCosts();
        }


        if (testContains == true)
        {
            testContains = false;

            TestContains();
        }


    }

    public void TestContains()
    {
        AstarHeap iHeap = new AstarHeap();

        int2 tst = new int2(1, 1);
        int2 cmp = new int2(1, 1);
        int2 cmp2 = new int2(0, 0);

        iHeap.Add(tst, tst);

        Debug.Log("CONTAINS:" + tst.ToString() + " = " + iHeap.Contains(cmp));
        Debug.Log("DOES NOT CONTAIN:" + tst.ToString() + " = " + iHeap.Contains(cmp2));
    }

    public void TestCosts()
    {
        AstarHeap iHeap = new AstarHeap();


        for (int i = 0; i < 20; i++)
        {
            //int rndInt = Random.Range(0, 100);
            iHeap.Add(new int2(random(), random()), new int2(random(), random()));
        }


        int x, y, fCost, hCost;

        for (int i = 0; i < 20; i++)
        {
            iHeap.PopMinValue(out x, out y, out fCost, out hCost);
            Debug.Log("fCost: " + fCost + ", hCost: " + hCost + "\t\t(" + x + "," + y + ")");
        }
    }

    public int random()
    {
        return Random.Range(randomMin, randomMax + 1);
    }
}
