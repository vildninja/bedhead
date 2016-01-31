using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BrainPiece : MonoBehaviour
{
    [HideInInspector]
    public Text label;
    [HideInInspector]
    public Rigidbody body;

    private ParticleSystem input;
    private ParticleSystem output;

    // Use this for initialization
    void Start ()
	{
	    label = GetComponentInChildren<Text>();
	    body = GetComponent<Rigidbody>();
        input = GetComponentInChildren<ParticleSystem>();
	}

    public void Trigger(Vector3 from)
    {
        //body.AddForceAtPosition(Vector3.up * 10, from, ForceMode.Impulse);

        FindObjectOfType<SleepWalker>().instructions.Enqueue(label.text);
    }

    public void Select(float power)
    {
        input.emissionRate = power*40;
    }
}
