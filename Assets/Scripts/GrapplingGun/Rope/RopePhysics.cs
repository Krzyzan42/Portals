using System.Collections;
using UnityEngine;

public class RopePhysics : MonoBehaviour
{
	[Header("YMultiplier")]
	public float minYMult = 0.8f;
	public float maxYMult = 2f;
	public float minYMultDist = 3f;
	public float maxYMultDist = 15f;

	[Header("Pull")]
	public float pullForce = 5f;
	public float verticalPullForce = 30f;

	[Header("Physics")]
	public float minPhysicsDist = 5f;
	public float maxPhysicsDist = 50f;

	[HideInInspector]
	public bool isPortalMode = false;

	private Vector3 RopeVector 
	{
		get => connectedPoint - itself.position;
	}

	private Vector3 connectedPoint;
	private Rigidbody itself;


	// Move to fixedupdate
	public void Pull() {
		if(isPortalMode)
			PullPortal();
		else
			PullNormal();
	}

	public float portalPullStrength;
	public float portalMaxVel;
	public float portalPullMinDist;
	private void PullPortal() {
		if(RopeVector.magnitude > portalPullMinDist){
			Vector3 dir = RopeVector.normalized;
			itself.velocity = Vector3.Lerp(itself.velocity, dir * portalMaxVel, portalPullStrength * Time.fixedDeltaTime);	
		}
	}

	private void PullNormal() {
		float dist = RopeVector.magnitude;
		if (dist > maxPhysicsDist || dist < minPhysicsDist)
			return;

		Vector2 horizontalPull = new Vector2(RopeVector.x, RopeVector.z).normalized * pullForce;
		

		float yDist = RopeVector.y;
		float yDistForce = Mathf.Clamp(yDist, minYMultDist, maxYMultDist);
		float yMult = MathExt.map(yDistForce, minYMultDist, maxYMultDist, minYMult, maxYMult);
		float verticalPull = yMult * verticalPullForce;
		if(yDist < 0)
			verticalPull = 0;

		itself.velocity += Time.fixedDeltaTime * new Vector3(horizontalPull.x, verticalPull, horizontalPull.y);
	}

	public void Connect(Vector3 worldHit, Rigidbody itself) {
		connectedPoint = worldHit;
		this.itself = itself;
	}
}
