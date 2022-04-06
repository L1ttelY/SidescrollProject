using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("»ú¹Ø/Âß¼­/ToggleTrigger")]
public class ToggleTrigger:MonoBehaviour {
	public bool state;
	[SerializeField] bool startOn;
	[SerializeField] UnityEvent<bool> SetState;
	private void Start() {
		state=startOn;
	}

	public void Toggle(){
		state=!state;
	}
	public void SetTrue(){
		state=true;
	}
	public void SetFalse() {
		state=false;
	}
	void FixedUpdate() {
		SetState?.Invoke(state);
	}
}
