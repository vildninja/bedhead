using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SequenceStep : MonoBehaviour
{

    public readonly HashSet<BrainPiece> touching = new HashSet<BrainPiece>();

    public void Trigger()
    {
        foreach (var piece in touching)
        {
            piece.Trigger();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        var hit = col.rigidbody.GetComponent<BrainPiece>();
        if (hit)
        {
            touching.Add(hit);
        }
    }

    void OnCollisionExit(Collision col)
    {
        var hit = col.rigidbody.GetComponent<BrainPiece>();
        if (hit)
        {
            touching.Remove(hit);
        }
    }
}
