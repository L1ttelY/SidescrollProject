using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlatePusher:MonoBehaviour {

	[SerializeField] float force;
	[SerializeField] float cooldown;
	[SerializeField] float pushDistance;

	private void Update() {
		direction=((Angle)transform.rotation+(Angle)90).vector;
	}

	void OnTriggerStay2D(Collider2D collision) {
		EntityBase entity = collision.GetComponent<EntityBase>();

		if(!entity) return;
		if(!pushing) return;

		GameObject otherGo = collision.gameObject;

		if(timeLastHit.ContainsKey(otherGo)&&timeLastHit[otherGo]>Time.time-cooldown) return;
		
		Rigidbody2D other = entity.GetComponent<Rigidbody2D>();
		other.velocity=Utility.EliminateOnDirection(other.velocity,-direction);
		other.velocity+=direction*force;
		Debug.Log(other.velocity);
		other.position+=direction*pushDistance;
		if(!timeLastHit.ContainsKey(otherGo)) timeLastHit.Add(otherGo,Time.time);
		else timeLastHit[otherGo]=Time.time;
	}

	Vector2 direction;
	Dictionary<GameObject,float> timeLastHit = new Dictionary<GameObject,float>();

	float yPrevious;
	bool pushing;
	public void SetPosition(float y) {

		pushing=y>yPrevious;
		yPrevious=y;

		transform.localPosition=new Vector3(0,y);
	}

}
