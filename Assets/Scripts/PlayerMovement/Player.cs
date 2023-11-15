using System.Collections;
using UnityEngine;

namespace Assets.Scripts.PlayerMovement
{
	public class Player : MonoBehaviour
	{
		void Update() {
			HandlePortalPlacement();
		}

		void HandlePortalPlacement() {
			bool e = Input.GetKeyDown(KeyCode.E);
			bool q = Input.GetKeyDown(KeyCode.Q);

			// Pressed portal keys
			if (e || q) {

				RaycastHit hit;
				if(Physics.Raycast(Camera.main.ViewportPointToRay(Vector3.one/2), out hit, 1000f, ~0, QueryTriggerInteraction.Collide)) {
					PortalSurface surf = hit.collider.gameObject.GetComponent<PortalSurface>();
					if (surf == null)
						return;
					PortalManager.instance.PlacePortal(surf, hit.point, e);
				}
			}
		}

	}




}