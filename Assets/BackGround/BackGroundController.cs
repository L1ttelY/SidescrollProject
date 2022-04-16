using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController:MonoBehaviour {

	[SerializeField] Transform min;
	[SerializeField] Transform max;
	[SerializeField] BoxCollider2D area;
	Camera camera;
	Vector2 CameraSize() {
		float y = camera.orthographicSize;
		float x = camera.orthographicSize*camera.aspect;
		return new Vector2(x,y);
	}

	private void Start() {
		camera=Camera.main;
	}

	private void Update() {
		Vector2 cameraSize = CameraSize();
		Vector2 positionMin = min.position-transform.position;
		Vector2 positionMax = max.position-transform.position;

		Vector2 positionWhenMin = -cameraSize-positionMin;
		Vector2 positionWhenMax = cameraSize-positionMax;

		Bounds bound = area.bounds;

		Vector2 cameraNormalized = camera.transform.position-bound.min;
		cameraNormalized.x/=bound.size.x;
		cameraNormalized.y/=bound.size.y;

		Vector2 positionCurrent = new Vector2 (
			Mathf.Lerp(positionWhenMin.x,positionWhenMax.x,cameraNormalized.x),
			Mathf.Lerp(positionWhenMin.y,positionWhenMax.y,cameraNormalized.y)
		);
		transform.position=(Vector2)camera.transform.position+positionCurrent;

	}
}
