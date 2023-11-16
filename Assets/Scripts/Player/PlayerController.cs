using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : PortalWalker
{
	private float lastJumpTime = 0f;

	private Rigidbody rb;
	private PlayerInput input;
	private PlayerCamera cam;
	private Vector2 HorizontalVel => new Vector2(rb.velocity.x, rb.velocity.z);

	public GrapplingGun grapplingGun;
	public GroundCheck groundCheck;

	public float rotSpeed = 10;

	public float jumpForce;
	public float jumpCooldown;



	[Header("Movement")]
	public float groundAcceleration;
	public float airAcceleration;
	public float maxGroundHorizontalSpeed = 6;
	public float gravity = 10;

	[Header("Drag")]
	public float groundHorizontalDrag;
	public float standingDrag;

	protected override void Awake() {
		base.Awake();
		input = GetComponent<PlayerInput>();
		rb = GetComponent<Rigidbody>();
		cam = FindObjectOfType<PlayerCamera>();
	}

	protected override void FixedUpdate() {
		ApplyMovement();
		ApplyDrag();
		ApplyGravity();
		base.FixedUpdate();
	}

	void ApplyMovement() {
		if (input.jump)
			TryJump();

		Vector3 dir = input.GetMoveVector();
		Vector3 transfromedDir = Camera.main.transform.TransformDirection(dir);
		Vector2 flattenedDir = new Vector2(transfromedDir.x, transfromedDir.z).normalized;
		float acceleration = groundCheck.onGround ? groundAcceleration : airAcceleration;
		Vector2 hor_vel = HorizontalVel + flattenedDir * Time.fixedDeltaTime * acceleration;
		if(groundCheck.onGround)
			hor_vel = Vector2.ClampMagnitude(hor_vel, maxGroundHorizontalSpeed);
		rb.velocity = new Vector3(hor_vel.x, rb.velocity.y, hor_vel.y);
	}

	void TryJump() {
		if (!groundCheck.onGround)
			return;
		float interval = Time.time - lastJumpTime;
		if (interval > jumpCooldown) {
			if(rb.velocity.y < 0)
				rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			lastJumpTime = Time.time;
		}
	}

	void ApplyDrag() {
		Vector2 horizontalVel = new Vector2(rb.velocity.x, rb.velocity.z);

		float drag = groundHorizontalDrag;
		if(input.GetMoveVector() == Vector3.zero)
			drag = standingDrag;
		if(!groundCheck.onGround)
			drag = 0;
		horizontalVel = Vector2.Lerp(horizontalVel, Vector2.zero, drag * Time.fixedDeltaTime);

		rb.velocity = new Vector3(horizontalVel.x, rb.velocity.y, horizontalVel.y);
	}

	void ApplyGravity() {
		Vector3 vel = rb.velocity;
		vel.y -= gravity * Time.fixedDeltaTime;
		rb.velocity = vel;
	}

	protected override void WalkedThrough(Portal portal){
		cam.OnWalkThroughPortal(portal);
		Vector3 angle = portal.TransformRot(transform.rotation).eulerAngles;
		transform.rotation = Quaternion.Euler(0, angle.y, 0);
		transform.position = portal.TransformPos(transform.position);
		rb.velocity = portal.TransformDir(rb.velocity);
		cam.ForceUpdateTransform();

		grapplingGun.ForceDisconnect();
	}
}
