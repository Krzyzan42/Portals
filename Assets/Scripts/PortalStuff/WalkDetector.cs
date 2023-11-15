using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane
{
	public Vector3 pos;
	public Vector3 normal;

	public bool InFrontOf(Vector3 position) {
		Vector3 dir = position - pos;
		return Vector3.Dot(normal, dir) > 0;
	}
}

public class WalkDetector : MonoBehaviour {
	private Plane up;
	private Plane down;
	private Plane right;
	private Plane left;
	private Plane middle;

	public Vector2 size;
	public bool walkIn;

	void Awake() {
		RecalculatePlanes();
	}
	public void SetSize(float width, float height) {
		size = new Vector2(width, height);
		RecalculatePlanes();
	}

	private void RecalculatePlanes() {
		up = ConstructPlane(Vector3.up, size.y);
		down = ConstructPlane(Vector3.down, size.y);
		right = ConstructPlane(Vector3.right, size.x);
		left = ConstructPlane(Vector3.left, size.x);
		middle = new Plane();
		middle.normal = transform.TransformDirection(-Vector3.forward);
		middle.pos = transform.position;
	}

	private Plane ConstructPlane(Vector3 dir, float size) {
		Vector3 pos = transform.TransformPoint(dir * size / 2);
		Vector3 normal = transform.TransformDirection(-dir);

		Plane plane = new Plane();
		plane.normal = normal;
		plane.pos = pos;
		return plane;
	}

	public bool HasWalkedThrough(Vector3 lastPos, Vector3 pos) {
		bool front = middle.InFrontOf(lastPos);
		bool nextFront = middle.InFrontOf(pos);

		if (front != nextFront) {
			if (!up.InFrontOf(lastPos))
				return false;
			if (!down.InFrontOf(lastPos))
				return false;
			if (!right.InFrontOf(lastPos))
				return false;
			if (!left.InFrontOf(lastPos))
				return false;
			if (!up.InFrontOf(pos))
				return false;
			if (!down.InFrontOf(pos))
				return false;
			if (!right.InFrontOf(pos))
				return false;
			if (!left.InFrontOf(pos))
				return false;
			return true;
		}
		return false;
	}

	public bool isInFront(Vector3 pos) {
		bool front = middle.InFrontOf(pos);

		if (front) {
			if (!up.InFrontOf(pos))
				return false;
			if (!down.InFrontOf(pos))
				return false;
			if (!right.InFrontOf(pos))
				return false;
			if (!left.InFrontOf(pos))
				return false;
			return true;
		}
		return false;
	}
}
