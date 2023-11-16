using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
	public Rope rope;
	public Rigidbody itself;
	public Highlight higlight;


	public float connectDistFactor = 0.1f;


	private bool isGrappling = false;
	private bool pullRope = false;


	void LateUpdate() { // to wait for portal updates
		if(!isGrappling)
			HighlightBestGrapple();

		if (UserInput.GrappleKeyDown) {
			TryConnectGrapple();
		}

		if (UserInput.GrappleKeyUp && isGrappling) {
			ForceDisconnect();
		}

		if (isGrappling && UserInput.GrapplePullPressed) {
			pullRope = true;
		} else
			pullRope = false;
	}

	void HighlightBestGrapple() {
		Grappable g = GetBestGrapplePoint();
		if (g != null)
			higlight.Enable(g.grapplePoint.position);
		else
			higlight.Disable();
	}

	Grappable GetBestGrapplePoint() {
		float minDotValue = 0.7f;

		SortedList<float, Grappable> grappableSorted = new SortedList<float, Grappable>(new DuplicateDescKeyComparer<float>());
		Ray ray = Camera.main.ViewportPointToRay(Vector3.one / 2);

		foreach (Grappable grappable in GrapplePointList.Instance.grapplePoints) {
			float angleFromCursor = Vector3.Dot(ray.direction.normalized, (grappable.HookTarget - ray.origin).normalized);
			float dist = (grappable.HookTarget - ray.origin).magnitude;
			float grappleScore = angleFromCursor + Mathf.Clamp(1f / dist, 0, 1f / 5f) * connectDistFactor;
			if (angleFromCursor > minDotValue)
				grappableSorted.Add(grappleScore, grappable);
		}

		foreach (var kvp in grappableSorted) {
			Grappable gp = kvp.Value;
			if (gp.CanBeSeen(Camera.main.transform.position))
				return gp;
		}
		return null;
	}

	void TryConnectGrapple() {
		Grappable grappable = GetBestGrapplePoint();
		if (grappable == null)
			return;
		else if (grappable.type == GrappleType.GrapplePoint) {
			ConnectToPoint(grappable);
		}
	}

	void ConnectToPoint(Grappable grappable) {
		Rigidbody grapplePoint = grappable.grapplePoint;
		rope.Connect(grapplePoint, grapplePoint.transform.position, itself, grappable.IsPortal);
		isGrappling = true;
		higlight.Disable();
	}

	public void ForceDisconnect() {
		if (isGrappling) {
			rope.Disconnect();
			isGrappling = false;
		}
	}

	private void FixedUpdate() {
		if (pullRope)
			rope.Pull();
	}

}
