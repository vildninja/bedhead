using UnityEngine;
using System.Collections;

public class BrainController : MonoBehaviour
{
    public SequenceStep[] sequence;
    private int index;

	// Use this for initialization
	IEnumerator Start ()
    {
	    while (true)
	    {
	        yield return new WaitForSeconds(0.5f);
            sequence[index].Trigger();
	    }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
