using UnityEngine;
using System.Collections;

public class RoombaWalker : Walker
{
    public bool turnLeft;

    public override void Tick()
    {
        base.Tick();

        if (Physics.Raycast(current + new Vector3(0, 0.15f, 0), direction, 1f))
        {
            direction = Vector3.Cross(direction, turnLeft ? Vector3.up : Vector3.down);
        }
    }
}
