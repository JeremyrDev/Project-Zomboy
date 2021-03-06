﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
    public GameObject inventoryUI;
    public GameObject collisionObject;
    float fpsTimer = 0;
    float speed = 2;
    public int health = 100;
    public int attackDamage = 50;
    public int moveDirection = 0;
    int fps = 0;
    public bool enteredWater = false;
    public bool treeZone = false;
    public bool dead = false;
    public Text inventoryText;
    public Text fpsText;
    public Text healthText;
    public Animator characterAnimator;
    public Inventory inventory;

    //add fatigue, can only sprint for so long
	void Update () 
    {
        if(fpsTimer >= 1)
        {
            fpsText.text = "FPS: " + fps;
            fpsTimer = 0;
            fps = 0;
        }
        else
        {
            fpsTimer += 1 * Time.deltaTime;
            fps++;
        }

        if(!dead)
        {
            Controls();
        }
	}
    void Controls()
    {
        if (collisionObject != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (collisionObject.tag == "Tree")
                {
                    collisionObject.SetActive(false);
                    inventory.addItem("Logs", 2, 2);
                }
                if (collisionObject.tag == "Zombie")
                {
                    collisionObject.GetComponent<ZombieController>().removeHealth(attackDamage);
                }
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                inventoryText.text = collisionObject.name+" Inventory: space used ~ "+collisionObject.GetComponent<Inventory>().spaceUsed+"\n";
                int index = 0;
                foreach (string s in collisionObject.GetComponent<Inventory>().items)
                {
                    inventoryText.text += s + ": " + collisionObject.GetComponent<Inventory>().itemAmount[index] + "\n";
                    index++;
                }
                inventoryUI.SetActive(true);
            }
            if (Input.GetKeyUp(KeyCode.E))
                inventoryUI.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (inventory.checkItem("Logs") < 2)
                return;
            if(moveDirection == 0)
            {
                //place behind
            }
            if (moveDirection == 1)
            {
                //place in front
            }
            if (moveDirection == 2)
            {
                //place to the right
            }
            if (moveDirection == 3)
            {
                //place to the left
            }
            inventory.removeItem("Logs", 2);
        }
        if(Input.GetKey(KeyCode.Tab))
        {
            //separate method, load all inventory values into gui
            //Inventory.text = "Logs: " + logs;
            int index = 0;
            inventoryText.text = "Player Inventory: \n";
            if (inventory.items.Count > 0)
                foreach (string s in inventory.items)
                {
                    inventoryText.text += s + ": " + inventory.itemAmount[index] + "\n";
                    index++;
                }
            else
                inventoryText.text += " -- Nothing -- ";
            inventoryUI.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            inventoryUI.SetActive(false);
        }
        if (Input.GetKey(KeyCode.LeftShift))
            speed = 5;
        else
            speed = 2;
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection = 0;
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            if (speed == 5)
            {
                characterAnimator.SetBool("Walking", false);
                characterAnimator.SetBool("Running", true);
            }
            else
            {
                characterAnimator.SetBool("Walking", true);
                characterAnimator.SetBool("Running", false);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection = 1;
            transform.Translate(-Vector2.up * speed * Time.deltaTime);
            if (speed == 5)
            {
                characterAnimator.SetBool("Walking", false);
                characterAnimator.SetBool("Running", true);
            }
            else
            {
                characterAnimator.SetBool("Walking", true);
                characterAnimator.SetBool("Running", false);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection = 2;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.localScale = new Vector2(1, transform.localScale.y);
            if (speed == 5)
            {
                characterAnimator.SetBool("Walking", false);
                characterAnimator.SetBool("Running", true);
            }
            else
            {
                characterAnimator.SetBool("Walking", true);
                characterAnimator.SetBool("Running", false);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection = 3;
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
            transform.localScale = new Vector2(-1, transform.localScale.y);
            if (speed == 5)
            {
                characterAnimator.SetBool("Walking", false);
                characterAnimator.SetBool("Running", true);
            }
            else
            {
                characterAnimator.SetBool("Walking", true);
                characterAnimator.SetBool("Running", false);
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            characterAnimator.SetBool("Walking", false);
            characterAnimator.SetBool("Running", false);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            characterAnimator.SetBool("Walking", false);
            characterAnimator.SetBool("Running", false);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            characterAnimator.SetBool("Walking", false);
            characterAnimator.SetBool("Running", false);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            characterAnimator.SetBool("Walking", false);
            characterAnimator.SetBool("Running", false);
        }
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Water")
            enteredWater = true;
        if (c.tag == "Tree")
            collisionObject = c.gameObject;
        if (c.tag == "Zombie")
            collisionObject = c.gameObject;
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag == "Water")
            enteredWater = false;
        if (c.tag == "Tree")
            collisionObject = null;
        if (c.tag == "Zombie")
            collisionObject = null;
    }
    public void removeHealth(int amount)
    {
        health -= amount;
        healthText.text = "Health: " + ((health / 100) * 100) + "%";
        if(health <= 0)
        {
            health = 0;
            dead = true;
        }
    }
}
