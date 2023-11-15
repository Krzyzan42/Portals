using System.Collections;
using UnityEngine;

// Highlights currently selectable grapple point
// One highlight point exists, reused for every grapple
// Uses 3d unlit sphere to highlight
public class Highlight : MonoBehaviour
{
	private Transform highlightPoint;


	public GameObject highlightPointPrefab;


	void Awake() {
		highlightPoint = Instantiate(highlightPointPrefab).transform;
		highlightPoint.parent = this.transform;
		highlightPoint.gameObject.SetActive(false);
	}

	// Places highlight on position
	// Highlights until disabled with Disable()
	public void Enable(Vector3 position) {
		highlightPoint.position = position;
		highlightPoint.gameObject.SetActive(true);
	}

	public void Disable() {
		highlightPoint.gameObject.SetActive(false);
	}
}
