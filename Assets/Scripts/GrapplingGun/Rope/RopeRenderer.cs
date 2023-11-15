using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
	private bool visible = false;
	private bool playing = false;
	private new Coroutine animation;

	public Vector3 end;
	public LineRenderer line;
	public int points = 40;

	public float speed = 14f;
	public float strength = 0.7f;
	public AnimationCurve strenghtOverTime;
	public AnimationCurve shape;

	private void Start() {
		Hide();
	}

	public void Show() {

		playing = true;
		visible = true;

		line.enabled = true;
		line.positionCount = points;
		animation = StartCoroutine(RopeAnimation());
	}

	public void Hide() {
		if(playing)
			StopCoroutine(animation);
		line.enabled = false;
		playing = false;
		visible = false;

	}

	private void Update() {
		if (!playing && visible)
			StraightenLine();
	}

	IEnumerator RopeAnimation() {
		float t = 0;

		while (t < 1) {
			Vector3 distVector = end - transform.position;
			Vector3[] positions = new Vector3[points];
			float dist = distVector.magnitude;

			for (int i = 0; i < points; i++) {
				float z = (1f * i / (1f*points)) * dist;
				float s = strenghtOverTime.Evaluate(t) * strength;
				float x = shape.Evaluate(1f*i/points) * s;

				positions[i] = new Vector3(x, x, z);
			}
			line.SetPositions(positions);
			line.transform.LookAt(end);
			yield return null;
			t += Time.deltaTime * speed;
		}
		playing = false;
		StraightenLine();
	}

	void StraightenLine() {
		if(line.positionCount != 2)
			line.positionCount = 2;
		line.SetPosition(0, Vector3.zero);
		line.SetPosition(1, transform.InverseTransformPoint(end));
		//transform.LookAt(end);
	}

}
