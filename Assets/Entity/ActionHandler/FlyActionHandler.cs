using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyActionHandler:ActionHandlerBase {

	[SerializeField] bool tapFlyBuff = true;
	[SerializeField] float acceleration = 8;
	[SerializeField] float fallSpeed = 2;
	[SerializeField] float flySpeed = 5;
	[SerializeField] float flyTime = 1;
	float flyTimeTotal;

	[HideInInspector] public bool doFly;

	public void ResetFlyTime() {
		flyTime=flyTimeTotal;
	}
	public void PartialResetFlyTime() {
		flyTime=Mathf.Min(flyTimeTotal,flyTime+0.5f*flyTimeTotal);
	}

	DashActionHandler dash;
	protected override void GetComponentReferences() {
		base.GetComponentReferences();
		dash=GetComponent<DashActionHandler>();
		flyTimeTotal=flyTime;
	}

	bool deadFly;
	int tickFlied;
	private void FixedUpdate() {
		if(groundedTester.timeNotGrounded<0.2f) ResetFlyTime();
		if(dash.dashing) doFly=false;

		float deltaSpeed = acceleration*Time.deltaTime;
		Vector2 velocity = rigidbody.velocity;

		bool startFly = tickFlied<3&&doFly;
		tickFlied=doFly ? tickFlied+1 : 0;

		if(!startFly&&groundedTester.grounded) deadFly=true;
		if(startFly) deadFly=false;
		if(deadFly) doFly=false;


		if(flyTime<=0) {

			//»¬ÐÐ
			if(doFly) {
				if(velocity.y+deltaSpeed<-fallSpeed) velocity.y+=deltaSpeed;
				else if(velocity.y<-fallSpeed) velocity.y=-fallSpeed;
			}

		} else {

			//·ÉÐÐ
			if(doFly) {
				float consumeFly = 0;

				float maxYSpeed = Mathf.Max(velocity.y,flySpeed);

				if(startFly&&tapFlyBuff) {
					velocity.y+=acceleration*0.2f;
					consumeFly+=0.1f;
				} else {
					velocity.y+=deltaSpeed;
					consumeFly+=Time.deltaTime;
				}

				flyTime-=consumeFly;

				velocity.y=Mathf.Min(velocity.y,maxYSpeed);

			}

		}

		rigidbody.velocity=velocity;

	}


}
