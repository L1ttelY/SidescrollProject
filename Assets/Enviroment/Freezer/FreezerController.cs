using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerController:MonoBehaviour {

	[SerializeField] float freezeInterval;

	SpriteRenderer spriteRenderer;
	private void Start() {
		spriteRenderer=GetComponent<SpriteRenderer>();
	}

	void Update() {
		if(activated) {
			timeAfterFreze+=Time.deltaTime;
			if(timeAfterFreze>freezeInterval) {
				timeAfterFreze-=freezeInterval;

				FreezableBase.OnFreeze(this,transform.position);

			}
		}
	}

	public void SetState(bool state) {
		Debug.Log(state);
		activated=state;
		spriteRenderer.color=state ? Color.blue : Color.clear;
	}

	bool activated=true;

	float timeAfterFreze;
}
