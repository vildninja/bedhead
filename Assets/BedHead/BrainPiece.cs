using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BrainPiece : MonoBehaviour
{

    private Text label;
    private Rigidbody body;

	// Use this for initialization
	void Start ()
	{
	    label = GetComponentInChildren<Text>();
	    body = GetComponent<Rigidbody>();
	}

    public void Trigger()
    {
        body.AddForce(Vector3.up * 20, ForceMode.Impulse);
    }
}
