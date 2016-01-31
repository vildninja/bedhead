using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SleepWalker : Walker
{
    private Animator animator;

    private readonly Queue<string> instructions = new Queue<string>();
    
    public bool IsJumping { get; private set; }

    public AnimationCurve jumpCurve;

    private LayerMask houseOnly;
    private LayerMask everything;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        houseOnly = LayerMask.GetMask("House");
        everything = LayerMask.GetMask("House", "Default");

        Tick();
    }

    public void Instruct(string instruction)
    {
        instructions.Enqueue(instruction);
        if (instruction == "Jump")
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    private int coffeeTicks = 0;

    public override void Tick()
    {
        base.Tick();

        var coffee = GameObject.Find("CoffeeMachine");

        if (IsJumping)
        {
            IsJumping = false;

            var overlaps = Physics.OverlapSphere(transform.position + new Vector3(0, 0.5f, 0), 0.4f);
            for (int i = 0; i < overlaps.Length; i++)
            {
                if (overlaps[i].transform.root != transform)
                {
                    IsJumping = true;
                    animator.SetTrigger("Jump");
                    walking = false;
                    return;
                }
            }

            GetComponent<Collider>().enabled = true;
        }


        if (Vector3.Distance(coffee.transform.position, current) < 0.1f)
        {
            direction = coffee.transform.forward;
            animator.SetTrigger("Coffee");
            walking = false;
            coffeeTicks++;

            if (coffeeTicks > 4)
            {
                SceneManager.LoadScene(0);
            }

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
            if (Physics.Raycast(current + new Vector3(0, 0.5f, 0), direction, 1f, IsJumping ? houseOnly : everything))
            {
                direction = -direction;
            }
            else if (!IsJumping)
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
