using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
	public WalkDetector walkDetector;
	public Camera portalCamera;
	public MeshRenderer screen;
	public Transform clippingPlane;
	public MeshRenderer backFace;
	public int recursionLimit;
	public Vector2 size;

	public Camera mainCamera;
	public Portal connectedPortal;
	public Collider attatchedCollider;

	private RenderTexture texture;

	// Transform functions
	public Vector3 TransformPos(Vector3 pos) {
		Vector3 dist = pos - transform.position;
		Vector3 local = Quaternion.Inverse(transform.rotation) * dist;

		Vector3 rotated = connectedPortal.transform.rotation * local;
		return connectedPortal.transform.position + rotated;
	}

	public Quaternion TransformRot(Quaternion rot) {
		Quaternion localRot = Quaternion.Inverse(transform.rotation)*rot;
		return connectedPortal.transform.rotation * localRot;
	}

	public Vector3 TransformDir(Vector3 dir) {
		Vector3 localForward = transform.InverseTransformDirection(dir);
		return connectedPortal.transform.TransformDirection(localForward);
	}

	// Creates Texture, handles recursion, draws screen
	public void Render()
	{
		if(!IsTextureValid())
			CreateTexture();
		if (!screen.IsVisibleFrom(mainCamera))
			return;

		connectedPortal.backFace.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
		connectedPortal.screen.enabled = false;
		screen.material.SetTexture("_MainTex", Texture2D.blackTexture);


		List<Vector3> positions = GetCameraPositions(recursionLimit);
		List<Quaternion> rotations = GetCameraRotations(recursionLimit);

		int renderStart = 0;
		for (int i = 0; i < recursionLimit; i++)
		{
			portalCamera.transform.position = positions[i];
			portalCamera.transform.rotation = rotations[i];
			renderStart = recursionLimit - i - 1;
			if (!screen.IsVisibleFrom(portalCamera))
			{
				break;
			}

		}

		rotations.Reverse();
		positions.Reverse();

		for (int i = renderStart; i < recursionLimit; i++)
		{
			portalCamera.transform.position = positions[i];
			portalCamera.transform.rotation = rotations[i];
			SetClippingPlane();
			portalCamera.Render();

			if (i == renderStart)
				screen.material.SetTexture("_MainTex", texture);
		}
		connectedPortal.backFace.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
		connectedPortal.screen.enabled = true;
	}
	
	bool IsTextureValid() {
		if (texture == null || texture.width != Screen.width || texture.height != Screen.height)
			return false;
		else
			return true;
	}

	void CreateTexture() {
		if(texture != null)
			texture.Release();
		texture = new RenderTexture(Screen.width, Screen.height, 0);
		portalCamera.targetTexture = texture;
		screen.material.SetTexture("_MainTex", texture);
	}

	void SetClippingPlane() {
		Vector3 normal = connectedPortal.clippingPlane.forward;
		Vector3 point = connectedPortal.clippingPlane.position;
		Vector3 camSpaceNormal = portalCamera.transform.InverseTransfromDirectionNS(normal);
		Vector3 camSpacePos = portalCamera.transform.InverseTransformPointNS(point);

		float d = -Vector3.Dot(camSpacePos, camSpaceNormal);
		portalCamera.projectionMatrix = mainCamera.CalculateObliqueMatrix(new Vector4(camSpaceNormal.x, camSpaceNormal.y, -camSpaceNormal.z, d));
	}

	List<Vector3> GetCameraPositions(int count) {
		List<Vector3> positions = new List<Vector3>(count);
		Vector3 currentPos = mainCamera.transform.position;
		for (int i = 0; i < count; i++) {
			currentPos = TransformPos(currentPos);
			positions.Add(currentPos);
		}
		return positions;
	}

	List<Quaternion> GetCameraRotations(int count) {
		List<Quaternion> rotations = new List<Quaternion>(count);
		Quaternion currentRot = mainCamera.transform.rotation;
		for (int i = 0; i < count; i++) {
			currentRot = TransformRot(currentRot);
			rotations.Add(currentRot);
		}
		return rotations;
	}

	private void LateUpdate() {
		Render();
	}

}
