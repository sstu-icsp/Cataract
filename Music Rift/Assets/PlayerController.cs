using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb;
    public int speed;
    bool isGrounded = true;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.transform.Translate(new Vector2(moveHorizontal, 0.0f) * Time.deltaTime * speed);

        if (isGrounded)
        {
            //if (Input.GetKey(KeyCode.Space))
                rb.AddForce(new Vector2(0, moveVertical) * 100);
        }
        //rb.AddForce(new Vector2(moveHorizontal, moveVertical) * speed);
    }

   void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
        Debug.Log("Enter");
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
        Debug.Log("Exit");
    }

   /* void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }*/
}
