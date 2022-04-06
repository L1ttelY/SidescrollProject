using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("»ú¹Ø/Êä³ö/SlideBlockController")]
public class SlideBlockController:MonoBehaviour {

	[SerializeField] GameObject pointOff;
	[SerializeField] GameObject pointOn;
	[SerializeField] float transistionTime;

	float time;
	public Vector2 velocity{ get; private set; }

	bool state = false;
	public void SetState(bool state) {
		this.state=state;

	}

	private void FixedUpdate() {
		if(state) time+=Time.deltaTime;
		else time-=Time.deltaTime;
		if(time<0) time=0;
		if(time>transistionTime) time=transistionTime;

		Vector3 prvPosition = transform.position;

		float t = time/transistionTime;
		transform.position=Vector3.Lerp(pointOff.transform.position,pointOn.transform.position,t);
		transform.rotation=Quaternion.Lerp(pointOff.transform.rotation,pointOn.transform.rotation,t);

		velocity=(transform.position-prvPosition)/Time.deltaTime;

	}

}
