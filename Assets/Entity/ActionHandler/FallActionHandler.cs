using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallActionHandler:ActionHandlerBase {

	public bool doFall;

	const int layerNormal = 7;
	const int layerFalling = 8;
	const float minimumFallTime = 0.1f;
	float timeAfterFall = 0;

	[SerializeField] GameObject stander;

	private void FixedUpdate() {
		UpdateFall();
	}

	void UpdateFall() {
		bool doFallThisTick = doFall;

		if(doFall) {
			timeAfterFall=0;
		}
		timeAfterFall+=Time.deltaTime;
		if(timeAfterFall<minimumFallTime) doFallThisTick=true;

		stander.layer=doFallThisTick ? layerFalling : layerNormal;

	}

}
