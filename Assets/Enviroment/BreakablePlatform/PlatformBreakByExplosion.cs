using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformBreakByExplosion:MonoBehaviour {

	[SerializeField] PlatformBreaker platform;

	public void OnBreak() {
		platform.Break();
	}

}
