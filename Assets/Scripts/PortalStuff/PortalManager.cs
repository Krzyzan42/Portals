using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PortalManager : MonoBehaviour {
	public static PortalManager instance;
	public Portal enterPortal;
	public Portal exitPortal;

	[HideInInspector]
	public UnityEvent portalPlacementChanged = new UnityEvent();

	private void Awake() {
		if (instance != null) {
			Debug.LogError("Two portal managers in the scene!");
			Destroy(gameObject);
		}
		else
			instance = this;
	}

	public void UpdatePortals() {
		portalPlacementChanged.Invoke();
	}
}
