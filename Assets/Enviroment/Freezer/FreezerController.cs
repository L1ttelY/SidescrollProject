using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerController:MonoBehaviour {

	[SerializeField] float freezeInterval;
	
	void Update() {
		timeAfterFreze+=Time.deltaTime;
		if(timeAfterFreze>freezeInterval){
			timeAfterFreze-=freezeInterval;

			FreezableBase.OnFreeze(this,transform.position);

		}
	}

	float timeAfterFreze;
}
