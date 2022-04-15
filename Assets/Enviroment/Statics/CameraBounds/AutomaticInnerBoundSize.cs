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
		cameraSize.y=camera.orthographicSize;
		cameraSize.x=cameraSize.y*camera.aspect;
		cameraSize*=2*cameraSizeCoefficiency;

		Vector2 size = outerBound.size-cameraSize;
		if(size.x<=0.01f) size.x=0.01f;
		if(size.y<=0.01f) size.y=0.01f;

		bound.size=size;
		transform.position=bound.transform.position;
		bound.offset=outerBound.offset;
	}

}
