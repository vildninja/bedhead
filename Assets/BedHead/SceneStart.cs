using UnityEngine;
using System.Collections;
using System.IO;

public class SceneStart : MonoBehaviour
{

    public bool showIntro = true;

    public GameObject brain;

    public Transform start;
    public Transform end;

    public float releaseBrainZ = 8;

	// Use this for initialization
	IEnumerator Start ()
	{
	    if (!showIntro)
	    {
	        transform.position = end.position;
	        transform.rotation = end.rotation;
	        var pos = brain.transform.position;
	        pos.z = releaseBrainZ;
	        brain.transform.position = pos;

	        foreach (var piece in brain.GetComponentsInChildren<Rigidbody>())
	        {
	            piece.isKinematic = false;
	        }
	        yield break;
	    }

	    for (float t = 0; t < 2; t += Time.deltaTime)
	    {
	        yield return null;
	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
