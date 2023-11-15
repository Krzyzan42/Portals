using UnityEngine;
using UnityEngine.Events;


public class PortalManager : MonoBehaviour {
	public Vector2 portalSize;
	public float portalPlacementOffset;

	public static PortalManager instance;
	public Portal enterPortal;
	public Portal exitPortal;
	public Portal enterPortalPrefab;
	public Portal exitPortalPrefab;
	
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

	public void PlacePortal(PortalSurface surface, Vector3 worldPos, bool isEnter) {
		Portal placedPortal = isEnter ? enterPortal : exitPortal;
		Portal otherPortal = isEnter ? exitPortal : enterPortal;
		Portal portalPrefab = isEnter ? enterPortalPrefab : exitPortalPrefab;

		if(placedPortal)
			Destroy(placedPortal.gameObject);
		BoxSurfaceResult result = surface.GetPortalPosit(worldPos, new Vector2(2.05f, 3.05f));
		Quaternion rot = result.rotation;
		Vector3 pos = result.position + rot * Vector3.forward * portalPlacementOffset;
		if(!isEnter)
			rot *= Quaternion.Euler(0, 180, 0);
		placedPortal = Instantiate(portalPrefab, pos, rot);
		placedPortal.surface = surface;
		placedPortal.connectedPortal = otherPortal;
		
		if(otherPortal)
			otherPortal.connectedPortal = placedPortal;

		if(isEnter)
			enterPortal = placedPortal;
		else	
			exitPortal = placedPortal;
		UpdatePortals();
	}
}
