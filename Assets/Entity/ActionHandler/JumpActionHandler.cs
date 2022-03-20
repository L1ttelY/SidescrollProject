using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpActionHandler:ActionHandlerBase {


	[SerializeField] float jumpSpeed = 5;
	[SerializeField] float minimumJumpTime = 0.1f;
	[SerializeField] float maximumJumpTime = 0.5f;
	[SerializeField] float jumpTolerenceTime = 0.1f;
	[SerializeField] bool waterJump = false;
	[SerializeField] AnimationCurve jumpSpeedCurve;

	public bool doJump;

	private void FixedUpdate() {
		UpdateJump();
	}


	float timeJumpHeld;
	float timeJumped;
	bool jumping;

	void UpdateJump() {
		Vector2 velocity = rigidbody.velocity;

		bool canJump = groundedTester.timeNotGrounded<=jumpTolerenceTime;
		if(waterJump&&groundedTester.floating) canJump=true;

		if(!doJump) timeJumpHeld=-Time.deltaTime;

		if(!jumping&&canJump&&doJump&&timeJumpHeld<=jumpTolerenceTime) {
			jumping=true;
			timeJumped=0;

		}

		timeJumpHeld+=Time.deltaTime;


		if(jumping) {
			//正在跳跃,判断是否要继续跳跃

			timeJumped+=Time.deltaTime;
			if(timeJumped>minimumJumpTime&&timeJumped<=maximumJumpTime) jumping=doJump;
			else if(timeJumped>maximumJumpTime) jumping=false;

			if(groundedTester.ceilinged) jumping=false;

		}

		if(jumping) {
			//正在跳跃
			float currentJumpSpeed = jumpSpeed;
			currentJumpSpeed*=jumpSpeedCurve.Evaluate(timeJumped/maximumJumpTime);
			if(velocity.y<jumpSpeed) velocity.y=currentJumpSpeed;
		}

		rigidbody.velocity=velocity;
	}

}
