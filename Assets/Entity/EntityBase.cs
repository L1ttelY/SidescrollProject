using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EntityBase:MonoBehaviour {

	[SerializeField] float fallSpeed = 5;

	void Start() {
		GetComponentReferences();
	}

	void FixedUpdate() {
		UpdateFallSpeed();
	}

	Rigidbody2D rigidBody;
	new Collider2D collider;
	ObjectGroundedTester groundedTester;
	void GetComponentReferences() {
		rigidBody=GetComponent<Rigidbody2D>();
		collider=GetComponent<Collider2D>();
		groundedTester=GetComponent<ObjectGroundedTester>();
	}


	void UpdateFallSpeed() {
		Vector2 velocity = rigidBody.velocity;
		if(velocity.y<-fallSpeed) velocity.y=-fallSpeed;
		rigidBody.velocity=velocity;
	}
}
