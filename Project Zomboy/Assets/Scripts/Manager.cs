using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour 
{
    public GameObject player;
    public objectPool op;
    public float spawnTimer = 0;
    float spawnLimit = 1;
    float counter = 10;
    //day night cycle, more zombies at night
	void Start () 
    {

	}
	
	void Update () 
    {
        if (spawnTimer > spawnLimit)
        {
            spawnLimit = Random.Range(counter + 1.5f, counter + 5);
            spawnTimer = 0;
            Vector2 pos = new Vector2(Random.Range(player.transform.position.x+10, player.transform.position.x-10), Random.Range(player.transform.position.y+10, player.transform.position.y-10));
            op.spawn(pos);
            if(counter > 1)
                counter-=.5f;
        }
        else
            spawnTimer += 1 * Time.deltaTime;
	}
}
