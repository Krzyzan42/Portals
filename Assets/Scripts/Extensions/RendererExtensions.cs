using UnityEngine;

public static class RendererExtensions {
	public static bool IsVisibleFrom(this Renderer renderer, Camera camera) {
		UnityEngine.Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}
}