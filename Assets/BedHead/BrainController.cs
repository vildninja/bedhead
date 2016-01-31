using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class BrainController : MonoBehaviour
{
    public SequenceStep[] sequence;
    public Material inactive;
    public Material active;

    public float dragForce;

    public const float StepLength = 1.5f;
    public static float NextStep { get; private set; }
    public static float StepT { get { return (Time.time - (NextStep - StepLength)) / StepLength; } }

    private string[] actions = new[] {"Right", "Left", "Jump", ""};

    public LayerMask touchMask;

    private readonly List<BrainPiece> chain = new List<BrainPiece>();

    public List<ParticleSystem> connections;
    private int overheat = 0;

        // Use this for initialization
	IEnumerator Start ()
    {
        int index = 0;
	    Renderer last = null;
	    var walkers = FindObjectsOfType<Walker>();

	    for (int i = 1; i < 1; i++)
	    {
	        connections.Add(Instantiate(connections[0]));
	    }

	    var brainPieces = FindObjectsOfType<BrainPiece>();
	    for (int i = 0; i < brainPieces.Length; i++)
	    {
	        if (i%actions.Length == 0)
	        {
	            actions = actions.OrderBy(s => Guid.NewGuid()).ToArray();
            }
	        brainPieces[i].label.text = actions[i%actions.Length];
	        if (chain.Count == 0 && brainPieces[i].label.text == "")
	        {
	            chain.Add(brainPieces[i]);
	        }
	    }

	    NextStep = Time.time + StepLength;

	    while (true)
        {
            for (int i = 0; i < chain.Count; i++)
            {
                chain[i].Select((chain.Count - i) / (float)chain.Count);
            }

            yield return new WaitForSeconds(NextStep - Time.time);

            if (chain.Count > 1)
            {
                chain[0].Select(0);
                chain.RemoveAt(0);
                overheat = 0;

                chain[0].Trigger(Vector3.zero);
            }
            else
            {
                overheat++;
            }

	        NextStep += StepLength;

	        if (last)
	        {
                last.material = inactive;
	        }

            if (index >= sequence.Length)
            {
                index = 0;
            }

            sequence[index].Trigger();
            last = sequence[index].GetComponentInChildren<Renderer>();
            last.material = active;

	        for (int i = 0; i < walkers.Length; i++)
	        {
	            walkers[i].Tick();
	        }

	        index++;
	    }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetMouseButtonDown(0) && chain.Count < 2)
	    {
	        RaycastHit hit;
	        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, touchMask))
	        {
	            var selected = hit.collider.GetComponentInParent<BrainPiece>();
	            if (selected)
	            {
                    chain.Add(selected);
                }
	        }
	    }


        bool pointAtMouse = true;
        for (int i = 0; i < connections.Count; i++)
        {
            if (chain.Count <= i + 1)
            {
                if (pointAtMouse && chain.Count > i)
                {
                    pointAtMouse = false;

                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    var dif = chain[i].transform.position.z - ray.origin.z;
                    var mouse = ray.origin + ray.direction * dif;

                    connections[i].transform.position = chain[i].transform.position + new Vector3(0, 0, -0.4f);
                    connections[i].transform.LookAt(mouse + new Vector3(0, 0, -0.4f), Vector3.back);
                    connections[i].startLifetime = Vector3.Distance(chain[i].transform.position, mouse) / 5;
                    continue;
                }

                connections[i].transform.position = new Vector3(0, -1000, 0);
                continue;
            }

            connections[i].transform.position = chain[i].transform.position + new Vector3(0, 0, -0.4f);
            connections[i].transform.LookAt(chain[i + 1].transform.position + new Vector3(0, 0, -0.4f), Vector3.back);
            connections[i].startLifetime = Vector3.Distance(chain[i].transform.position, chain[i + 1].transform.position) / 5;
        }
    }
}
