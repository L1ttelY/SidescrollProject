using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SensorController:MonoBehaviour {

	[SerializeField] UnityEvent<bool> SetState;

	int stayToken = 0;
	private void OnTriggerStay2D(Collider2D collision) {
		if(!collision.GetComponent<EntityBase>()) return;
		stayToken=2;
	}

	private void FixedUpdate() {

		if(stayToken>0) {
			stayToken--;
			SetState?.Invoke(true);

		}else {

			SetState?.Invoke(false);
		}

	}

}
