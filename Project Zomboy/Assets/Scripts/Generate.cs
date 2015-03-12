using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generate : MonoBehaviour 
{
    public List<GameObject> grassList;
    public List<GameObject> sandList;
    public List<GameObject> waterList;
    public List<GameObject> treeList;
    GameObject creationObject;
    public GameObject water;
    public GameObject grass;
    public GameObject sand;
    public GameObject tree;
    public int xMin = 6;
    public int xMax = 15;
    public int yMin = 6;
    public int yMax = 15;
    int sandCounter = 0;
    int grassCounter = 0;
    int waterCounter = 0;
    int treeCounter = 0;
    int grassX = 0;
    int grassY = 0;
    int sandX = 0;
    int sandY = 0;
    int waterX = 0;
    int waterY = 0;

    int lastIncrememt = 0   ;
    int increment = 1;
    bool incrementTooHigh = false;
    bool incrementTooLow = false;

	void Start () 
    {
        grassList = new List<GameObject>();
        sandList = new List<GameObject>();
        waterList = new List<GameObject>();
        treeList = new List<GameObject>();
	}
	
	void Update () 
    {
	    if(Input.GetKeyDown(KeyCode.E))
        {
            clearObjects();
            generateLand();
        }
	}

    public void generateLand()
    {
        int maxXRight = Random.Range(xMin, xMax);
        int maxXLeft = Random.Range(xMin, xMax);
        int maxXR = Random.Range(xMin, xMax);
        int maxXL = Random.Range(-xMin, -xMax);
        int maxX = Random.Range(xMin, xMax);

        int maxYRight = Random.Range(yMin, yMax);
        int maxYLeft = Random.Range(yMin, yMax);
        int maxYT = Random.Range(yMin, yMax);
        int maxYB = Random.Range(-yMin, -yMax);
        int maxY = Random.Range(yMin, yMax);

        int xPeakPosition = Random.Range(-maxYT, maxYT);
        int yPeakPosition = Random.Range(-maxXR, maxXR);

        int l = 0;
        int r = 0;

        for(int y = maxYB; y < maxYT; y++)
        {
            Debug.Log(increment);
            if (increment > 2)
                incrementTooHigh = true;

            else
                incrementTooHigh = false;
   
            if (increment < -2)
                incrementTooLow = true;

            else
                incrementTooLow = false;

            
            if (!incrementTooHigh && !incrementTooLow)
                increment = Random.Range(lastIncrememt - 1, lastIncrememt + 1);

            else if(incrementTooHigh)
                increment = Random.Range(lastIncrememt - 1, lastIncrememt);

            else if (incrementTooLow)
                increment = Random.Range(lastIncrememt, lastIncrememt + 1);

            lastIncrememt = increment;

            if(y < 0)
            {
                l = Random.Range(((xPeakPosition - y) + maxXL) - increment, ((xPeakPosition - y) + maxXL) + increment);
                r = Random.Range(((xPeakPosition + y) + maxXR) - increment, ((xPeakPosition + y) + maxXR) + increment);
            }
            else
            {
                l = Random.Range(((xPeakPosition + y) + maxXL) - increment, ((xPeakPosition + y) + maxXL) + increment);
                r = Random.Range(((xPeakPosition - y) + maxXR) - increment, ((xPeakPosition - y) + maxXR) + increment);
            }
            for(int x = l; x < r; x++)
            {
                createObject(x,y,0);
                //fills top and bottom
                //if (y == maxYB + 2 || y == maxYT - 3)
                //    createObject(x, y, 1); //sand

                //if (y == maxYB + 3 || y == maxYT - 4)
                //    createObject(x, y, 1); //sand

                //if (y <= maxYB + 1 || y >= maxYT - 2)
                //    createObject(x, y, 2); //water

                //if (y > maxYB + 1 && y < maxYT - 2)
                //    createObject(x, y, 0); //grass

                //if ((y > maxYB + 2 && y < maxYT - 3) || (y == maxYB + 2 || y == maxYT - 3))
                //    if(Random.Range(0,20) == 5)
                //        createObject(x, y, 3); //trees

                //fills sides
                //if (x == r - 2)
                //{
                //    if (y > maxYB + 2 && y < maxYT - 3)
                //        createObject(r, y, 1);
                //    else
                //        createObject(r, y, 2);
                    //--------------------------
                    //for (int i2 = 1; i2 < 15; i2++)//add wave/shore/foam block when i2 == 4
                    //    if (i2 <= 6)
                    //        if (y > maxYB + 2 && y < maxYT - 3)
                    //            createObject(r + i2, y, 1);
                    //        else
                    //            createObject(r + i2, y, 2);
                    //    else
                    //        createObject(r + i2, y, 2);
                    //--------------------------
                //}
                //else if (x == l)
                //{
                //    if (y > maxYB + 2 && y < maxYT - 3)
                //        createObject(l - 1, y, 1);
                //    else
                //        createObject(l - 1, y, 2);
                    //--------------------------
                    //for (int i2 = 2; i2 < 15; i2++)//add wave/shore/foam block when i2 == 5
                    //    if(i2 <= 6)
                    //        if (y > maxYB + 2 && y < maxYT - 3)
                    //            createObject(l - i2, y, 1);
                    //        else
                    //            createObject(l - i2, y, 2);
                    //    else
                    //        createObject(l - i2, y, 2);
                //}
            }
        }

        print
        (
            "MaxXL: " + maxXL + ", " +
            "MaxXR: " + maxXR + ", " +
            "XPeak: " + xPeakPosition + " : " +
            "MaxYB: " + maxYB + ", " +
            "MaxYT: " + maxYT + ", " +
            "YPeak: " + yPeakPosition +" : "+
            "Grass: "+grassCounter+", "+
            "Sand: "+sandCounter+", "+
            "Water: "+waterCounter+", "+
            "Trees: "+treeCounter
        );
    }

    void createObject(int x, int y, int type)
    {
        if(type == 0)
            creationObject = grass;
        if(type == 1)
            creationObject = sand;
        if(type == 2)
            creationObject = water;
        if(type == 3)
            creationObject = tree;

        GameObject obj = (GameObject)Instantiate(creationObject);
        obj.name = x.ToString();
        obj.transform.parent = GameObject.Find(creationObject.name).transform;
        obj.transform.position = new Vector2(x, y);

        if (type == 0)
        {
            grassList.Add(obj);
            grassCounter = grassList.Count;
        }
        if (type == 1)
        {
            sandList.Add(obj);
            sandCounter = sandList.Count;
        }
        if (type == 2)
        {
            waterList.Add(obj);
            waterCounter = waterList.Count;
        }
        if (type == 3)
        {
            treeList.Add(obj);
            treeCounter = treeList.Count;
        }
    }
    void clearObjects()
    {
        lastIncrememt = 2;
        foreach (GameObject g in grassList)
            Destroy(g);
        grassList.RemoveRange(0, grassList.Count);
        foreach (GameObject g in sandList)
            Destroy(g);
        sandList.RemoveRange(0, sandList.Count);
        foreach (GameObject g in waterList)
            Destroy(g);
        waterList.RemoveRange(0, waterList.Count);
        foreach (GameObject g in treeList)
            Destroy(g);
        treeList.RemoveRange(0, treeList.Count);
    }
}
