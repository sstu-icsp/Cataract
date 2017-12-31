using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoScript : Element {

    bool isFlight = false;
    bool moveToRight = false;
    public float speed = 0.01f;

    void Start()
    {
        isFlight = true;
        moveToRight = transform.position.x < app.view.player.gameObject.transform.position.x ? true : false;
    }
    void FixedUpdate()
    {
        if(isFlight)
        Flight();
    }
    void Flight()
    {
        float temp = moveToRight ? speed : -speed;        
        transform.position = new Vector3(transform.position.x + temp, transform.position.y, transform.position.z);
        
        if (!moveToRight)
        transform.rotation = Quaternion.Euler(
         new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + 180));
    }

   /* void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag != "Enemy")
        {
            isFlight = false;
            Destroy(gameObject);
        }
    }*/
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Enemy")
        {
            isFlight = false;
            Destroy(gameObject);
        }
        if(col.tag == "Player")
        {
            app.controller.player.ChangeHealth(-5);
        }
    }
}
