using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovementActionHandler:ActionHandlerBase {

	[SerializeField] float accelerationGround = 25;
	[SerializeField] float accelerationAir = 15;
	[SerializeField] float speed = 5;

	[HideInInspector] public int moveDirection;

	DashActionHandler dash;
	protected override void GetComponentReferences() {
		base.GetComponentReferences();
		dash=GetComponent<DashActionHandler>();
	}

	private void FixedUpdate() {
		UpdateHorizontalMovement();
	}

	void UpdateHorizontalMovement() {

		if(dash.dashing) return;

		Vector2 velocity = rigidbody.velocity;
		float acceleration = groundedTester.grounded ? accelerationGround : accelerationAir;
		acceleration*=Time.deltaTime;
		float targetSpeed = moveDirection*speed;

		if(targetSpeed>0) {//œÚ”“

			if(velocity.x<targetSpeed-acceleration) velocity.x+=acceleration;
			else if(velocity.x<targetSpeed) velocity.x=targetSpeed;
			else if(velocity.x>targetSpeed*2) velocity.x=targetSpeed*2;

		} else if(targetSpeed<0) {//œÚ◊Û

			if(velocity.x>targetSpeed+acceleration) velocity.x-=acceleration;
			else if(velocity.x>targetSpeed) velocity.x=targetSpeed;
			else if(velocity.x<targetSpeed*2) velocity.x=targetSpeed*2;

		} else {//Õ£÷π

			if(velocity.x>acceleration) velocity.x-=acceleration;
			else if(velocity.x<-acceleration) velocity.x+=acceleration;
			else velocity.x=0;

		}

		rigidbody.velocity=velocity;

	}

}
