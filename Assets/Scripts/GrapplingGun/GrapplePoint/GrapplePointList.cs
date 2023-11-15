using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Grappable point database, accessible globally
public class GrapplePointList : MonoBehaviour
{
	[System.NonSerialized]
	public List<Grappable> grapplePoints = new List<Grappable>();

	public static GrapplePointList Instance = null;

	public void Awake() {
		if (Instance != null) {
			Debug.LogError("Two or more grapple point lists in the scene");
		} else
			Instance = this;
	}
}
