using UnityEngine;
using System.Collections;

public class Hiker_Animation : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey ("right"))
		   animator.SetInteger ("walk", 1);

		if(Input.GetKey ("v"))
		{
		animator.SetBool ("pressv", true);
			animator.SetBool ("pressc", false);
			animator.SetBool ("pressx", false);
			animator.SetBool ("pressz", false);

		}

		if(Input.GetKey ("c"))
		{
		animator.SetBool ("pressc", true);
			animator.SetBool ("pressv", false);
			animator.SetBool ("pressx", false);			//<switching between animations
			animator.SetBool ("pressz", false);
		}

		if(Input.GetKey ("x"))
		{
		animator.SetBool ("pressx", true);
			animator.SetBool ("pressc", false);
			animator.SetBool ("pressv", false);
			animator.SetBool ("pressz", false);
		}

		if(Input.GetKey ("z"))
		{
		animator.SetBool ("pressz", true);
			animator.SetBool ("pressc", false);
			animator.SetBool ("pressx", false);
			animator.SetBool ("pressv", false);
		}

	}
}
