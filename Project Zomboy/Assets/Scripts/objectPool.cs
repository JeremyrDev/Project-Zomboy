using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class objectPool : MonoBehaviour 
{
    public List<GameObject> objects;
    public GameObject o;
    public GameObject op;
    public int numberOfObjects = 100;
    public int objectsLimit = 150;
    public int counter = 0;
    public bool generateName = false;
    public bool createNew = false;
    bool opIsNull = false;
    generateNames gn;

    void Start()
    {
        gn = new generateNames();
        objects = new List<GameObject>();

        if (op == null)
        {
            GameObject parent = new GameObject();
            parent.name = gameObject.name;
            if (o.tag == "Zombies")
                parent.transform.parent = GameObject.Find("Zombies").transform;
            op = parent;
        }

        for (int i = 0; i < numberOfObjects; i++)
            createObject(i); 	
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            counter++;
        if (Input.GetKeyDown(KeyCode.Space))
            objects[counter].SetActive(true);
    }

    public void spawn(Vector2 p)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            if (!objects[i].activeInHierarchy)
            {
                objects[i].transform.position = p;
                objects[i].SetActive(true);
                return;						
            }

            if (createNew && (numberOfObjects < objectsLimit || objectsLimit == 0) && i == numberOfObjects - 1)
            {
                createObject(i++);
                numberOfObjects++;
            }
        }
    }

    void createObject(int i)
    {
        GameObject obj = (GameObject)Instantiate(o);
        obj.SetActive(false);

        if (generateName)
            obj.name = "Zombie "+gn.Generate();
        else
            obj.name = i.ToString();

        obj.transform.parent = op.transform;
        objects.Add(obj);
    }
}