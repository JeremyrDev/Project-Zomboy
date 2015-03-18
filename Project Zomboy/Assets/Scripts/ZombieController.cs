using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZombieController : MonoBehaviour
{
    public GameObject player;
    public GameObject collisionObject;
    //make overall agression value that will determine everything
    public float speed = 2;
    public float normalSpeed = 2;
    public float engageDistance = 6;
    public int fidgetValue = 4;
    public float distance = 0;
    float updateTime = 1.2f;
    public int health = 100;
    public int attackDamage = 25;
    public int logs = 0;
    public bool dead = false;
    public bool enteredWater = false;
    public bool treeZone = false;
    public bool move = false;
    Vector3 endPos;
    Vector3 prevLoc;
    public Animator characterAnimator;
    Inventory inventory;
    void Awake()
    {
        player = GameObject.Find("Player");
        inventory = GetComponent<Inventory>();
        StartCoroutine("moveTowardsPlayer");
        health = Random.Range(50, 120);
    }
    void OnEnable()
    {
        StartCoroutine("moveTowardsPlayer");
        health = Random.Range(50, 120);
        engageDistance = Random.Range(6, 20);
        //inventory.addItem("Logs", 5, 2);
    }
    void Update()
    {
        if (dead)
            return;
        if (endPos != Vector3.zero)
        {
            if (transform.position == endPos)
            {
                move = false;
                characterAnimator.SetBool("Walking", false);
            }
            else
                transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
        }
    }
    IEnumerator moveTowardsPlayer()
    {
        while(true)
        {
            if (speed == 0)
                speed = normalSpeed;
            distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < engageDistance)
            {
                endPos = player.transform.position;
                characterAnimator.SetBool("Walking", true);
                characterAnimator.SetBool("Running", false);

                Vector3 curVel = (transform.position - prevLoc) / Time.deltaTime;
                if (curVel.x > 0)
                    transform.localScale = new Vector2(1, transform.localScale.y);
                else
                    transform.localScale = new Vector2(-1, transform.localScale.y);
                prevLoc = transform.position;
            }
            else
            {
                if(!move)
                {
                    endPos = Vector3.zero;
                    characterAnimator.SetBool("Walking", false);
                    characterAnimator.SetBool("Running", false);
                }

                if (Random.Range(-fidgetValue, fidgetValue) == 0 && !move)
                {
                    move = true;
                    if (Random.Range(0, 2) == 0)
                        endPos = new Vector2(transform.position.x + Random.Range(-2, 2), transform.position.y);
                    else
                        endPos = new Vector2(transform.position.x, transform.position.y + Random.Range(-2, 2));
                    characterAnimator.SetBool("Walking", true);
                }
                Vector3 curVel = (transform.position - prevLoc) / Time.deltaTime;
                if (curVel.x > 0)
                    transform.localScale = new Vector2(1, transform.localScale.y);
                else
                    transform.localScale = new Vector2(-1, transform.localScale.y);
                prevLoc = transform.position;
            }

            yield return new WaitForSeconds(updateTime);
            updateTime = Random.Range(.5f, 2f);

            if (dead)
                gameObject.SetActive(false);
            
            if (collisionObject != null)
                if(collisionObject.tag == "Player") // attack
                    collisionObject.GetComponent<PlayerController>().removeHealth(attackDamage);
                else if(collisionObject.tag == "Tree")// collect tree
                {
                    collisionObject.SetActive(false);
                    inventory.addItem("Logs", 2, 2);
                    collisionObject = null;
                }
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Water")
            enteredWater = true;
        
        if (c.tag == "Player")
            if (!dead)
                collisionObject = c.gameObject;
        if (c.tag == "Log")
            speed = 0;
    }
    void OnTriggerStay2D(Collider2D c)
    {
        if (c.tag == "Tree")
            collisionObject = c.gameObject;
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag == "Water")
            enteredWater = false;
        collisionObject = null;
    }

    public void removeHealth(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            health = 0;
            dead = true;
            StopAllCoroutines();
            characterAnimator.SetBool("Walking", false);
            characterAnimator.SetBool("Running", false);
        }
    }
}
