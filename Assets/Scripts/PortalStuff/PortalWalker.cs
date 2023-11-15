using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PortalWalker : MonoBehaviour
{
	public Collider col;

	private CollisionUpdater collisionUpdater;
	private Vector3 lastPos;

	private PortalManager portalManager;
	private Portal enterPortal;
	private Portal exitPortal;



	protected virtual void FixedUpdate() {
		UpdatePos();
		collisionUpdater.UpdateCollisions(transform.position);
	}

	private void Update() {
		UpdatePos();
	}
	private void UpdatePos() {
		Vector3 pos = transform.position;
		if (enterPortal.walkDetector.HasWalkedThrough(lastPos, pos)) {
			WalkedThrough(enterPortal);
		}
		if (exitPortal.walkDetector.HasWalkedThrough(lastPos, pos)) {
			WalkedThrough(exitPortal);
		}
		lastPos = transform.position;
	}

	protected virtual void WalkedThrough(Portal portal) {
		transform.position = portal.TransformPos(transform.position);
		transform.rotation = portal.TransformRot(transform.rotation);
	}

	private void PortalChanged() {
		collisionUpdater.UpdateCollisions(transform.position, true);
		enterPortal = portalManager.enterPortal;
		exitPortal = portalManager.exitPortal;
	}

	protected virtual void Awake() {
		collisionUpdater = new CollisionUpdater(col);
		lastPos = transform.position;
		portalManager = PortalManager.instance;

		enterPortal = portalManager.enterPortal;
		exitPortal = portalManager.exitPortal;
		portalManager.portalPlacementChanged.AddListener(PortalChanged);
	}
}

class CollisionUpdater
{
	public Collider col;

	private Collider enterPortalAttachedCol;
	private Collider exitPortalAttachedCol;

	private readonly PortalManager portalManager;
	private Portal enterPortal;
	private Portal exitPortal;

	public CollisionUpdater(Collider collider) {
		col = collider;
		portalManager = PortalManager.instance;
		enterPortal = portalManager.enterPortal;
		exitPortal = portalManager.exitPortal;
	}

	public void UpdateCollisions(Vector3 currentPosition, bool portalPlacementChanged = false) {
		if(portalPlacementChanged) {
			// enable collision with previous portals surfaces
			if(enterPortalAttachedCol)
				Physics.IgnoreCollision(enterPortalAttachedCol, col, false);
			if(exitPortalAttachedCol)
				Physics.IgnoreCollision(exitPortalAttachedCol, col, false);

			enterPortal = portalManager.enterPortal;
			exitPortal = portalManager.exitPortal;
		}

		if (enterPortal.attatchedCollider == exitPortal.attatchedCollider && enterPortal.attatchedCollider != null) {

			bool p1Front = enterPortal.walkDetector.isInFront(currentPosition);
			bool p2Front = exitPortal.walkDetector.isInFront(currentPosition);
			if (p1Front || p2Front)
				Physics.IgnoreCollision(enterPortal.attatchedCollider, col);
			else
				Physics.IgnoreCollision(enterPortal.attatchedCollider, col, false);
		} else {
			if (enterPortal.attatchedCollider)
				Physics.IgnoreCollision(col, enterPortal.attatchedCollider, enterPortal.walkDetector.isInFront(currentPosition));
			if (exitPortal.attatchedCollider)
				Physics.IgnoreCollision(col, exitPortal.attatchedCollider, exitPortal.walkDetector.isInFront(currentPosition));
		}
		enterPortalAttachedCol = enterPortal.attatchedCollider;
		exitPortalAttachedCol = exitPortal.attatchedCollider;

	}
}