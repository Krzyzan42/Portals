using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHolder : MonoBehaviour {
	private Vector3 currentVel;

	public Transform gun;
	public Vector3 targetPosition;
	public Vector3 targetAngle;

	public float smoothTime;
	public float rotSpeed = 0.1f;

	void Start() {

	}
	
	void FixedUpdate() {
		Vector3 position = transform.TransformPoint(targetPosition);

		gun.position = Vector3.Lerp(gun.position, position, rotSpeed);

		Quaternion targetRot = transform.rotation* Quaternion.Euler(targetAngle);
		gun.rotation = Quaternion.Lerp(gun.rotation, targetRot, rotSpeed);
	}
}
