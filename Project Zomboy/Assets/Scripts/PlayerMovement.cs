using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
    float speed = 2;
    public Animator characterAnimator;
    public bool enteredWater = false;
	void Start () 
    {
	    
	}
	void Update () 
    {
        Controls();
	}
    void Controls()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            speed = 5;
        else
            speed = 2;
        if (Input.GetKey(KeyCode.W))
        {
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
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag == "Water")
            enteredWater = false;
    }
}
