using UnityEngine;
using System.Collections;

public class SelectScript : MonoBehaviour 
{
    void OnMouseDown()
    {
        print("Selected Existing Log");
        GameObject.Find("Player").GetComponent<PlayerController>().selectedObject = null;
        GameObject.Find("Player").GetComponent<PlayerController>().selectedObject = gameObject;
        GameObject.Find("Player").GetComponent<PlayerController>().existing = true;
        GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, .25f);
    }
}
