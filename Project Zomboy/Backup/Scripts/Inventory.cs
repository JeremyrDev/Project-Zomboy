using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
    public List<string> items;      // string with item name
    public List<int> itemAmount;    // number of each item
    public List<int> itemSpace;     // how much space each item takes up
    public int numberOfItems = 0;
    public int spaceUsed = 0;
    public int spaceAvailable = 0;
    public int totalSpace = 100;

    void Awake()
    {
        items = new List<string>();
        itemAmount = new List<int>();
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

        updateStats();
    }
    public void removeItem(string item, int amount)
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
