using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticInnerBoundSize : MonoBehaviour{

	BoxCollider2D bound;
	[SerializeField] BoxCollider2D outerBound;
	new Camera camera;
	[SerializeField] float cameraSizeCoefficiency=1;

	private void Start() {
		bound=GetComponent<BoxCollider2D>();
		camera=Camera.main;
	}

	private void Update() {
		Vector2 cameraSize = Vector2.zero;
		cameraSize.x=camera.orthographicSize;
		cameraSize.y=cameraSize.x/camera.aspect;
		cameraSize*=2*cameraSizeCoefficiency;

		bound.size=outerBound.size-cameraSize;
		transform.position=bound.transform.position;
		bound.offset=outerBound.offset;
	}

}
