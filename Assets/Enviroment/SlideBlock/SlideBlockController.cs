using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBlockController:MonoBehaviour {

	[SerializeField] GameObject pointOff;
	[SerializeField] GameObject pointOn;
	[SerializeField] float speed;

	bool state = false;
	public void SetState(bool state) {
		this.state=state;

	}

	private void FixedUpdate() {
		Vector2 target = state ? pointOn.transform.position : pointOff.transform.position;
		float moveDistance = speed*Time.deltaTime;
		if((target-(Vector2)transform.position).sqrMagnitude<=moveDistance*moveDistance) transform.position=target;
		else {

			transform.position=Vector2.MoveTowards(transform.position,target,moveDistance);

		}
	}

}
