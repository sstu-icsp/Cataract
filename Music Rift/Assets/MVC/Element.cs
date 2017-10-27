using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all elements in application.
public class Element : MonoBehaviour
{

    // Gives access to the application and all instances.
    public App app { get { return GameObject.FindObjectOfType<App>(); } }

}
