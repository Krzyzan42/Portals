using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLerp : MonoBehaviour {
	public float targetZ;
	public float targetX;
	public float speed;
	
	// Update is called once per frame
	void Update() {
		Vector3 rot = transform.rotation.eulerAngles;
		float zDiff =  Mathf.DeltaAngle(rot.z, targetZ);
		rot.z += zDiff * speed;
		//float xDiff = Mathf.DeltaAngle(rot.x, targetX);
		//rot.x += xDiff * speed;
		transform.rotation = Quaternion.Euler(rot);
	}

	
}
