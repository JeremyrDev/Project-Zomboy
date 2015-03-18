using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generate : MonoBehaviour 
{
    public List<GameObject> islandList;
    public List<GameObject> grassList;
    public List<GameObject> sandList;
    public List<GameObject> waterList;
    public List<GameObject> treeList;

    public GameObject islands;
    public GameObject water;
    public GameObject grass;
    public GameObject sand;
    public GameObject tree;
    GameObject creationObject;
    GameObject islandParent;
    GameObject grassParent;
    GameObject sandParent;
    GameObject waterParent;
    GameObject treeParent;

    public int xMin = 6;
    public int xMax = 15;
    public int yMin = 6;
    public int yMax = 15;
    public int xBounds = 0;
    public int playerBoundsX = 0;
    public int playerBoundsY = 0;
    public int islandsToGenerate = 6;

    int sandCounter = 0;
    int grassCounter = 0;
    int waterCounter = 0;
    int treeCounter = 0;

    int increment = 1;
    int gradualGenCycle = 0;

    public bool gradGen = false;

	void Start () 
    {
        islandList = new List<GameObject>();
        grassList = new List<GameObject>();
        sandList = new List<GameObject>();
        waterList = new List<GameObject>();
        treeList = new List<GameObject>();

        for(int i = 0; i < 1; i++)
        {
            if(i == 0)
            {
                playerBoundsX = 0;
                playerBoundsY = 0;
            }
            if(i == 1)
            {
                playerBoundsX = 60;
                playerBoundsY = 60;
            }
            if (i == 2)
            {
                playerBoundsX = -60;
                playerBoundsY = 60;
            }
            if (i == 3)
            {
                playerBoundsX = -60;
                playerBoundsY = -60;
            }
            if (i == 4)
            {
                playerBoundsX = 60;
                playerBoundsY = -60;
            }
            if (i > 4)
            {
                playerBoundsX = 60 * (i - 2);
                playerBoundsY = -60;
            }
            generateLand();
        }
        
	}
	//create bounds for each island, name each island, maybe weather per island?
	void Update () 
    {
	    if(Input.GetKeyDown(KeyCode.Q))
        {
            clearObjects();
            playerBoundsX = 0;
            playerBoundsY = 0;
            generateLand();
        }
        //if (transform.position.x > playerBoundsX || transform.position.y > playerBoundsY)
        //{
        //    //playerBounds += 60;
        //    //generateLand();
        //}
        //if (transform.position.x < -playerBoundsX || transform.position.y < -playerBoundsY)
        //{

        //}
	}
    public void generateLand()
    {
        islandParent = new GameObject();
        islandParent.name = "Island"+islandList.Count;
        islandParent.transform.SetParent(islands.transform, true);
        islandList.Add(islandParent);
        

        grassParent = new GameObject();
        grassParent.name = "Grass";
        grassParent.transform.parent = islandParent.transform;
        grassParent.transform.position = new Vector2(playerBoundsX, playerBoundsY);

        sandParent = new GameObject();
        sandParent.name = "Sand";
        sandParent.transform.parent = islandParent.transform;
        sandParent.transform.position = new Vector2(playerBoundsX, playerBoundsY);

        waterParent = new GameObject();
        waterParent.name = "Water";
        waterParent.transform.parent = islandParent.transform;
        waterParent.transform.position = new Vector2(playerBoundsX, playerBoundsY);

        treeParent = new GameObject();
        treeParent.name = "Tree";
        treeParent.transform.parent = islandParent.transform;
        treeParent.transform.position = new Vector2(playerBoundsX, playerBoundsY);

        increment = Random.Range(1, 5);

        int maxXR = Random.Range(xMin, xMax);
        int maxXL = Random.Range(-xMin, -xMax);

        int maxYT = Random.Range(yMin, yMax);
        int maxYB = Random.Range(-yMin, -yMax);

        int xPeakPosition = Random.Range(-maxYT, maxYT);
        int yPeakPosition = Random.Range(-maxXR, maxXR);

        int l = 0;
        int r = 0;

        for(int y = maxYB; y < maxYT; y++)
        {

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
               // createObject(x,y,0);

               // fills top and bottom
                if (y == maxYB + 2 || y == maxYT - 3)
                    createObject(x, y, 1); //sand

                if (y == maxYB + 3 || y == maxYT - 4)
                    createObject(x, y, 1); //sand

                if (y <= maxYB + 1 || y >= maxYT - 2)
                    createObject(x, y, 2); //water

                if (y > maxYB + 1 && y < maxYT - 2)
                    createObject(x, y, 0); //grass


                if ((y > maxYB + 2 && y < maxYT - 3) || (y == maxYB + 2 || y == maxYT - 3))
                    if (Random.Range(0, 20) == 5)
                        createObject(x, y, 3); //tree

                ////fills sides
                //if (x == r - 1)
                //{
                //    if (y > maxYB + 2 && y < maxYT - 3)
                //        createObject(r, y, 1); // create sand
                //    else
                //        createObject(r, y, 2); // create water

                //if ((y > maxYB + 2 && y < maxYT - 3) || (y == maxYB + 2 || y == maxYT - 3))
                //    if(Random.Range(0,20) == 5)
                //        createObject(x, y, 3); //trees

                //fills sides
                if (x == r - 2)
                {
                    if (y > maxYB + 2 && y < maxYT - 3)
                        createObject(r, y, 1);
                    else
                        createObject(r, y, 2);

                   // --------------------------
                    for (int i2 = 1; i2 < 15; i2++)//add wave/shore/foam block when i2 == 4
                        if (i2 <= 6)
                            if (y > maxYB + 2 && y < maxYT - 3)
                                createObject(r + i2, y, 1);
                            else
                                createObject(r + i2, y, 2);
                        else
                            createObject(r + i2, y, 2);
                    //--------------------------
                }
                else if (x == l)
                {
                    if (y > maxYB + 2 && y < maxYT - 3)
                        createObject(l - 1, y, 1);
                    else
                        createObject(l - 1, y, 2);
                    //--------------------------

                    for (int i2 = 2; i2 < 15; i2++)//add wave/shore/foam block when i2 == 5
                        if (i2 <= 4)
                            if (y > maxYB + 2 && y < maxYT - 3)
                                createObject(l - i2, y, 1);
                            else
                                createObject(l - i2, y, 2);
                        else
                            createObject(l - i2, y, 2);
                }

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
        islandParent.transform.localPosition = new Vector2(playerBoundsX, playerBoundsY);
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
        obj.transform.position = new Vector2(x, y);

        if (type == 0)
        {
            obj.transform.parent = GameObject.Find(islandParent.name+"/"+grassParent.name).transform;
            grassList.Add(obj);
            grassCounter = grassList.Count;
        }
        if (type == 1)
        {
            obj.transform.parent = GameObject.Find(islandParent.name + "/" + sandParent.name).transform;
            sandList.Add(obj);
            sandCounter = sandList.Count;
        }
        if (type == 2)
        {
            obj.transform.parent = GameObject.Find(islandParent.name + "/" + waterParent.name).transform;
            waterList.Add(obj);
            waterCounter = waterList.Count;
        }
        if (type == 3)
        {
            obj.transform.parent = GameObject.Find(islandParent.name + "/" + treeParent.name).transform;
            treeList.Add(obj);
            treeCounter = treeList.Count;
        }
    }
    void clearObjects()
    {
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
