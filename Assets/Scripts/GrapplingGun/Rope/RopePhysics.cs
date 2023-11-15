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
	public float pushForce = 6f;
	public float minPullForce = 5f;
	public float maxPullForce = 30f;
	public float minPullForceDist = 3f;
	public float maxPullForceDist = 30f;

	[Header("Physics")]
	public float minPhysicsDist = 5f;
	public float maxPhysicsDist = 50f;


	private Vector3 RopeVector 
	{
		get => connectedPoint - itself.position;
	}

	private Vector3 connectedPoint;
	private Rigidbody itself;


	// Move to fixedupdate
	public void Pull() {
		float dist = RopeVector.magnitude;
		if (dist > maxPhysicsDist || dist < minPhysicsDist)
			return;

		float distForce = Mathf.Clamp(dist, minPullForceDist, maxPullForceDist);
		float pullForce = MathExt.map(distForce, minPullForceDist, maxPullForceDist, minPullForce, maxPullForce);

		float yDist = RopeVector.y;
		float yDistForce = Mathf.Clamp(yDist, minYMultDist, maxYMultDist);
		float yMult = MathExt.map(yDistForce, minYMultDist, maxYMultDist, minYMult, maxYMult);

		itself.velocity += Time.fixedDeltaTime * pullForce * yMult * RopeVector.normalized;
		itself.velocity += Time.fixedDeltaTime * pushForce * itself.transform.forward;
	}

	public void Connect(Vector3 worldHit, Rigidbody itself) {
		connectedPoint = worldHit;
		this.itself = itself;
	}
}
