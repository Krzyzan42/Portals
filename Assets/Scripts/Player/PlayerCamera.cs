using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PlayerMovement;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Vector3 offset;

    float xRot, zRot, yRot;
    PlayerInput input;
    Player player;

    void Awake() {
        player = FindObjectOfType<Player>();
        input = player.gameObject.GetComponent<PlayerInput>();
    }

    void FixedUpdate()
    {
		float hor = input.horMouse;
		float ver = input.verMouse;

		float diff = Mathf.DeltaAngle(0, zRot);
		zRot -= diff * 0.1f;
		
		xRot += ver;
		xRot = Mathf.Clamp(xRot, -90, 90);
		transform.localRotation = Quaternion.Euler(xRot, yRot, zRot);
		Vector3 rot = transform.rotation.eulerAngles;
		rot.y += hor;
        yRot = rot.y;
		transform.rotation = Quaternion.Euler(rot);
        transform.position = player.transform.position + offset;
    }

    void Update() {
        FixedUpdate();
    }

    public void ForceUpdateTransform() {
        transform.position = player.transform.position + offset;
        transform.rotation = Quaternion.Euler(xRot, yRot, zRot);
    }

    public void OnWalkThroughPortal(Portal portal) {
		Vector3 angle = portal.TransformRot(transform.rotation).eulerAngles;
		xRot = Mathf.DeltaAngle(0, angle.x);
		zRot = angle.z;
        yRot = angle.y;

		Camera.main.transform.localRotation = Quaternion.Euler(angle.x, yRot, angle.z);
    }
}
