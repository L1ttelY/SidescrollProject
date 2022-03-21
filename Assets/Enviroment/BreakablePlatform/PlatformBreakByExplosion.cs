using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformBreakByExplosion:MonoBehaviour {

	[SerializeField] UnityEvent onBreak;

	public void OnBreak() {
		onBreak.Invoke();
	}

}
