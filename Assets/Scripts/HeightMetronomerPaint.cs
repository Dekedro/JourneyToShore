using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeightMetronomerPaint : MonoBehaviour {

	Vector3 start;
	Vector3 end;
	Vector3 movePoint;
	float maxheight = 8000;  /* CHANGE THIS VALUE, PLEASE */
	float maxNumberOfPoints = 500; 

	private SubmarineResource _depth;
	private HeightMetronomerPaint _grapher;

	List<Vector3> points;
	Vector3[] P;


	public void DrawHeight(float height) {
		for (int i = 0; i < points.Count; ++i) {
			points [i] -= movePoint;
		}

		float y = end.y + (start.y - end.y)/maxheight*height;
		points.Add(new Vector3(end.x, y, -8));
		if (points.Count > maxNumberOfPoints) {
			points.RemoveAt (0);
		}

		for (int i = 0; i < points.Count; ++i) {
			P [i].x = points [i].x * background.gameObject.transform.lossyScale.x * 0.8f + background.gameObject.transform.position.x;
			P [i].y = points [i].y * background.gameObject.transform.lossyScale.y * 0.8f + background.gameObject.transform.position.y;
			P [i].z = -8;

		}

		for (int i = points.Count; i < maxNumberOfPoints; ++i) {
			P [i] = P [points.Count - 1];
		}
		GetComponent<LineRenderer>().SetPositions(P);
	}

	private SpriteRenderer background;
	void Awake () {
		background = GetComponentInParent<SpriteRenderer> ();

		P = new Vector3[(int)maxNumberOfPoints];
		start = background.sprite.bounds.min;
		end = background.sprite.bounds.max;
		movePoint = new Vector3((end.x - start.x)/maxNumberOfPoints, 0, -8);
		points = new List<Vector3> ();

	}
}