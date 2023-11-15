using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoxSurfaceResult
{
	public Vector3 position;
	public Quaternion rotation;

	public BoxSurfaceResult(Vector3 pos, Quaternion rot) {
		position = pos;
		rotation = rot;
	}
}
public class PortalSurface : MonoBehaviour
{
	private Quaternion ForwardOrientation => Quaternion.LookRotation(transform.forward, Vector3.up);
	private Quaternion BackwardOrientation => Quaternion.LookRotation(-transform.forward, Vector3.up);

	public Vector2 initialSize;
	private const float colliderThickness = 0.05f;
	[Range(0f, 0.2f)]
	public float Offset = 0.05f;

	private Vector2 size;


	public Vector2 Size {
		get => size;
		set {
			size = value;
			GetComponent<BoxCollider>().size = new Vector3(size.x, size.y, colliderThickness);
		}
	}
	public Collider AttachedCollider;

    public BoxSurfaceResult GetPortalPosit(Vector3 hitWorld, Vector2 portalSize) {
		Vector3 hitLocal = transform.InverseTransformPoint(hitWorld);
		float hitX = hitLocal.x, hitY = hitLocal.y;
		float x = Mathf.Clamp(hitX, (-(size.x - portalSize.x))/2, (size.x - portalSize.x)/2);
		float y = Mathf.Clamp(hitY, (-(size.y - portalSize.y)/2), (size.y - portalSize.y)/2);
		float z = hitLocal.z > 0 ? Offset : -Offset;
		Vector3 portalPos = transform.TransformPoint(new Vector3(x, y, z));
		Quaternion portalOrientation = hitLocal.z > 0 ? ForwardOrientation : BackwardOrientation;

		return new BoxSurfaceResult(portalPos, portalOrientation);
	}

	private void Awake() {
		size = initialSize;
		BoxCollider collider = GetComponent<BoxCollider>();
		if(!collider) {
			collider = gameObject.AddComponent<BoxCollider>();
		}
		collider.size = new Vector3(initialSize.x, initialSize.y, colliderThickness);
		collider.isTrigger = true;
	}
}
