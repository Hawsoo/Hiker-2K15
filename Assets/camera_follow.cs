using UnityEngine;
using System.Collections;

public class camera_follow : MonoBehaviour 
{
	public GameObject target = null;
	//public bool orbitY = false;  

	private Vector3 positionOffset = Vector3.zero;
	
	// Use this for initialization
	private void Start () 
	{
		positionOffset = transform.position - target.transform.position;
	}
	
	// Update is called once per frame
	private void Update () 
	{
		if (target != null)
		{
			//transform.LookAt (target.transform);

			//if (orbitY)
			//	transform.RotateAround(target.transform.position, Vector3.up, Time.deltaTime * 10);

			transform.position = target.transform.position + positionOffset;
		}
	}
}

// Lines 7,22,24,25 for orbiting camera