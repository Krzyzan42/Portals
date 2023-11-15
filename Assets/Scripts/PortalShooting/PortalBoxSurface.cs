using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To help build test layouts, runs only once to set selected wals of a box to a portal
public class PortalBoxSurface : MonoBehaviour
{
    public Transform AttachedBox;
    public bool Front, Back, Left, Right, Up, Down;

    void Start() {
        Vector3 size = AttachedBox.lossyScale;
        if (Front) {
            Vector3 worldPos = AttachedBox.TransformPoint(0, 0, 0.5f);
            Quaternion worldOrientation = Quaternion.LookRotation(Vector3.forward) * AttachedBox.rotation;
            GenerateSurface(worldPos, worldOrientation, size.x, size.y);
		}
        if (Back) {
            Vector3 worldPos = AttachedBox.TransformPoint(0, 0, -0.5f);
            Quaternion worldOrientation = Quaternion.LookRotation(Vector3.back) * AttachedBox.rotation;
            GenerateSurface(worldPos, worldOrientation, size.x, size.y);
            // GenerateSurface(new Vector3(0, 0, -size.z / 2), Quaternion.LookRotation(Vector3.back), size.x, size.y);
        }
        if (Left) {
            Vector3 worldPos = AttachedBox.TransformPoint(-0.5f, 0, 0);
            Quaternion worldOrientation = Quaternion.LookRotation(Vector3.left) * AttachedBox.rotation;
            GenerateSurface(worldPos, worldOrientation, size.z, size.y);
            // GenerateSurface(new Vector3(-size.x / 2, 0, 0), Quaternion.LookRotation(Vector3.left), size.z, size.y);
        }
        if (Right) {
            Vector3 worldPos = AttachedBox.TransformPoint(0.5f, 0, 0);
            Quaternion worldOrientation = Quaternion.LookRotation(Vector3.right) * AttachedBox.rotation;
            GenerateSurface(worldPos, worldOrientation, size.z, size.y);
            // GenerateSurface(new Vector3(size.x / 2, 0, 0), Quaternion.LookRotation(Vector3.right), size.z, size.y);
        }
        if (Up) {
            Vector3 worldPos = AttachedBox.TransformPoint(0, 0.5f, 0);
            Quaternion worldOrientation = Quaternion.LookRotation(Vector3.up, Vector3.forward) * AttachedBox.rotation;
            GenerateSurface(worldPos, worldOrientation, size.x, size.z);
            // GenerateSurface(new Vector3(0, size.y / 2, 0), Quaternion.LookRotation(Vector3.up, Vector3.forward), size.x, size.z);
        }
        if (Down) {
            Vector3 worldPos = AttachedBox.TransformPoint(0, -0.5f, 0);
            Quaternion worldOrientation = Quaternion.LookRotation(Vector3.down, Vector3.forward) * AttachedBox.rotation;
            GenerateSurface(worldPos, worldOrientation, size.x, size.z);
            // GenerateSurface(new Vector3(0, -size.y / 2, 0), Quaternion.LookRotation(Vector3.down, Vector3.forward), size.x, size.z);
        }
    }

    void GenerateSurface(Vector3 pos, Quaternion rot, float sizeX, float sizeY) {
        GameObject surfObj = new GameObject();
        surfObj.transform.SetParent(transform);
        surfObj.transform.position = pos;
        surfObj.transform.rotation = rot;

        PortalSurface surf = surfObj.AddComponent<PortalSurface>();
        surf.Size = new Vector2(sizeX, sizeY);
        Collider boxCollider = AttachedBox.GetComponent<Collider>();
        surf.AttachedCollider = boxCollider;
    }

    void Update()
    {
        
    }
}
