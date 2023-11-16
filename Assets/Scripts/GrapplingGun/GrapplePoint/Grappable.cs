using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrappleType
{
	Portal,
	GrapplePoint
}

// Represents physical object(point) that player can grab onto
public class Grappable : MonoBehaviour
{

	public Vector3 HookTarget => transform.position;

	public GrappleType type;

	// Points that can bee seen from main camera to be able to grapple things
	// Not really needed
	public List<VisibilityPoint> visibilityPoints = new List<VisibilityPoint>();

	// Rigidbody that player can connect to
	// Can be made into Transform, since grapple implementation has it's own physics
	public Rigidbody grapplePoint;
	public bool IsPortal;


	public void Awake() {
		GrapplePointList.Instance.grapplePoints.Add(this);
	}

	public bool CanBeSeen(Vector3 fromPosition) {
		if (visibilityPoints.Count == 0)
			throw new System.InvalidOperationException();

		foreach (VisibilityPoint gpPoint in visibilityPoints) {
			Ray ray = new Ray(fromPosition, gpPoint.Position - fromPosition);
			if (Physics.Raycast(ray, out RaycastHit info)) {
				if (info.collider != gpPoint.collider)
					return false;
			} else
				Debug.LogWarning("Raycast failed?");
		}
		return true;
	}

	public void OnDestroy() {
		GrapplePointList.Instance.grapplePoints.Remove(this);
	}
}

