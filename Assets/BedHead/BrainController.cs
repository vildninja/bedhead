using UnityEngine;
using System.Collections;

public class BrainController : MonoBehaviour
{
    public SequenceStep[] sequence;
    public Material inactive;
    public Material active;

    private BrainPiece selected;
    private Vector3 local;

    public float dragForce;

	// Use this for initialization
	IEnumerator Start ()
    {
        int index = 0;
	    Renderer last = null;

	    while (true)
	    {
	        yield return new WaitForSeconds(0.5f);

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

	        index++;
	    }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetMouseButtonDown(0))
	    {
	        RaycastHit hit;
	        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
	        {
	            selected = hit.collider.GetComponentInParent<BrainPiece>();
	            if (selected)
	            {
	                local = selected.transform.InverseTransformPoint(hit.point);
                    selected.body.drag = 5f;
                    selected.body.angularDrag = 5f;
                }
	        }
	    }
	    if (Input.GetMouseButtonUp(0))
	    {
	        if (selected)
            {
                selected.body.drag = 0.2f;
                selected.body.angularDrag = 0.2f;
            }
	        selected = null;
	    }
	}

    void FixedUpdate()
    {
        if (selected == null)
        {
            return;
        }

        var current = selected.transform.TransformPoint(local);
        current.z = transform.position.z;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float dif = transform.position.z - ray.origin.z;

        var target = ray.origin + ray.direction*dif;

        var force = (target - current);

        if (force.magnitude > 1)
        {
            force.Normalize();
        }

        selected.body.AddForceAtPosition(force * dragForce, current);
    }
}
