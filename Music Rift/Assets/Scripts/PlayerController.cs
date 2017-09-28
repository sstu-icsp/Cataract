using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float jumpForce = 700f;
    bool facingRight = true;
    public bool grounded;
    Rigidbody2D rb;
    public float move;

    public Transform trS1;
    public Transform trE1;
    public Transform trS2;
    public Transform trE2;
    [SerializeField]
    GameObject fightPanel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        move = Input.GetAxis("Horizontal");
        grounded = Physics2D.Linecast(trS1.position, trE1.position, 1 << LayerMask.NameToLayer("Ground")) 
            || Physics2D.Linecast(trS2.position, trE2.position, 1 << LayerMask.NameToLayer("Ground"));
        //Debug.DrawLine(trS1.position, trE1.position, Color.green);
        //Debug.DrawLine(trS2.position, trE2.position, Color.green);
       // Debug.DrawLine(transform.position, new Vector2(transform.position.x + 1, transform.position.y + 0.5f), Color.green);
      //  Debug.DrawLine(transform.position, new Vector2(transform.position.x - 1, transform.position.y - 0.5f), Color.green);
        if (wallCollision())
        {
            move = 0;
        }
    }

    void Update()
    {
        if (grounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.AddForce(new Vector2(0f, 700));
        }
        rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    bool wallCollision()
    {
        return Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(transform.position.x + 1, transform.position.y + 0.5f), 1 << LayerMask.NameToLayer("Ground")) && move > 0
            || Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(transform.position.x - 1, transform.position.y + 0.5f), 1 << LayerMask.NameToLayer("Ground")) && move < 0
            || Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y - 0.5f), new Vector2(transform.position.x + 1, transform.position.y - 0.5f), 1 << LayerMask.NameToLayer("Ground")) && move > 0
            || Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y - 0.5f), new Vector2(transform.position.x - 1, transform.position.y - 0.5f), 1 << LayerMask.NameToLayer("Ground")) && move < 0;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("col");
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("colsad");
            fightPanel.GetComponent<FightPanel>().Fight(col);
        }
        if (col.gameObject.tag == "Exit")
        {
            Application.LoadLevel(Application.loadedLevel);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
        }
        if (col.gameObject.tag == "Rift")
        {
            Debug.Log("colsad");
            fightPanel.GetComponent<FightPanel>().Fight(col);
        }
    }
}
