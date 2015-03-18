using UnityEngine;
using System.Collections;

public class cameraControl : MonoBehaviour 
{
    public Transform player;
    Vector3 pos;
	void Start () 
    {
	
	}
	
	void Update () 
    {
        pos = new Vector3(player.position.x, player.position.y, -5);
        if (Input.GetMouseButton(1))
            pos.z = -25;
        transform.position = Vector3.Lerp(transform.position, pos, 10 * Time.deltaTime);
	}
}
