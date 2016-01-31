using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BrainPiece : MonoBehaviour
{
    [HideInInspector]
    public Text label;
    [HideInInspector]
    public Rigidbody body;

    public BrainPiece[] neighbours;

    // Use this for initialization
    void Awake ()
	{
	    label = GetComponentInChildren<Text>();
	    body = GetComponent<Rigidbody>();
	}

    public void Trigger(Vector3 from)
    {
        //body.AddForceAtPosition(Vector3.up * 10, from, ForceMode.Impulse);

        FindObjectOfType<SleepWalker>().instructions.Enqueue(label.text);
    }

    public void Select(Material material)
    {
        GetComponentInChildren<MeshRenderer>().sharedMaterial = material;
    }

    void OnDrawGizmos()
    {
        if (neighbours == null)
        {
            return;
        }

        Gizmos.color = Color.red;

        for (int i = 0; i < neighbours.Length; i++)
        {
            Gizmos.DrawLine(transform.position, Vector3.Lerp(transform.position, neighbours[i].transform.position, 0.5f));
        }
    }
}
