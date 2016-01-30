using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gnome : MonoBehaviour
{
    private Animator animator;

    public readonly Queue<string> instructions = new Queue<string>(); 

	// Use this for initialization
	void Start ()
	{
	    animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void Walk()
    {
        
    }
}
