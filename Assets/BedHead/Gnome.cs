using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gnome : MonoBehaviour
{
    private Animator animator;

    public readonly Queue<string> instructions = new Queue<string>();

    private Vector3 direction;
    private Vector3 current;
    private bool walking;
    private bool drinking;
    private bool turning;
    private float jump;
    public bool IsJumping { get { return Time.time < jump; } }

    public void Tick()
    {
        current = transform.position;
        current.x = Mathf.Round(current.x);
        current.y = 0;
        current.z = Mathf.Round(current.z);

        while (instructions.Count > 0)
        {
            var action = instructions.Dequeue();
            switch (action)
            {
                case "Walk":
                    walking = true;
                    break;
                case "Stop":
                    walking = false;
                    break;
                case "Left":
                    direction = Vector3.Cross(direction, Vector3.down);
                    break;
                case "Right":
                    direction = Vector3.Cross(direction, Vector3.up);
                    break;
                case "Drink":
                    drinking = true;
                    break;
                case "Jump":
                    jump = Time.time + 1.1f;
                    animator.SetTrigger("Jump");
                    break;
            }
        }
    }

    void Update()
    {
        transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(direction, transform.forward) > 0.2f)
        {
            turning = true;
        }


        animator.SetBool("Walking", walking || turning);
    }

    public void Walk()
    {
        
    }
}
