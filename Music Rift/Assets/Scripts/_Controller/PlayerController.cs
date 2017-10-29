using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CnControls;
using System;

public class PlayerController : Element
{
    private PlayerModel model;
    private PlayerView view;
    private float move;

    void Awake()
    {
        model = app.model.player;
        view = app.view.player;
        EventsController.Collision += OnCollision;
    }

    private void OnCollision(Element p_source, object[] p_data)
    {
        if (p_source == view)
        {
            Collision2D col = (Collision2D)p_data[0];
            switch (col.gameObject.tag)
            {
                case "Exit": Application.LoadLevel(Application.loadedLevel); break;
                case "Acid": Application.LoadLevel(Application.loadedLevel); break;
            }
        }
    }

    void FixedUpdate()
    {
        move = CnInputManager.GetAxis("Horizontal");

        if (CnInputManager.GetButton("Jump"))
        {
            model.grounded = Physics2D.Linecast(view.trS1.position, view.trE1.position, 1 << LayerMask.NameToLayer("Ground"))
           || Physics2D.Linecast(view.trS2.position, view.trE2.position, 1 << LayerMask.NameToLayer("Ground"));
            if (model.grounded)
            {
                view.rb.AddForce(new Vector2(0f, model.jumpForce));
                view.rb.velocity = new Vector2(0, 0);
            }
        }
        view.rb.velocity = new Vector2(move * model.maxSpeed, view.rb.velocity.y);

        if (move > 0 && !model.facingRight) Flip();
        else if (move < 0 && model.facingRight) Flip();
    }

    public void ChangeHealth(int val)
    {
        model.health = Mathf.Clamp(model.health + val, 0, model.maxHealth);
        app.view.ui.setHealth(model.health);
    }

    void Flip()
    {
        model.facingRight = !model.facingRight;
        Vector3 theScale = view.transform.localScale;
        theScale.x *= -1;
        view.transform.localScale = theScale;
    }

}
