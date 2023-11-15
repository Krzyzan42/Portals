using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GrappleBuilding : MonoBehaviour
{
    private Vector3 size;
    private Vector3 grappleSize;

    public Transform Building;
    public List<Transform> GrapplePoints;
    [FormerlySerializedAs("BuildingSize")]
    public Vector3 EditorSize;
    [FormerlySerializedAs("<EditorGrappleSize>GrappleSize")]
    public Vector3 EditorGrappleSize;

    public Vector3 Size {
        get => size;
        set { size = value; UpdatePositions(); }
    }
    public Vector3 GrappleSize {
        get => grappleSize;
        set { grappleSize = value; UpdatePositions(); }
    }


    private void UpdatePositions() {
        Building.localScale = size;

        //float xMax = Size.x / 2 - GrappleSize.x / 2;
        //float yMax = Size.y / 2 + GrappleSize.y / 2;
        //float z = Size.z / 2 - GrappleSize.z / 2;

        float xMax = Size.x / 2;
        float yMax = Size.y / 2;
        float z = Size.z / 2;

        Vector3[] pos = new Vector3[] {
            new Vector3(xMax, yMax, z),
            new Vector3(-xMax, yMax, z),
            new Vector3(xMax, yMax, -z),
            new Vector3(-xMax, yMax, -z)
        };

		for (int i = 0; i < 4; i++) {
            GrapplePoints[i].localScale = grappleSize;
            GrapplePoints[i].localPosition = pos[i];
		}

	}
}
