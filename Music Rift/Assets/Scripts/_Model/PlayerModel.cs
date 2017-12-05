using UnityEngine;

public class PlayerModel : Element
{
    public int maxHealth;
    public float maxSpeed = 1f;
    public int health;
    public float jumpForce = 700f;
    public bool facingRight = true;
    public GameObject playerObject;
    public bool isGrounded;
    public bool collectingList = false;
}