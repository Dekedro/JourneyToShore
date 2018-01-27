using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeighMetronomerPaint : MonoBehaviour {

	Vector3 start;
	Vector3 end;
	Vector3 movePoint;
	float maxHeigh = 1000;  /* CHANGE THIS VALUE, PLEASE */
	float maxNumberOfPoints = 500; 


	public GameObject LeftTopCorner;
	public GameObject RightBottomCorner;

	List<Vector3> points;
	Vector3[] P;


	void NewHeigh(float heigh) {
		for (int i = 0; i < points.Count; ++i) {
			points [i] -= movePoint;
		}

		float y = end.y + (start.y - end.y)/maxHeigh*heigh;
		points.Add(new Vector3(end.x, y, 0));
		if (points.Count > maxNumberOfPoints) {
			points.RemoveAt (0);
		}

		for (int i = 0; i < points.Count; ++i) {
			P [i].x = points [i].x * GetComponentInParent<Transform>().lossyScale.x + GetComponentInParent<Transform> ().transform.position.x;// * GetComponentInParent<Transform> ().lossyScale.x;

			P [i].y = points [i].y * GetComponentInParent<Transform>().lossyScale.x +GetComponentInParent<Transform> ().transform.position.y;// * GetComponentInParent<Transform> ().lossyScale.y;

			P [i].z = points [i].z * GetComponentInParent<Transform>().lossyScale.x + GetComponentInParent<Transform> ().transform.position.z;// * GetComponentInParent<Transform> ().lossyScale.z;
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
			NewHeigh(n);
			yield return new WaitForSeconds (Time.fixedDeltaTime * 1);
		}
		//stop = true;
	}*/
	// Use this for initialization
	void Start () {
		P = new Vector3[(int)maxNumberOfPoints];
		start = GetComponentInParent<SpriteRenderer>().sprite.bounds.min;
		Debug.Log (start.x);
		Debug.Log (start.y);
		Debug.Log (start.z);
		end = GetComponentInParent<SpriteRenderer>().sprite.bounds.max;
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
