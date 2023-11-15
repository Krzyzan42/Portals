using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
	private bool lockedMouse = true;

	[HideInInspector]
	public bool forward = false;
	[HideInInspector]
	public bool backward = false;
	[HideInInspector]
	public bool left = false;
	[HideInInspector]
	public bool right = false;
	[HideInInspector]
	public bool jump = false;

	[HideInInspector]
	public float horMouse;
	[HideInInspector]
	public float verMouse;

	public float rotSpeed = 10f;

	private void Start() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update() {


		forward = Input.GetKey(KeyCode.W);
		backward = Input.GetKey(KeyCode.S);
		left = Input.GetKey(KeyCode.A);
		right = Input.GetKey(KeyCode.D);
		jump = Input.GetKey(KeyCode.Space);

		if (lockedMouse) {
			horMouse = Input.GetAxis("Mouse X") * rotSpeed;
			verMouse = -Input.GetAxis("Mouse Y") * rotSpeed;
		}
		else {
			horMouse = 0;
			verMouse = 0;
		}

		if (Input.GetKeyDown(KeyCode.F)) {
			lockedMouse = !lockedMouse;
			if (lockedMouse) {
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			else {
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

	}

	public Vector3 GetMoveVector() {
		Vector3 dir = Vector3.zero;
		if (forward)
			dir += Vector3.forward;
		if (backward)
			dir += Vector3.back;
		if (right)
			dir += Vector3.right;
		if (left)
			dir += Vector3.left;
		return dir.normalized;
	}
}
