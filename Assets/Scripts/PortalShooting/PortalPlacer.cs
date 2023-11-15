using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Portals are destroyed and recreated when placed
// Takes care of initialazing and deinitialazing portals
public class PortalPlacer : MonoBehaviour {
	private PortalManager portals;
	private Portal enterPortal;
	private Portal exitPortal;

	public Portal enterPortalPrefab;
	public Portal exitPortalPrefab;
	public Camera main;
	public Vector2 portalSize;

	[Range(0, 1)]
	public float offset = 0.07f;
	
	private void Awake() {
		portals = PortalManager.instance;
		enterPortal = portals.enterPortal;
		exitPortal = portals.exitPortal;
	}

	public void PlaceExit(Vector3 pos, Quaternion rot, Collider collider) {
		if(exitPortal)
			Destroy(exitPortal.gameObject);
		pos += rot * Vector3.forward * offset;
		rot *= Quaternion.Euler(0, 180, 0);
		exitPortal = Instantiate(exitPortalPrefab, pos, rot);
		exitPortal.mainCamera = main;
		exitPortal.attatchedCollider = collider;
		exitPortal.connectedPortal = enterPortal;
		enterPortal.connectedPortal = exitPortal;

		portals.exitPortal = exitPortal;
		portals.UpdatePortals();
	}

	public void PlaceEnter(Vector3 pos, Quaternion rot, Collider collider) {
		if(enterPortal)
			Destroy(enterPortal.gameObject);
		pos += rot * Vector3.forward * offset;
		enterPortal = Instantiate(enterPortalPrefab, pos, rot);
		enterPortal.mainCamera = main;
		enterPortal.attatchedCollider = collider;
		enterPortal.connectedPortal = exitPortal;
		exitPortal.connectedPortal = enterPortal;

		portals.enterPortal = enterPortal;
		portals.UpdatePortals();
	}
}
