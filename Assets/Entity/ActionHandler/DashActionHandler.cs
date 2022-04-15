using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashActionHandler:ActionHandlerBase {

	Vector3 originalScale;

	[SerializeField] bool accelerateAfterDeflect;
	[SerializeField] bool recordSpeed;
	float speedAfterDash;

	[SerializeField] float dashTime;
	[SerializeField] float dashSpeed;
	[SerializeField] float dashEndSpeed;
	[SerializeField] float dashSize;
	[SerializeField] int layerNormal = 7;
	[SerializeField] int layerDashing = 8;

	FlyActionHandler fly;

	[SerializeField] bool canDash;

	[HideInInspector] public Vector2 targetDirection;
	[HideInInspector] public bool doDash;
	public bool dashing { get; private set; }
	int deflect;

	float timeDashed;

	Vector2 dashVelocity;

	float initialGravity;
	protected override void Start() {
		base.Start();
		originalScale=transform.localScale;
		fly=GetComponent<FlyActionHandler>();
		initialGravity=rigidbody.gravityScale;
	}

	private void FixedUpdate() {

		if(bounceToken>0) bounceToken--;

		if(groundedTester.grounded&&!dashing) canDash=true;

		if(dashing) {

			rigidbody.velocity=dashVelocity;
			timeDashed+=Time.deltaTime;

			if(timeDashed>dashTime) EndDash();

		} else {

			if(canDash&&doDash) {

				//¿ªÊ¼³å´Ì
				speedAfterDash=rigidbody.velocity.magnitude;
				timeDashed=0;
				rigidbody.gravityScale=0;
				dashVelocity=targetDirection.normalized*dashSpeed;
				rigidbody.velocity=dashVelocity;
				dashing=true;
				transform.localScale=Vector3.one*dashSize;
				canDash=false;
				gameObject.layer=layerDashing;
				deflect=5;

			}

		}

	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if(!dashing) return;
		if(deflect==0) return;

		string str = "";
		foreach(var i in collision.contacts) str+=i.normal;
		Debug.Log(str);

		DashDeflect other = collision.gameObject.GetComponent<DashDeflect>();
		

		if(other&&other.doDeflect) {

			other.OnUse();
			dashVelocity=Vector2.Reflect(dashVelocity,collision.GetContact(0).normal);
			if(accelerateAfterDeflect) dashVelocity*=1.4f;
			timeDashed=0;
			fly.ResetFlyTime();
			canDash=true;
			deflect--;
			OnBounce(true);

		} else {

			bool doesDeflect=false;
			if(!collision.collider.usedByEffector) doesDeflect=true;
			if(collision.GetContact(0).normal.y>0) doesDeflect=true;
		
			if(doesDeflect) {

				other?.OnUse();
				dashVelocity=Vector2.Reflect(dashVelocity,collision.GetContact(0).normal);
				fly.PartialResetFlyTime();
				timeDashed=dashTime*0.5f;
				deflect--;
				OnBounce(false);

			}
		}

	}

	void EndDash() {
		//½áÊø³å´Ì
		rigidbody.gravityScale=initialGravity;
		rigidbody.velocity=dashVelocity.normalized*(recordSpeed? speedAfterDash:dashEndSpeed);
		dashing=false;
		transform.localScale=originalScale;
		gameObject.layer=layerNormal;
		//fly.PartialResetFlyTime();
	}

	public void GainDash() {
		canDash=true;
	}

	public int bounceToken { get; private set; }
	public bool lastBounceMirror{ get; private set; }
	void OnBounce(bool isMirror){
		bounceToken=2;
		lastBounceMirror=isMirror;
	}

}