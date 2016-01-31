using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Walker : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody body;
    [HideInInspector]
    public Vector3 direction = Vector3.back;
    [HideInInspector]
    public Vector3 current;
    [HideInInspector]
    public bool walking;
    
    public virtual void Awake()
    {
        body = GetComponent<Rigidbody>();

        current = transform.position;
        current.x = Mathf.Round(current.x);
        current.y = 0;
        current.z = Mathf.Round(current.z);
    }



    public virtual void Tick()
    {
        current = transform.position;
        current.x = Mathf.Round(current.x);
        current.y = 0;
        current.z = Mathf.Round(current.z);
    }

    public virtual void Update()
    {
        transform.forward = Vector3.RotateTowards(transform.forward, direction, Time.deltaTime * 10, 1);
    }

    public virtual void FixedUpdate()
    {
        var target = current + direction * (walking ? BrainController.StepT + 0.1f : 0f);
        var vel = (target - transform.position) * 10 / BrainController.StepLength;
        body.velocity = vel;
    }
}
