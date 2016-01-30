using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BrainPiece : MonoBehaviour
{

    private Text label;
    [HideInInspector]
    public Rigidbody body;

	// Use this for initialization
	void Start ()
	{
	    label = GetComponentInChildren<Text>();
	    body = GetComponent<Rigidbody>();
	}

    public void Trigger(Vector3 from)
    {
        body.AddForceAtPosition(Vector3.up * 10, from, ForceMode.Impulse);
    }
}
