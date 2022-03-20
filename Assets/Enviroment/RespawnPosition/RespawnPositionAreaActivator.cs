using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespawnPositionAreaActivator:MonoBehaviour {

	[SerializeField] UnityEvent activationEvent;
	private void OnTriggerEnter2D(Collider2D collision) {
		activationEvent.Invoke();
	}

}