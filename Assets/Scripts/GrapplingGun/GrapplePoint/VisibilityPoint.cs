using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to check if grapple point is visible from camera
// Has collider for raycast
[RequireComponent(typeof(Collider))]
public class VisibilityPoint : MonoBehaviour
{
	[NonSerialized]
	public new Collider collider;

	public Vector3 Position => transform.position;


	private void Awake() {
		collider = GetComponent<Collider>();
	}
}
