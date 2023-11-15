using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalResizer : MonoBehaviour
{
	public float portalThickness;
	public float thickness;
	public float height;
	public float width;
	public bool reverse = false;
	public float grapplePointDepth;

	public Transform left;
	public Transform right;
	public Transform top;
	public Transform bottom;
	public Transform borders;

	public Transform screen;
	public Transform back;

	public WalkDetector walkDetector;
	public Transform clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Awake()
    {
		/*transform.localScale = new Vector3(1, 1, portalThickness);

		top.transform.localPosition = new Vector3(0, height / 2, 0);
		top.transform.localScale = new Vector3(width + thickness, thickness, 1);

		bottom.transform.localPosition = new Vector3(0, -height / 2, 0);
		bottom.transform.localScale = new Vector3(width + thickness, thickness, 1);

		left.transform.localPosition = new Vector3(-width / 2, 0, 0);
		left.transform.localScale = new Vector3(thickness, height + thickness, 1);
		
		right.transform.localPosition = new Vector3(width / 2, 0, 0);
		right.transform.localScale = new Vector3(thickness, height + thickness, 1);

		back.localScale = new Vector3(width / 10, 1, height / 10);
		screen.localScale = new Vector3(width / 10, 1, height / 10);
		borders.localPosition = new Vector3(0, 0, 0);
		if (reverse) {

			screen.localPosition = new Vector3(0, 0, 0.5f);

			back.localPosition = new Vector3(0, 0, 0.5f);
			borders.localScale = new Vector3(1, 1, 1);
			clippingPlane.localPosition = new Vector3(0, 0, -0.5f);
		}
		else {
			screen.localPosition = new Vector3(0, 0, -0.5f);

			back.localPosition = new Vector3(0, 0, -0.5f);
			borders.localScale = new Vector3(1, 1, 1);
			clippingPlane.localPosition = new Vector3(0, 0, 0.5f);
		}

		walkDetector.SetSize(width, height);*/
	}
}
