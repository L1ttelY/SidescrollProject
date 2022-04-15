using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation:MonoBehaviour {

[SerializeField]float animationSpeed;

	Animator animator;
	new Rigidbody2D rigidbody;
	DashActionHandler dash;
	FlyActionHandler fly;
	ObjectGroundedTester grounded;
	SpriteRenderer spriteRenderer;
	HorizontalMovementActionHandler move;

	void Start() {
		animator=GetComponent<Animator>();
		rigidbody=GetComponent<Rigidbody2D>();
		dash=GetComponent<DashActionHandler>();
		fly=GetComponent<FlyActionHandler>();
		grounded=GetComponent<ObjectGroundedTester>();
		spriteRenderer=GetComponent<SpriteRenderer>();
		move=GetComponent<HorizontalMovementActionHandler>();
	}

	bool dashingPrevious;
	bool landedPrevious;

	void Update() {
		animator.speed=animationSpeed;
		animator.SetFloat("SpeedX",move.relativeSpeed);
		animator.SetFloat("SpeedY",rigidbody.velocity.y);

		bool dashing = dash.dashing;
		bool landed = grounded.grounded;
		if(dashing) landed=false;

			if(dashing&&!dashingPrevious) animator.SetTrigger("Dash");
		if(landed&&!landedPrevious) animator.SetTrigger("Land");
		dashingPrevious=dashing;
		landedPrevious=landed;

		animator.SetBool("Grounded",landed);
		animator.SetBool("Flying",fly.flying);
		animator.SetBool("Dashing",dashing);
		if(rigidbody.velocity.x<-0.01f) spriteRenderer.flipX=true;
		if(rigidbody.velocity.x>0.01f) spriteRenderer.flipX=false;
	}

}
