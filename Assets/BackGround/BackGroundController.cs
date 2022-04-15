using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController:MonoBehaviour {


	[SerializeField] BoxCollider2D bound;
	Camera camera;
	Vector2 CmeraSize() {
		float y = camera.orthographicSize;
		float x = camera.orthographicSize*camera.aspect;
		return new Vector2(x,y);
	}

	//Vector2
}
