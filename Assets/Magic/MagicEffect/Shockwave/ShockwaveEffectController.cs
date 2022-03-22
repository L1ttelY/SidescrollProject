using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveEffectController:MagicEffectControllerBase {

	[SerializeField] float force;
	[SerializeField] bool useDirectionProcesser;
	[SerializeField] bool eliminateNegativeVelocity;
	[SerializeField] float radius;
	[SerializeField] AnimationCurve dropoff;
	[SerializeField] float airBorneForceModifier = 0.6f;
	[SerializeField] float timeTotal=0.15f;

	ContactFilter2D filterEntity;
	ContactFilter2D filterDefault;
	static readonly Angle upRight = new Angle(60);
	static readonly Angle upLeft = new Angle(120);
	static readonly Angle downRight = new Angle(-75);
	static readonly Angle downLeft = new Angle(-115);

	private void Start() {
		filterEntity=Utility.GetFilterByLayerName("Entity");
		filterDefault=Utility.GetFilterByLayerName("Default");
		filterDefault.useTriggers=true;
	}

	void Update() {

		if(!exploded) Explode();

		time+=Time.deltaTime;
		if(time>timeTotal) Destroy(gameObject);

	}

	void Explode() {

		exploded=true;

		int cnt = Physics2D.OverlapCircleNonAlloc(transform.position,radius,Utility.colliderBuffer,filterEntity.layerMask);
		for(int i = 0;i<cnt;i++) {
			Rigidbody2D other = Utility.colliderBuffer[i].attachedRigidbody;
			if(!other) continue;
			if(other.gameObject.tag=="Stander") continue;

			bool isOtherGrounded = true;
			ObjectGroundedTester otherGrounded = other.GetComponent<ObjectGroundedTester>();
			if(other) isOtherGrounded=otherGrounded.grounded;

			Vector2 otherPosition = other.position;
			Vector2 otherClosestPosition = other.GetComponent<Collider2D>().ClosestPoint(transform.position);
			Vector2 offset = (otherPosition-(Vector2)transform.position).normalized;

			float distance = (otherClosestPosition-(Vector2)transform.position).magnitude;

			if(useDirectionProcesser) offset=DirectionProcesser(offset);

			float forceThisTime = force*(isOtherGrounded ? airBorneForceModifier : 1);

			float normalizedDistance = distance/radius;
			forceThisTime*=dropoff.Evaluate(normalizedDistance);

			if(eliminateNegativeVelocity) other.velocity=Utility.EliminateOnDirection(other.velocity,-offset);
			other.AddForce(offset*forceThisTime,ForceMode2D.Impulse);

		}

		cnt=Physics2D.OverlapCircleNonAlloc(transform.position,radius,Utility.colliderBuffer,filterDefault.layerMask);
		for(int i = 0;i<cnt;i++) {
			Collider2D other = Utility.colliderBuffer[i];
			PlatformBreakByExplosion platform = other.GetComponent<PlatformBreakByExplosion>();
			if(!platform) break;

			platform.OnBreak();

		}
	}

	bool exploded;
	float time;

	Vector2 DirectionProcesser(Vector2 direction) {
		if(((Angle)direction).IfBetween(upRight,upLeft)) {
			direction.y=1;
			if(Mathf.Abs(direction.x)>0.15f) direction.x=direction.x>0 ? 0.3f : -0.3f;
			else direction.x=0;
		} else {
			direction.y=0.3f;
			if(direction.x!=0) direction.x=direction.x>0 ? 1 : -1;
		}
		return direction.normalized;
	}

}