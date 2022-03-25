using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformBreakOnStand:MonoBehaviour {

	[SerializeField] float timeToBreak;
	[SerializeField] PlatformBreaker platform;

	float timeSteppedOn = -1;

	private void FixedUpdate() {

		if(timeSteppedOn!=-1) {
			if(timeSteppedOn<Time.time-timeToBreak) {
				timeSteppedOn=-1;
				platform.Break();
			}
		}

	}

	private void OnTriggerStay2D(Collider2D collision) {
		if(!collision.attachedRigidbody) return;
		if(platform.broken) return;
		if(collision.gameObject.layer!=8) return;
		if(timeSteppedOn==-1) timeSteppedOn=Time.time;
	}

}
