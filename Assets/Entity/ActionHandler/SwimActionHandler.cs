using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimActionHandler:ActionHandlerBase {

	[SerializeField] float swimSpeed;
	[SerializeField] float acceleration;

	public int swimDirection;


	void FixedUpdate() {

		if(!groundedTester.submerged) return;
		if(swimDirection==0) return;

		Vector2 velocity = rigidbody.velocity;
		float acceleration = this.acceleration*Time.deltaTime;
		float targetSpeed = swimDirection*swimSpeed;

		if(targetSpeed>0) {//œÚ”“
			if(velocity.y<targetSpeed-acceleration) velocity.y+=acceleration;
			else if(velocity.y<targetSpeed) velocity.y=targetSpeed;
		} else if(targetSpeed<0) {//œÚ◊Û
			if(velocity.y>targetSpeed+acceleration) velocity.y-=acceleration;
			else if(velocity.y>targetSpeed) velocity.y=targetSpeed;
		} else {//Õ£÷π
			if(velocity.y>acceleration) velocity.y-=acceleration;
			else if(velocity.y<-acceleration) velocity.y+=acceleration;
			else velocity.y=0;
		}

		rigidbody.velocity=velocity;

	}



}
