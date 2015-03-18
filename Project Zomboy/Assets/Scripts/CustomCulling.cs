using UnityEngine;
using System.Collections;

public class CustomCulling : MonoBehaviour 
{
    void OnBecameVisible()
    {
        enabled = true;
    }
    void OnBecameInvisible()
    {
        enabled = false;
    }
}
