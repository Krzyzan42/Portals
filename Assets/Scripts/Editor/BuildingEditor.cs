using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GrappleBuilding))]
public class BuildingEditor : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		GrappleBuilding building = (GrappleBuilding)target;
		building.Size = building.EditorSize;
		building.GrappleSize = building.EditorGrappleSize;
	}
}
