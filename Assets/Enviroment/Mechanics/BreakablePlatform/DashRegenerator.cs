using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRegenerator:MonoBehaviour {

	[SerializeField]float recoverTime;
	float timeAfterUse;
	SpriteRenderer sprite;

	private void OnTriggerEnter2D(Collider2D collision) {
		DashActionHandler dash = collision.GetComponent<DashActionHandler>();
		if(!dash) return;
		if(timeAfterUse<=recoverTime) return;
		dash.GainDash();
		timeAfterUse=0;
	}

	private void Start() {
		sprite=GetComponent<SpriteRenderer>();
		timeAfterUse+=recoverTime;
	}

	private void Update() {
		timeAfterUse+=Time.deltaTime;
		sprite.color=(timeAfterUse>recoverTime) ? Color.green : Color.clear;
	}


}
