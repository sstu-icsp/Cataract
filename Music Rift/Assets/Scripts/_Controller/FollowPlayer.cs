using System.Collections;

using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    void Start()
    {
        offset = new Vector3(0, 0, transform.position.z - player.transform.position.z);
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
