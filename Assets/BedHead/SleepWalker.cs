using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SleepWalker : Walker
{
    private Animator animator;

    public readonly Queue<string> instructions = new Queue<string>();
    
    public bool IsJumping { get; private set; }

    public AnimationCurve jumpCurve;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        Tick();
    }

    public override void Tick()
    {
        base.Tick();

        var coffee = GameObject.Find("CoffeeMachine");

        IsJumping = false;
        if (Vector3.Distance(coffee.transform.position, current) < 0.1f)
        {
            direction = coffee.transform.forward;
            animator.SetTrigger("Coffee");
            walking = false;
            return;
        }

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
                    direction = Vector3.Cross(direction, Vector3.up);
                    break;
                case "Right":
                    direction = Vector3.Cross(direction, Vector3.down);
                    break;
                case "Jump":
                    IsJumping = true;
                    animator.SetTrigger("Jump");
                    break;
            }
        }

        walking = false;
        for (int i = 0; i < 2; i++)
        {
            if (Physics.Raycast(current + new Vector3(0, 0.5f, 0), direction, 1f))
            {
                direction = -direction;
            }
            else
            {
                BrainController.Instance.Reserve(current + direction);
                walking = true;
                break;
            }
        }

        animator.SetBool("Walking", walking);
    }

    public override void Update()
    {
        base.Update();
        if (IsJumping)
        {
            var pos = transform.position;
            pos.y = jumpCurve.Evaluate(BrainController.StepT);
            transform.position = pos;
        }
    }
}
