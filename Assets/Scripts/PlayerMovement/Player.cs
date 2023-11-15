using System.Collections;
using UnityEngine;

namespace Assets.Scripts.PlayerMovement
{
	public class Player : MonoBehaviour
	{

		private PortalPlacer portalPlacer;
		public Camera mainCamera;

		private void Awake() {
			portalPlacer = GetComponent<PortalPlacer>();
		}

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {
			PlacePortals();
		}

		void PlacePortals() {
			bool e = Input.GetKeyDown(KeyCode.E);
			bool q = Input.GetKeyDown(KeyCode.Q);

			// Pressed portal keys
			if (e || q) {

				RaycastHit hit;
				if(Physics.Raycast(mainCamera.ViewportPointToRay(Vector3.one/2), out hit, 1000f, ~0, QueryTriggerInteraction.Collide)) {
					PortalSurface surf = hit.collider.gameObject.GetComponent<PortalSurface>();
					if (surf == null)
						return;

					Vector3 hitPos = hit.point;
					Collider collider = surf.AttachedCollider; 
					Transform playerTransform = transform.transform;
					BoxSurfaceResult r = surf.GetPortalPosit(hitPos, portalPlacer.portalSize);
					if (e) {
						portalPlacer.PlaceExit(r.position, r.rotation, collider);
					} else {
						portalPlacer.PlaceEnter(r.position, r.rotation, collider);
					}
				}
			}
		}

	}




}