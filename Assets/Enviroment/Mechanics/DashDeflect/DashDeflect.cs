using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("����/����/DashDeflect")]
public class DashDeflect:MonoBehaviour {
	public void OnUse() {
		platform?.Break();
		onDeflect?.Invoke();
	}

	[SerializeField] PlatformBreaker platform;
	public bool doDeflect = true;
	[SerializeField] UnityEvent onDeflect;


}
