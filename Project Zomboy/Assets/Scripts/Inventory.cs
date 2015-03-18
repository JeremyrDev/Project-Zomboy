using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
    public List<string> items;      // string with item name
    public List<int> itemAmount;    // number of each item
    public List<int> itemSpace;     // how much space each item takes up
    public List<List<GameObject>> itemObjects;
    public int numberOfItems = 0;
    public int spaceUsed = 0;
    public int spaceAvailable = 0;
    public int totalSpace = 100;

    //public class Items
    //{

    //}
    void Awake()
    {
        items = new List<string>();
        itemAmount = new List<int>();
        itemSpace = new List<int>();
    }

    public void addItem(string item = "", int amount = 1, int space = 1)
    {
        int index = 0;
        foreach(string s in items)
        {
            if(s == item)
            {
                itemAmount[index] += amount;
                updateStats();
                return;
            }
            index++;
        }

        items.Add(item);
        itemAmount.Add(amount);
        itemSpace.Add(space);
        //List<GameObject> obj = new List<GameObject>();
        //itemObjects.Add(obj);
        updateStats();
    }

    public void removeItem(string item = "", int amount = 0)
    {
        int index = 0;
        foreach (string s in items)
        {
            if (s == item)
            {
                itemAmount[index] -= amount;
                updateStats();
                return;
            }
            index++;
        }
        print("Does not contain: " + item);
    }

    public GameObject placeObject(string s = "", Vector2 pos = default(Vector2), Quaternion rot = default(Quaternion), int layer = -2)
    {
        GameObject obj = (GameObject)Instantiate(Resources.Load(s));
        obj.name = s;
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.transform.parent = GameObject.Find(s).transform;
        obj.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, .25f);
        if(layer != -2)
            obj.GetComponent<SpriteRenderer>().sortingOrder = layer;
        return obj;
    }
    public void updateStats()
    {
        numberOfItems = items.Count;
        int index = 0;
        spaceUsed = 0;
        foreach (string s in items)
        {
            spaceUsed += itemSpace[index] * itemAmount[index];
            index++;
        }
        spaceAvailable = totalSpace - spaceUsed;
    }
    public int checkItem(string item)
    {
        int returnAmount = 0;
        int index = 0;
        foreach (string s in items)
        {
            if (s == item)
            {
                return itemAmount[index];
            }
            index++;
        }
        return returnAmount;
    }
}
