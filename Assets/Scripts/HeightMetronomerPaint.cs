using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeightMetronomerPaint : MonoBehaviour {

	Vector3 start;
	Vector3 end;
	Vector3 movePoint;
	float maxheight = 1000;  /* CHANGE THIS VALUE, PLEASE */
	float maxNumberOfPoints = 500; 

	List<Vector3> points;
	Vector3[] P;


	public void DrawHeight(float height) {
		for (int i = 0; i < points.Count; ++i) {
			points [i] -= movePoint;
		}

		float y = end.y + (start.y - end.y)/maxheight*height;
		points.Add(new Vector3(end.x, y, 0));
		if (points.Count > maxNumberOfPoints) {
			points.RemoveAt (0);
		}

		for (int i = 0; i < points.Count; ++i) {
			P [i].x = points [i].x * background.gameObject.transform.lossyScale.x + background.gameObject.transform.position.x;// * GetComponentInParent<Transform> ().lossyScale.x;

			P [i].y = points [i].y * background.gameObject.transform.lossyScale.y + background.gameObject.transform.position.y;// * GetComponentInParent<Transform> ().lossyScale.y;

			P [i].z = points [i].z * background.gameObject.transform.lossyScale.z + background.gameObject.transform.position.z;// * GetComponentInParent<Transform> ().lossyScale.z;
		}
		for (int i = points.Count; i < maxNumberOfPoints; ++i) {
			P [i] = P [points.Count - 1];
		}
		GetComponent<LineRenderer>().SetPositions(P);
	}

	/*SAMPLE USAGE*/
	/*
	private IEnumerator Whatever() {
		float n = 0;
		while (true) {
			if (n > 900f) {
				n -= 50f;
			} else if (n > 500f) {
				n += 5f;
			} else if (n > 100f) {
				n += 10f;
			} else if (n > 0f) {
				n = 2f * n;
			} else {
				n++;
			}
			Newheight(n);
			yield return new WaitForSeconds (Time.fixedDeltaTime * 1);
		}
		//stop = true;
	}*/
	// Use this for initialization

	private SpriteRenderer background;
	void Start () {
		background = GetComponentInParent<SpriteRenderer> ();
		//background = GetComponentInChildren<SpriteRenderer> ().Where (renderer => renderer.gameObject.name == "Background").First ();

		P = new Vector3[(int)maxNumberOfPoints];
		start = background.sprite.bounds.min;
		Debug.Log (start.x);
		Debug.Log (start.y);
		Debug.Log (start.z);
		end = background.sprite.bounds.max;
		Debug.Log (start.x);
		Debug.Log (start.y);
		Debug.Log (start.z);
		movePoint = new Vector3((end.x - start.x)/maxNumberOfPoints, 0, 0);
		points = new List<Vector3> ();


		/* SAMPLE USAGE */
		/*
		StartCoroutine (Whatever ());	
		*/
	}

}
