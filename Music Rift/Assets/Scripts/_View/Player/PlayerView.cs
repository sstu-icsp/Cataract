using UnityEngine;

public class PlayerView : Element {

    public Rigidbody2D rb;
    public AudioClip attackSound; 
    public Transform trS1;
    public Transform trE1;
    public Transform trS2;
    public Transform trE2;
    private PlayerController controller;

    void Awake()
    {
        controller = app.controller.player;
    }
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        app.controller.events.OnCollision(this, col);
    }

    public void Die()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
