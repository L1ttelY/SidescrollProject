using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBoltBase:MonoBehaviour {

	[SerializeField] float speed;
	new Rigidbody2D rigidbody;
	Vector2 velocity;
	public GameObject shooter;

	public void Init(Vector2 velocity,GameObject shooter) {
		this.shooter=shooter;
		this.velocity+=velocity;
	}

	protected virtual void Start() {
		rigidbody=GetComponent<Rigidbody2D>();
		velocity+=speed*Angle.FromQuaternion(transform.rotation).vector;
	}

	protected virtual void FixedUpdate() {
		rigidbody.velocity=velocity;
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision) {
		if(!IfCollisionEffective(collision)) return;
		OnImpact(collision.contacts[0].normal);
	}

	public virtual void EndCast(){

	}

	protected virtual bool IfCollisionEffective(Collision2D collision) {
		if(collision.collider.usedByEffector) return false;
		return true;
	}

	protected virtual void OnImpact(Vector2 normal) {
		Destroy(gameObject);
	}

}
