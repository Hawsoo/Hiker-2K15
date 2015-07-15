using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public GameObject parentWrapper;

    public GameObject followObject;
    public float followSpeed;

	// Init
	void Awake()
    {
        parentWrapper.transform.position = followObject.transform.position;
	}
	
	// Update
	void FixedUpdate()
    {
	    // Lerp towards position
        parentWrapper.transform.position =
            Vector3.Lerp(parentWrapper.transform.position, followObject.transform.position, Time.deltaTime * followSpeed);

        // Face object
        transform.LookAt(followObject.transform.position);
	}
}
