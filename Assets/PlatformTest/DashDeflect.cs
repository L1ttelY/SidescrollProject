using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDeflect:MonoBehaviour {
	public void OnUse() {
		platform?.Break();
	}

	[SerializeField] PlatformBreaker platform;

}
