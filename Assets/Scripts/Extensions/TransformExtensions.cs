using UnityEngine;

public static class TransformExtensions {
	public static Vector3 InverseTransformPointNS(this Transform t, Vector3 vector) {
		Vector3 dist = vector-t.position;
		return Quaternion.Inverse(t.rotation) * dist;
	}

	public static Vector3 InverseTransfromDirectionNS(this Transform t, Vector3 vector) {
		return Quaternion.Inverse(t.rotation) * vector;
	}
}

