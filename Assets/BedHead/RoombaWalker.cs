using UnityEngine;
using System.Collections;

public class RoombaWalker : Walker
{
    public bool turnLeft;
    public LayerMask mask;

    public override void Tick()
    {
        base.Tick();

        walking = false;
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(current + new Vector3(0, 0.15f, 0), direction, 1f, mask))
            {
                direction = Vector3.Cross(direction, turnLeft ? Vector3.up : Vector3.down);
            }
            else
            {
                BrainController.Instance.Reserve(current + direction);
                walking = true;
                break;
            }
        }
    }
}
