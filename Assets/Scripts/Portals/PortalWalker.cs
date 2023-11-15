using UnityEngine;

public abstract class PortalWalker : MonoBehaviour
{
	public Collider col;

	private Vector3 lastPos;
	private Collider enterPortalAttachedCol;
	private Collider exitPortalAttachedCol;

	protected virtual void FixedUpdate() {
		Update();
		UpdateCollisions();
	}

	protected virtual void Update() {
		Vector3 pos = transform.position;
		Portal enterPortal = PortalManager.instance.enterPortal;
		Portal exitPortal = PortalManager.instance.exitPortal;

		if (enterPortal && enterPortal.walkDetector.HasWalkedThrough(lastPos, pos)) {
			WalkedThrough(enterPortal);
		}
		if (exitPortal && exitPortal.walkDetector.HasWalkedThrough(lastPos, pos)) {
			WalkedThrough(exitPortal);
		}
		lastPos = transform.position;
	}
	
	protected virtual void WalkedThrough(Portal portal) {
		transform.position = portal.TransformPos(transform.position);
		transform.rotation = portal.TransformRot(transform.rotation);
	}

	protected virtual void Awake() {
		lastPos = transform.position;
	}

	public void UpdateCollisions() {
		Vector3 currentPosition = transform.position;
		Portal enterPortal = PortalManager.instance.enterPortal;
		Portal exitPortal = PortalManager.instance.exitPortal;
		Collider enterCollider = enterPortal ? enterPortal.surface.AttachedCollider : null;
		Collider exitCollider = exitPortal ? exitPortal.surface.AttachedCollider : null;

		if(enterPortalAttachedCol)
			Physics.IgnoreCollision(enterPortalAttachedCol, col, false);
		if(exitPortalAttachedCol)
			Physics.IgnoreCollision(exitPortalAttachedCol, col, false);

		if (enterCollider == exitCollider && enterCollider != null) {

			bool p1Front = enterPortal.walkDetector.IsInFront(currentPosition);
			bool p2Front = exitPortal.walkDetector.IsInFront(currentPosition);
			if (p1Front || p2Front)
				Physics.IgnoreCollision(enterCollider, col);
			else
				Physics.IgnoreCollision(enterCollider, col, false);
		} else {
			if (enterCollider)
				Physics.IgnoreCollision(col, enterCollider, enterPortal.walkDetector.IsInFront(currentPosition));
			if (exitCollider)
				Physics.IgnoreCollision(col, exitCollider, exitPortal.walkDetector.IsInFront(currentPosition));
		}
		enterPortalAttachedCol = enterCollider;
		exitPortalAttachedCol = exitCollider;
	}
}
