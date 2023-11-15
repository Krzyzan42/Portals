using System.Collections;
using UnityEngine;

// Manages physics and renderer
// Grappling gun handles input, tells rope what to do
[RequireComponent(typeof(RopePhysics))]
[RequireComponent(typeof(RopeRenderer))]
public class Rope : MonoBehaviour
{
	private bool pulling = false;
	private bool pullCalled = false;
	private bool connected = false;

	private RopeRenderer ropeRenderer;
	private RopePhysics ropePhysics;


	public virtual void Connect(Rigidbody other, Vector3 worldHit, Rigidbody itself) {
		connected = true;
		ropePhysics.Connect(worldHit, itself);
		ropeRenderer.end = worldHit;
		ropeRenderer.Show();
	}

	public virtual void Disconnect() {
		ropeRenderer.Hide();
		connected = false;
	}

	public virtual void Pull() {
		if (!connected)
			throw new System.InvalidOperationException("Trying to pull with grapple gun without grapple gun connected");
		pullCalled = true;
		pulling = true;
	}

	private void Update() {
		if(!pullCalled && pulling) {
			pulling = false;
		}
		pullCalled = false;
	}

	private void FixedUpdate() {
		if (pulling)
			ropePhysics.Pull();
	}

	private void Awake() {
		ropeRenderer = GetComponent<RopeRenderer>();
		ropePhysics = GetComponent<RopePhysics>();
	}
}
