using UnityEngine;
using CnControls;
using UnityEngine.SceneManagement;

public class PlayerController : Element
{
    private PlayerModel model;
    private PlayerView view;
    private float move;
    private bool isAndroid;

    void Awake()
    {
        model = app.model.player;
        view = app.view.player;
        EventsController.Collision += OnCollision;
        if (Application.platform == RuntimePlatform.Android)
        {
            isAndroid = true;
            app.view.ui.disableJoystick();
        }

    }

    private void OnCollision(Element p_source, object[] p_data)
    {
        if (p_source == view)
        {
            Collision2D col = (Collision2D)p_data[0];
            switch (col.gameObject.tag)
            {
                case "Exit": app.controller.game.ReloadLevel(); break;
                case "Acid": app.controller.game.ReloadLevel(); break;
                case "Ground": model.isGrounded = true; break;
            }
        }
    }

    void FixedUpdate()
    {
        if (isAndroid)
        {
            if ((Input.acceleration.x > 0.2 || Input.acceleration.x < -0.2) && !app.controller.fight.IsFighting)
            {
                move = Input.acceleration.x * 2f;
            }
            else
            {
                move = 0;
            }
        }
        else
        {
            move = CnInputManager.GetAxis("Horizontal");
        }

        if (CnInputManager.GetButton("Jump"))
        {
            //model.isGrounded = Physics2D.Linecast(view.trS1.position, view.trE1.position, 1 << LayerMask.NameToLayer("Ground"))
           //|| Physics2D.Linecast(view.trS2.position, view.trE2.position, 1 << LayerMask.NameToLayer("Ground"));
            if (model.isGrounded)
            {
                view.rb.AddForce(new Vector2(0f, model.jumpForce));
                view.rb.velocity = new Vector2(0, 0);
                model.isGrounded = false;
            }
        }

        view.rb.velocity = new Vector2(move * model.maxSpeed, view.rb.velocity.y);
        if (move > 0 && !model.facingRight) Flip();
        else if (move < 0 && model.facingRight) Flip();

        if (model.health == 0)
            app.controller.game.ReloadLevel();

    }

    public void ChangeHealth(int val)
    {
        model.health = Mathf.Clamp(model.health + val, 0, model.maxHealth);
        app.view.ui.setHealth(model.health);
    }

    public void Flip()
    {
        model.facingRight = !model.facingRight;
        Vector3 theScale = view.transform.localScale;
        theScale.x *= -1;
        view.transform.localScale = theScale;
    }

}
