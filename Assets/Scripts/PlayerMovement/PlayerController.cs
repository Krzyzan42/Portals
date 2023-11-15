using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : PortalWalker
{
	private float xRot = 0f;
	private float zRot = 0f;
	private float lastJumpTime = 0f;

	private Rigidbody rb;
	private PlayerInput input;

	public new Transform camera;
	public GrapplingGun grapplingGun;
	public GroundCheck groundCheck;

	public float rotSpeed = 10;

	public float jumpForce;
	public float jumpCooldown;



	[Header("Movement")]
	public float airSpeed;
	public float speed;
	public float maxSpeed = 6;
	public float extraGravity = 10;

	[Header("Drag")]
	public float horizontalDrag;
	public float verticalDrag;
	[Range(0, 1)]
	public float airDragModifier;
	public float minAirDragVel = 0;

	protected override void Awake() {
		base.Awake();
		input = GetComponent<PlayerInput>();
		rb = GetComponent<Rigidbody>();
	}

	protected override void FixedUpdate() {
		UpdateRotation();
		ApplyMovement();
		ApplyDrag();
		ApplyGravity();
		base.FixedUpdate();
	}

	void UpdateRotation() {
		float hor = input.horMouse;
		float ver = input.verMouse;

		float diff = Mathf.DeltaAngle(0, zRot);
		zRot -= diff * 0.1f;
		
		xRot += ver;
		xRot = Mathf.Clamp(xRot, -90, 90);
		camera.localRotation = Quaternion.Euler(xRot, 0, zRot);
		Vector3 rot = transform.rotation.eulerAngles;
		rot.y += hor;
		transform.rotation = Quaternion.Euler(rot);
	}

	void ApplyMovement() {
		if (input.jump)
			TryJump();

		Vector3 dir = input.GetMoveVector();
		Vector3 transfromedDir = camera.TransformDirection(dir);
		Vector2 flattened = new Vector2(transfromedDir.x, transfromedDir.z).normalized;
		Vector2 velocityAdd;
		if (groundCheck.onGround)
			velocityAdd = speed * Time.fixedDeltaTime * flattened;
		else
			velocityAdd = airSpeed * Time.fixedDeltaTime * flattened;
		if (CanMove(velocityAdd)) 
			rb.velocity += new Vector3(velocityAdd.x, 0, velocityAdd.y);
		

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

	bool CanMove(Vector2 velAdd) {
		Vector2 rbVel = new Vector2(rb.velocity.x, rb.velocity.z);
		float speedInFacingDirection = Vector2.Dot(velAdd.normalized, rbVel.normalized) * rbVel.magnitude;
		return speedInFacingDirection < maxSpeed;

	}

	void ApplyDrag() {
		Vector2 horizontalVel = new Vector2(rb.velocity.x, rb.velocity.z);
		float verticalVel = rb.velocity.y;

		float drag = 0f;
		if (groundCheck.onGround)
			drag = horizontalDrag;
		else if(horizontalVel.magnitude > minAirDragVel)
			drag =  horizontalDrag * airDragModifier;
		horizontalVel = Vector2.Lerp(horizontalVel, Vector2.zero, drag * Time.fixedDeltaTime);

		verticalVel = Mathf.Lerp(verticalVel, 0f, verticalDrag*Time.fixedDeltaTime);

		rb.velocity = new Vector3(horizontalVel.x, verticalVel, horizontalVel.y);
	}

	void ApplyGravity() {
		Vector3 vel = rb.velocity;
		vel.y -= extraGravity * Time.fixedDeltaTime;
		rb.velocity = vel;
	}

	protected override void WalkedThrough(Portal portal){
		Vector3 angle = portal.TransformRot(camera.rotation).eulerAngles;
		xRot = Mathf.DeltaAngle(0, angle.x);
		zRot = angle.z;

		camera.localRotation = Quaternion.Euler(angle.x,0, angle.z);
		transform.rotation = Quaternion.Euler(0, angle.y, 0);
		transform.position = portal.TransformPos(transform.position);
		rb.velocity = portal.TransformDir(rb.velocity);

		grapplingGun.ForceDisconnect();
	}
}
