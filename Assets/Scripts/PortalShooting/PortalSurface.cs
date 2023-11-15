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
	public Vector2 initialSize;
	[Range(0f, 0.2f)]
	public float Offset = 0.05f;
	public Collider AttachedCollider;

	private Vector2 size;
	private const float colliderThickness = 0.05f;

	private void Awake() {
		size = initialSize;
		BoxCollider collider = GetComponent<BoxCollider>();
		if(!collider) {
			collider = gameObject.AddComponent<BoxCollider>();
		}
		collider.size = new Vector3(initialSize.x, initialSize.y, colliderThickness);
		collider.isTrigger = true;
	}

    public BoxSurfaceResult GetPortalPosit(Vector3 hitWorld, Vector2 portalSize) {
		Vector3 hitLocal = transform.InverseTransformPoint(hitWorld);
		portalSize /= new Vector2(transform.lossyScale.x, transform.lossyScale.y);
		float x = Mathf.Clamp(hitLocal.x, -(size.x - portalSize.x)/2, (size.x - portalSize.x)/2);
		float y = Mathf.Clamp(hitLocal.y, -(size.y - portalSize.y)/2, (size.y - portalSize.y)/2);
		float z = hitLocal.z > 0 ? Offset : -Offset;
		Vector3 portalPos = transform.TransformPoint(new Vector3(x, y, z));
		Quaternion portalOrientation;
        if (hitLocal.z > 0)
            portalOrientation = Quaternion.LookRotation(transform.forward, Vector3.up);
        else
            portalOrientation = Quaternion.LookRotation(-transform.forward, Vector3.up);

        return new BoxSurfaceResult(portalPos, portalOrientation);
	}
}
