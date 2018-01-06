using UnityEngine;
using CnControls;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : Element
{
    private PlayerModel model;
    private PlayerView view;
    private float move;
    public bool isAndroid;
    private float jumpStartY;
    public float jumpHeight;
    public bool isWeaponPowerUp;
    private bool isWeaponized;

    void Awake()
    {
        model = app.model.player;
        view = app.view.player;
        EventsController.Collision += OnCollision;
        if (Application.platform == RuntimePlatform.Android)
        {
            isAndroid = true;
        }
        else
        {
            app.view.ui.disableJoystick();
        }
        GunView.OnGunChanged += GunChanged;
        GunController.OnPowerUp += GunPowerUp;
        WeaponPowerUpBehaviour.OnPowerUpFinished += GunPowerUpFinished;
        GunController.OnStopShooting += StopShooting;
    }
    void OnDestroy()
    {
        GunView.OnGunChanged -= GunChanged;
        GunController.OnPowerUp -= GunPowerUp;
        WeaponPowerUpBehaviour.OnPowerUpFinished -= GunPowerUpFinished;
        GunController.OnStopShooting -= StopShooting;
    }
    private void GunChanged(int mode)
    {
        if (mode == 0)
        {
            view.RemoveWeapon();
            isWeaponized = false;
        }
        else
        {
            view.TakeWeapon();
            isWeaponized = true;
        }
    }

    private void GunPowerUp()
    {
        if (isWeaponized)
        {
            isWeaponPowerUp = true;
            view.StartShooting();
        }
    }

    private void GunPowerUpFinished()
    {
        isWeaponPowerUp = false;
    }

    private void StopShooting()
    {
        if (isWeaponized)
        {
            view.StopShooting();
            isWeaponPowerUp = false;
        }
    }

    private void OnCollision(Element p_source, object[] p_data)
    {
        if (p_source == view)
        {
            Collision2D col = (Collision2D)p_data[0];
            switch (col.gameObject.tag)
            {
                case "Exit": app.controller.game.CongrateShow(); break;
                case "Acid": app.controller.game.ReloadLevel(); break;
                case "Ground": model.isGrounded = true; break;
            }
        }
    }

    void FixedUpdate()
    {
        if (!isWeaponPowerUp)
            UpdateMovement();
        if (model.health == 0)
            app.controller.game.ReloadLevel();
    }

    private void UpdateMovement()
    {
        move = CnInputManager.GetAxis("Horizontal");
        if (CnInputManager.GetButton("Jump"))
        {
            model.isGrounded = Physics2D.Linecast(view.trS1.position, view.trE1.position, 1 << LayerMask.NameToLayer("Ground"))
           || Physics2D.Linecast(view.trS2.position, view.trE2.position, 1 << LayerMask.NameToLayer("Ground"));
            if (model.isGrounded)
            {
                view.rb.AddForce(new Vector2(0f, model.jumpForce));
                view.rb.velocity = new Vector2(0, 0);
                model.isGrounded = false;
                jumpStartY = view.transform.position.y;
            }
        }

        view.rb.velocity = new Vector2(move * model.maxSpeed, view.rb.velocity.y);
        jumpHeight = view.transform.position.y - jumpStartY;
        if (move > 0 && !model.facingRight) Flip();
        else if (move < 0 && model.facingRight) Flip();
    }

    public void ChangeHealth(int val)
    {
        model.health = Mathf.Clamp(model.health + val, 0, model.maxHealth);
        app.view.ui.setHealth(model.health);
        // app.view.amplirude.h = model.health;
    }

    public void Flip()
    {
        model.facingRight = !model.facingRight;
        Vector3 theScale = view.transform.localScale;
        theScale.x *= -1;
        view.transform.localScale = theScale;
    }

}
